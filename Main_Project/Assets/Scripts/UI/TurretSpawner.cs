using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TurretSpawner : MonoBehaviour
{
    public Manager manager;
    public Text errorText;

    public Camera cam;
    private GameObject currentSpawner = null;
    private Color previousColor;

    public GameObject build;
    public GameObject noBuild;
    public GameObject turretBoard;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        //setButtonsVisibility(false);
        errorText.enabled = false;
        noBuild.SetActive(false);
        turretBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
        {
            getClickUpdateSpawnTurret();
        }
    }

    //void getClickUpdateSpawnTurret()
    //{
        
    //}

    void getClickUpdateSpawnTurret()
    {
        if (Input.GetMouseButtonDown(0) && currentSpawner == null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if ((hit.transform.gameObject.CompareTag("OffenseSpawner") || hit.transform.gameObject.CompareTag("SupportSpawner")) && hit.transform.gameObject.layer == 9)
                {
                    currentSpawner = hit.transform.gameObject;
                    previousColor = currentSpawner.gameObject.GetComponent<Renderer>().material.color;
                    setStatusColor(Color.green);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && currentSpawner != null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 5 || hit.transform.gameObject.layer == 9)
                {
                    if (hit.transform.gameObject.CompareTag("OffenseSpawner") || hit.transform.gameObject.CompareTag("SupportSpawner"))
                    {
                        setStatusColor(previousColor);
                        currentSpawner = hit.transform.gameObject;
                        previousColor = currentSpawner.gameObject.GetComponent<Renderer>().material.color;
                        setStatusColor(Color.green);
                        return;
                    }
                }
            }
            setStatusColor(previousColor);
            currentSpawner = null;
        }
    }

    /*
    public void SpawnTurret()
    {
        if (canBuy(0))
        {
            GameObject turret = Instantiate(turrets[0], currentSpawner.transform.position, currentSpawner.transform.rotation * Quaternion.Euler(-90f, 0f, -90f));
            //setButtonsVisibility(false);
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
            //setButtonsVisibility(false);
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
            //setButtonsVisibility(false);
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
            //setButtonsVisibility(false);
            Destroy(currentSpawner);
            manager.currency -= turret.GetComponent<Turret>().costDeployment;
        }
        if (errorText.enabled == false)
        {
            errorText.enabled = true;
            Invoke("setInvisibleError", 2f);
        }
    }
    */

    void setStatusColor(Color color)
    {
        if (currentSpawner != null)
        {
            currentSpawner.gameObject.GetComponent<Renderer>().material.color = color;
        }
    }

    /*
    bool canBuy(int i)
    {
        if (manager.currency < turrets[i].GetComponent<Turret>().costDeployment)
            return false;
        else
            return true;
    }
    */

    public GameObject getCurrentSpawner()
    {
        return currentSpawner;
    }

    public void setCurrentSpawner(GameObject spawner)
    {
        currentSpawner = spawner;
    }

    public void enableBuilds()
    {
        foreach (GameObject spawner in manager.turretSpawners)
        {
            if (spawner != null)
            {
                spawner.SetActive(true);
            }
        }
        foreach (GameObject spawner in manager.supportSpawners)
        {
            if (spawner != null)
            {
                spawner.SetActive(true);
            }
        }
        build.SetActive(false);
        noBuild.SetActive(true);
        turretBoard.SetActive(true);
    }

    public void disableBuilds()
    {
        foreach (GameObject spawner in manager.turretSpawners)
        {
            if (spawner != null)
            {
                spawner.SetActive(false);
            }
        }
        foreach (GameObject spawner in manager.supportSpawners)
        {
            if (spawner != null)
            {
                spawner.SetActive(false);
            }
        }
        build.SetActive(true);
        noBuild.SetActive(false);
        turretBoard.SetActive(false);
    }
}
