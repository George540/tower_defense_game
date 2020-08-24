using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceButton : MonoBehaviour
{
    public Camera cam;
    public Manager manager;
    public GameObject placeableItem;
    public GameObject currentHolder;
    private int cost;

    private Button button;
    private Image buttonImage;

    private void Start()
    {
        cost = placeableItem.GetComponent<Placeable>().cost;
        button = GetComponent<Button>();
        buttonImage = GetComponent<Button>().GetComponent<Image>();
    }

    private void Update()
    {
        checkButton();
    }

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

    void checkButton()
    {
        if (cost <= manager.currency)
        {
            var tempColor = buttonImage.color;
            tempColor.a = 1f;
            buttonImage.color = tempColor;
            button.interactable = true;
        }
        else
        {
            var tempColor = buttonImage.color;
            tempColor.a = 0.5f;
            buttonImage.color = tempColor;
            button.interactable = false;
        }
    }
}
