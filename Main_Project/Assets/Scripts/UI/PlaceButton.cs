using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceButton : MonoBehaviour
{
    public Camera cam;

    public GameObject placeableItem;
    public GameObject currentHolder;

    public void spawnPlaceable()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float angle = Random.Range(0f, 360f);
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            if (currentHolder == null)
            {
                currentHolder = Instantiate(placeableItem, hit.point, Quaternion.identity * Quaternion.Euler(-90f, 0f, angle));
                return;
            }
            else
            {
                Destroy(currentHolder);
                currentHolder = null;
                currentHolder = Instantiate(placeableItem, hit.point, Quaternion.identity * Quaternion.Euler(-90f, 0f, angle));
                return;
            }
        }
        else
        {
            if (currentHolder == null)
            {
                currentHolder = Instantiate(placeableItem, Input.mousePosition, Quaternion.identity * Quaternion.Euler(-90f, 0f, angle));
                return;
            }
            else
            {
                Destroy(currentHolder);
                currentHolder = null;
                currentHolder = Instantiate(placeableItem, Input.mousePosition, Quaternion.identity * Quaternion.Euler(-90f, 0f, angle));
                return;
            }
        }
    }
}
