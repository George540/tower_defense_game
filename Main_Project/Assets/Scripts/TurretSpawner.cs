using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretSpawner : MonoBehaviour
{
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        //GetComponent<Button>().interactable = false;
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (camera != null)
            getClickUpdate();
    }

    void getClickUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
                if (hit.transform.CompareTag("TurretSpawner"))
                {
                    Debug.Log(hit.transform.name);
                    Destroy(hit.transform);
                }
            }
        }
    }
}
