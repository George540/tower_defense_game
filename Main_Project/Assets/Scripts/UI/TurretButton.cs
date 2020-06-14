using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretButton : MonoBehaviour
{
    public TurretSpawner turretSpawner;
    private GameObject currentSpawner;
    public Manager manager;
    public GameObject turret;
    private float cost;
    private bool canBuy;

    private Button button;
    private Image buttonImage;
    private Text costText;

    public string type;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Button>().GetComponent<Image>();
        cost = turret.GetComponent<Turret>().costDeployment;
        costText = transform.GetChild(0).GetComponent<Text>();
        costText.text = cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        checkSelectedSpawner();
        checkButton();
    }

    void checkSelectedSpawner()
    {
        if (turretSpawner.getCurrentSpawner() != null)
            currentSpawner = turretSpawner.getCurrentSpawner();
    }
    void checkButton()
    {
        if (cost <= manager.currency && matchSelection() == true)
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

    public bool matchSelection()
    {
        if (turretSpawner.getCurrentSpawner() != null)
        {
            if (currentSpawner.tag == type)
                return true;
            else
                return false;
        }
        return false;
    }

    public void spawnTurret()
    {
        Instantiate(turret, currentSpawner.transform.position, currentSpawner.transform.rotation * Quaternion.Euler(-90f, 0f, -180f));
        if (currentSpawner.CompareTag("OffenseSpawner"))
        {
            manager.turretSpawners.Remove(currentSpawner);
        }
        else if (currentSpawner.CompareTag("SupportSpawner"))
        {
            manager.supportSpawners.Remove(currentSpawner);
        }
        manager.currency -= cost;
        Destroy(currentSpawner);
    }
}
