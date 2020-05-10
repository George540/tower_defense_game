using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorTest : MonoBehaviour
{
    public Camera cam;
    public GameObject button;
    public GameObject selectedTurret;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void getClickUpdateSpawnTurret()
    {
        if (Input.GetMouseButtonDown(0) && button.activeSelf == false)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Turret"))
                {
                    button.SetActive(true);
                    selectedTurret = hit.transform.gameObject;
                    //setStatusColor(Color.green);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && button.activeSelf == true)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Turret"))
                {
                    button.SetActive(true);
                    //setStatusColor(Color.red);
                    selectedTurret = hit.transform.gameObject;
                    //setStatusColor(Color.green);
                    return;
                }
            }
            button.SetActive(false);
            // setStatusColor(Color.red);
            selectedTurret = null;
        }
    }
}
