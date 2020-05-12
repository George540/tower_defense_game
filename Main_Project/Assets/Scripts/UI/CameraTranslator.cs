using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTranslator : MonoBehaviour
{
    public GameObject cameraManager;
    public Transform cameraRig;
    public GameObject selected;
    public float scale;
    public float speed;
    public float speedVertical = 500f;
    public float scrollSpeed = 40f;

    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float maxZ;
    public float minZ;
    public float offset = 200;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraTranslate();
        cameraTranslationVertical();
        if (cameraRig != null)
            transform.eulerAngles = transform.eulerAngles = new Vector3(0, cameraRig.GetComponent<CameraRig>().getYaw(), 0f);

        transform.position = setCameraBoundaries();
        selected = transform.GetChild(0).GetChild(0).GetComponent<SelectObject>().selected;
        if (selected != null)
        {
            //moveCameraToSelected();
        }
    }

    public void cameraTranslate()
    {
        if (Input.GetMouseButton(2) && !Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.Translate(scale * speed * Time.deltaTime, 0, 0);
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                transform.Translate(scale * -speed * Time.deltaTime, 0, 0);
            }
            if (Input.GetAxis("Mouse Y") > 0)
            {
                transform.Translate(0, 0, scale * speed * Time.deltaTime);
            }
            else if (Input.GetAxis("Mouse Y") < 0)
            {
                transform.Translate(0, 0, scale * -speed * Time.deltaTime);
            }
        }
    }

    public void cameraTranslationVertical()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetAxis("Mouse Y") > 0)
            {
                transform.position += new Vector3(0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speedVertical, 0f);
            }
            else if (Input.GetAxis("Mouse Y") < 0)
            {
                transform.position += new Vector3(0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speedVertical, 0f);
            }
        }
    }

    Vector3 setCameraBoundaries()
    {
        minX = cameraManager.GetComponent<Manager>().minX;
        maxX = cameraManager.GetComponent<Manager>().maxX;
        minY = cameraManager.GetComponent<Manager>().minY;
        maxY = cameraManager.GetComponent<Manager>().maxY;
        minZ = cameraManager.GetComponent<Manager>().minZ;
        maxZ = cameraManager.GetComponent<Manager>().maxZ;
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(transform.position.x, minX - offset, maxX + offset);
        position.y = Mathf.Clamp(transform.position.y, minY - offset, maxY + offset);
        position.z = Mathf.Clamp(transform.position.z, minZ - offset, maxZ + offset);
        return position;
    }

    void moveCameraToSelected()
    {
        Vector3 translation = Vector3.Lerp(transform.position, selected.transform.position, Time.deltaTime * scrollSpeed);
        transform.position = translation;
    }
}
