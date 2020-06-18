using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretBoard : MonoBehaviour
{
    public GameObject turret;

    public string turretName;
    public string type;
    public int health;
    public int range;
    public float damage;

    private GameObject cameraTranslator;
    private TurretDestroyer destroyer;
    // Start is called before the first frame update
    void Start()
    {
        cameraTranslator = GameObject.FindGameObjectWithTag("Camera");
        destroyer = cameraTranslator.transform.GetChild(0).GetChild(0).GetComponent<TurretDestroyer>();
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
            if (turret.GetComponent<LaserTurret>() == null)
                transform.GetChild(5).gameObject.GetComponent<Text>().text = "Damage: " + turret.GetComponent<Turret>().bullet.GetComponent<Bullet>().damage.ToString();
            else if (turret.GetComponent<LaserTurret>() != null)
                transform.GetChild(5).gameObject.GetComponent<Text>().text = "Damage: " + turret.GetComponent<LaserTurret>().laserDamage.ToString();
        }
    }

    public void destroyTurret()
    {
        destroyer.DestroyTurret();
    }
}
