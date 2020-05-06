using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class TurretSpawner : MonoBehaviour
{
    public Camera cam;
    public GameObject button;
    public GameObject currentSpawner = null;
    public GameObject turret;
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
            getClickUpdate();
    }

    void getClickUpdate()
    {
        if (Input.GetMouseButtonDown(0) && button.activeSelf == false)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("TurretSpawner") && hit.transform.gameObject.layer == 9)
                {
                    button.SetActive(true);
                    currentSpawner = hit.transform.gameObject;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && button.activeSelf == true)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 5 || hit.transform.gameObject.layer == 9)
                {
                    button.SetActive(true);
                    currentSpawner = hit.transform.gameObject;
                    return;
                }
            }
            button.SetActive(false);
            currentSpawner = null;
        }
    }

    public void SpawnTurret()
    {
        Instantiate(turret, currentSpawner.transform.position, currentSpawner.transform.rotation * Quaternion.Euler(-90f, 0f, -90f));
        button.SetActive(false);
        Destroy(currentSpawner);
    }
}
