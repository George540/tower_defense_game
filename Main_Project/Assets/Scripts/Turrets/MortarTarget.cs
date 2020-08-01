using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarTarget : MonoBehaviour
{
    public Material[] materials = new Material[2];
    private Camera cam;
    private Vector3 mousePosition;
    private bool isPlaceable;

    public bool isSettingTarget;
    public bool isTargetSet;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("Camera").transform.GetChild(0).GetChild(0).GetComponent<Camera>();
        GetComponent<Renderer>().material = materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        setTargetArea();
        checkColor();
        placeTarget();
    }

    void setTargetArea()
    {
        if (isTargetSet == false)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                transform.position = hit.point;
                if (hit.transform.gameObject.CompareTag("PlaceArea"))
                {
                    isPlaceable = true;
                }
                else
                {
                    isPlaceable = false;
                }
            }
            else
            {
                mousePosition = Input.mousePosition;
                mousePosition.z = 70f;
                transform.position = cam.ScreenToWorldPoint(mousePosition);
                isPlaceable = false;
            }
        }
    }

    void checkColor()
    {
        if (isPlaceable == true)
        {
            GetComponent<Renderer>().material = materials[1];
        }
        else
        {
            GetComponent<Renderer>().material = materials[0];
        }
    }

    void placeTarget()
    {
        if (Input.GetMouseButtonDown(0) && isPlaceable)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                if (hit.transform.gameObject.CompareTag("PlaceArea"))
                {
                    isTargetSet = true;
                }
            }
        }
    }
}
