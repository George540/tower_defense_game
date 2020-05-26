using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class TurretSpawner : MonoBehaviour
{
    public Manager manager;
    public Text errorText;

    public Camera cam;
    public GameObject[] buttons = new GameObject[2];

    private GameObject currentSpawner = null;
    public GameObject[] turrets = new GameObject[2];


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        setButtonsVisibility(false);
        errorText.enabled = false;
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
        if (Input.GetMouseButtonDown(0) && buttons[0].activeSelf == false && buttons[1].activeSelf == false && buttons[2].activeSelf == false && buttons[3].activeSelf == false)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("TurretSpawner") && hit.transform.gameObject.layer == 9)
                {
                    setButtonsVisibility(true);
                    currentSpawner = hit.transform.gameObject;
                    setStatusColor(Color.green);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && buttons[0].activeSelf == true && buttons[1].activeSelf == true && buttons[2].activeSelf == true && buttons[3].activeSelf == true)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 5 || hit.transform.gameObject.layer == 9)
                {
                    if (hit.transform.gameObject.CompareTag("TurretSpawner"))
                    {
                        setButtonsVisibility(true);
                        setStatusColor(Color.red);
                        currentSpawner = hit.transform.gameObject;
                        setStatusColor(Color.green);
                        return;
                    }
                }
            }
            setButtonsVisibility(false);
            setStatusColor(Color.red);
            currentSpawner = null;
        }
    }

    public void SpawnTurret()
    {
        if (canBuy(0))
        {
            GameObject turret = Instantiate(turrets[0], currentSpawner.transform.position, currentSpawner.transform.rotation * Quaternion.Euler(-90f, 0f, -90f));
            setButtonsVisibility(false);
            Destroy(currentSpawner);
            manager.currency -= turret.GetComponent<Turret>().costDeployment;
        }
        if (errorText.enabled == false)
        {
            errorText.enabled = true;
            Invoke("setInvisibleError", 2f);
        }
    }

    public void SpawnAuto()
    {
        if (canBuy(1))
        {
            GameObject turret = Instantiate(turrets[1], currentSpawner.transform.position, currentSpawner.transform.rotation * Quaternion.Euler(-90f, 0f, -90f));
            setButtonsVisibility(false);
            Destroy(currentSpawner);
            manager.currency -= turret.GetComponent<Turret>().costDeployment;
        }
        if (errorText.enabled == false)
        {
            errorText.enabled = true;
            Invoke("setInvisibleError", 2f);
        }
    }

    public void SpawnSniper()
    {
        if (canBuy(2))
        {
            GameObject turret = Instantiate(turrets[2], currentSpawner.transform.position, currentSpawner.transform.rotation * Quaternion.Euler(-90f, 0f, -180f));
            setButtonsVisibility(false);
            Destroy(currentSpawner);
            manager.currency -= turret.GetComponent<Turret>().costDeployment;

        }
        if (errorText.enabled == false)
        {
            errorText.enabled = true;
            Invoke("setInvisibleError", 2f);
        }
    }

    public void SpawnRocket()
    {
        if (canBuy(3))
        {
            GameObject turret = Instantiate(turrets[3], currentSpawner.transform.position, currentSpawner.transform.rotation * Quaternion.Euler(-90f, 0f, -180f));
            setButtonsVisibility(false);
            Destroy(currentSpawner);
            manager.currency -= turret.GetComponent<Turret>().costDeployment;
        }
        if (errorText.enabled == false)
        {
            errorText.enabled = true;
            Invoke("setInvisibleError", 2f);
        }
    }

    void setStatusColor(Color color)
    {
        if (currentSpawner != null)
        {
            currentSpawner.gameObject.GetComponent<Renderer>().material.color = color;
        }
    }

    void setButtonsVisibility(bool switcher)
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(switcher);
        }
    }

    bool canBuy(int i)
    {
        if (manager.currency < turrets[i].GetComponent<Turret>().costDeployment)
            return false;
        else
            return true;
    }

    void setInvisibleError()
    {
        errorText.enabled = false;
    }
}
