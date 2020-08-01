using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretBoard : MonoBehaviour
{
    public Manager manager;
    public GameObject turret;

    public string turretName;
    public string type;
    public int health;
    public int range;
    public float damage;

    // Targetting System
    private Camera cam;
    public GameObject mortarTarget;
    public GameObject setTarget;

    protected GameObject cameraTranslator;
    protected TurretDestroyer destroyer;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("MANAGER").GetComponent<Manager>();
        manager.currentTurret = turret;
        cameraTranslator = GameObject.FindGameObjectWithTag("Camera");
        destroyer = cameraTranslator.transform.GetChild(0).GetChild(0).GetComponent<TurretDestroyer>();
        cam = GameObject.FindGameObjectWithTag("Camera").transform.GetChild(0).GetChild(0).GetComponent<Camera>();
        if (turret.GetComponent<Mortar>() != null)
        {
            setTarget.GetComponent<Button>().GetComponent<Image>().enabled = true;
            setTarget.GetComponent<Button>().interactable = true;
            setTarget.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = true;
        }
        else
        {
            setTarget.GetComponent<Button>().GetComponent<Image>().enabled = false;
            setTarget.GetComponent<Button>().interactable = false;
            setTarget.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        setSpecs();
    }

    void setSpecs()
    {
        if (turret != null)
        {
            transform.GetChild(1).gameObject.GetComponent<Text>().text = turret.GetComponent<Turret>().turretName;
            if (turret.CompareTag("OffenseTurret"))
                transform.GetChild(2).gameObject.GetComponent<Text>().text = "(Defense)";
            else if (turret.CompareTag("SupportTurret"))
                transform.GetChild(2).gameObject.GetComponent<Text>().text = "(Support)";
            transform.GetChild(3).gameObject.GetComponent<Text>().text = turret.GetComponent<Turret>().health.ToString();
            transform.GetChild(4).gameObject.GetComponent<Text>().text = "Range: " + turret.GetComponent<Turret>().range.ToString();

            if (turret.GetComponent<Turret>().bullet != null)
                transform.GetChild(5).gameObject.GetComponent<Text>().text = "Damage: " + turret.GetComponent<Turret>().bullet.GetComponent<Bullet>().damage.ToString();
            else if (turret.GetComponent<LaserTurret>() != null)
                transform.GetChild(5).gameObject.GetComponent<Text>().text = "Damage: " + turret.GetComponent<LaserTurret>().laserDamage.ToString();
            else if (turret.GetComponent<FlameTurret>() != null)
                transform.GetChild(5).gameObject.GetComponent<Text>().text = "Damage: " + turret.GetComponent<FlameTurret>().fireDamage;
            else if (turret.GetComponent<MineDispenser>() != null)
                transform.GetChild(5).gameObject.GetComponent<Text>().text = "Damage: " + turret.GetComponent<MineDispenser>().bullet.GetComponent<Mine>().damage;

            else if (turret.GetComponent<Healer>() != null)
                transform.GetChild(5).gameObject.GetComponent<Text>().text = "Heal: " + turret.GetComponent<Healer>().fireRate;
            else if (turret.GetComponent<Outpost>() != null)
                transform.GetChild(5).gameObject.GetComponent<Text>().text = "";
            else if (turret.GetComponent<MiningDrill>() != null)
                transform.GetChild(5).gameObject.GetComponent<Text>().text = "Currency: " + turret.GetComponent<MiningDrill>().currencyRate;
            else if (turret.GetComponent<Scout>() != null)
                transform.GetChild(5).gameObject.GetComponent<Text>().text = "";
            else if (turret.GetComponent<Mortar>() != null)
                transform.GetChild(5).gameObject.GetComponent<Text>().text = "Damage: " + turret.GetComponent<Mortar>().damage;

            if (turret.GetComponent<Mortar>() != null && turret.GetComponent<Mortar>().mortarTarget != null)
            {
                turret.GetComponent<Mortar>().mortarTarget.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    public void destroyTurret()
    {
        destroyer.DestroyTurret();
    }

    public void setMortarTarget()
    {
        GameObject currentHolder;
        Destroy(turret.GetComponent<Mortar>().mortarTarget);
        turret.GetComponent<Mortar>().mortarTarget = null;
        currentHolder = Instantiate(mortarTarget, Input.mousePosition, Quaternion.identity);
        currentHolder.GetComponent<MortarTarget>().isTargetSet = false;
        turret.GetComponent<Mortar>().mortarTarget = currentHolder;
        turret.GetComponent<Mortar>().target = currentHolder.transform;
    }
}
