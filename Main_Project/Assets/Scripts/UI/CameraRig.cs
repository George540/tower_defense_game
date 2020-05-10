using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform cameraTranslator;
    public Vector3 currentPosition;
    public float cameraRadius;
    public float yaw;
    public float pitch;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cameraRotation();
        transform.position = cameraTranslator.position;
        currentPosition = transform.position;
    }

    public void cameraRotation()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            yaw += 10f * Input.GetAxis("Mouse X");
        }
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    public float getYaw()
    {
        return yaw;
    }

    public float getRadius()
    {
        return cameraRadius;
    }
}
