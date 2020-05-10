using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretDestroyer : MonoBehaviour
{
    public Camera cam;
    public GameObject button;

    private GameObject currentTurret = null;
    public GameObject spawner;

    private GameObject currentSelector;
    public GameObject selector;
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
                if (hit.transform.gameObject.CompareTag("Turret") && hit.transform.gameObject.layer == 9)
                {
                    button.SetActive(true);
                    currentTurret = hit.transform.gameObject;
                    currentSelector = Instantiate(selector, transform.position + new Vector3(0f, 20f, 0f), transform.rotation);
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
                    if (hit.transform.gameObject.CompareTag("Turret"))
                    {
                        button.SetActive(true);
                        Destroy(currentSelector);
                        currentTurret = hit.transform.gameObject;
                        currentSelector = Instantiate(selector, currentTurret.transform.position + new Vector3(0f, 15f, 0f), currentTurret.transform.rotation);
                        return;
                    }
                }
            }
            button.SetActive(false);
            currentTurret = null;
            Destroy(currentSelector);
        }
    }

    public void DestroyTurret()
    {
        Instantiate(spawner, currentTurret.transform.position, currentTurret.transform.rotation * Quaternion.Euler(0f, 90f, 90f));
        button.SetActive(false);
        Destroy(currentSelector);
        Destroy(currentTurret);
    }
}
