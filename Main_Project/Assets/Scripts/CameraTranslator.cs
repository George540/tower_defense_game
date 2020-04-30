using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTranslator : MonoBehaviour
{
    public Transform cameraRig;
    public float speed = 0.2f;
    public float speedVertical = 500f;
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
    }

    public void cameraTranslate()
    {
        if (Input.GetMouseButton(2) && !Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
            if (Input.GetAxis("Mouse Y") > 0)
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
            }
            else if (Input.GetAxis("Mouse Y") < 0)
            {
                transform.Translate(0, 0, -speed * Time.deltaTime);
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
}
