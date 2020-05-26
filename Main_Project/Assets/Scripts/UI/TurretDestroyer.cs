using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretDestroyer : MonoBehaviour
{
    public Manager manager;
    public Text errorText;

    public Camera cam;
    public GameObject button;

    public GameObject currentTurret = null;
    public GameObject spawner;

    private GameObject currentSelector;
    public GameObject selector;

    private GameObject currentRanger;
    public GameObject ranger;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
        {
            getClickUpdateSpawnTurret();
        }
    }

    void getClickUpdateSpawnTurret()
    {
        if (Input.GetMouseButtonDown(0) && button.activeSelf == false)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Turret") && hit.transform.gameObject.layer == 9 && currentTurret != hit.transform.gameObject)
                {
                    button.SetActive(true);
                    currentTurret = hit.transform.gameObject;
                    currentSelector = Instantiate(selector, currentTurret.transform.position + new Vector3(0f, 15f, 0f), currentTurret.transform.rotation);
                    currentRanger = Instantiate(ranger, currentTurret.transform.position + new Vector3(0f, 0f, 0f), currentTurret.transform.rotation * Quaternion.Euler(90f, 0f, 0f));
                    currentRanger.transform.localScale = new Vector3(currentTurret.GetComponent<Turret>().range * 2, ranger.transform.localScale.y, currentTurret.GetComponent<Turret>().range * 2);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && button.activeSelf == true)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            button.SetActive(false);
            currentTurret = null;
            Destroy(currentSelector);
            Destroy(currentRanger);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Turret") && hit.transform.gameObject.layer == 9 && currentTurret != hit.transform.gameObject)
                {
                    button.SetActive(true);
                    Destroy(currentSelector);
                    currentTurret = hit.transform.gameObject;
                    currentSelector = Instantiate(selector, currentTurret.transform.position + new Vector3(0f, 15f, 0f), currentTurret.transform.rotation);
                    currentRanger = Instantiate(ranger, currentTurret.transform.position + new Vector3(0f, 0f, 0f), currentTurret.transform.rotation * Quaternion.Euler(90f, 0f, 0f));
                    currentRanger.transform.localScale = new Vector3(currentTurret.GetComponent<Turret>().range * 2, ranger.transform.localScale.y, currentTurret.GetComponent<Turret>().range * 2);
                    return;
                }
            }
            
        }
    }

    public void DestroyTurret()
    {
        Instantiate(spawner, currentTurret.transform.position, currentTurret.transform.rotation * Quaternion.Euler(0f, 90f, 90f));
        button.SetActive(false);
        manager.currency += currentTurret.GetComponent<Turret>().costDeployment/2;
        Destroy(currentSelector);
        Destroy(currentRanger);
        Destroy(currentTurret);
    }
}
