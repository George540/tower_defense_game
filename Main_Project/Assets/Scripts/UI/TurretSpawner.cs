using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class TurretSpawner : MonoBehaviour
{
    public Camera cam;
    public GameObject[] buttons = new GameObject[2];

    private GameObject currentSpawner = null;
    public GameObject[] turrets = new GameObject[2];


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        setButtonsVisibility(false);
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
        if (Input.GetMouseButtonDown(0) && buttons[0].activeSelf == false && buttons[1].activeSelf == false)
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
        else if (Input.GetMouseButtonUp(0) && buttons[0].activeSelf == true && buttons[1].activeSelf == true)
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
        Instantiate(turrets[0], currentSpawner.transform.position, currentSpawner.transform.rotation * Quaternion.Euler(-90f, 0f, -90f));
        setButtonsVisibility(false);
        Destroy(currentSpawner);
    }

    public void SpawnSniper()
    {
        Instantiate(turrets[1], currentSpawner.transform.position, currentSpawner.transform.rotation * Quaternion.Euler(-90f, 0f, -180f));
        setButtonsVisibility(false);
        Destroy(currentSpawner);
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
}
