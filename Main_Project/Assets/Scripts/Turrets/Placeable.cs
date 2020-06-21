using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    private GameObject manager;
    public Material[] materials = new Material[2];
    public GameObject item;
    public int cost;
    private Camera cam;
    private Vector3 mousePosition;
    private bool isPlaceable;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        cam = GameObject.FindGameObjectWithTag("Camera").transform.GetChild(0).GetChild(0).GetComponent<Camera>();
        GetComponent<Renderer>().material = materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        checkPosition();
        checkColor();
        placeItem();
    }

    void checkPosition()
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
        }
    }

    void checkColor ()
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

    void placeItem()
    {
        if (Input.GetMouseButtonDown(0) && isPlaceable)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                if (hit.transform.gameObject.CompareTag("PlaceArea"))
                {
                    GameObject go = Instantiate(item, transform.position, transform.rotation);
                    Destroy(gameObject);
                    manager.GetComponent<Manager>().currency -= cost;
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && !isPlaceable)
        {
            Destroy(gameObject);
        }
    }
}
