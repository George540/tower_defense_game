﻿using System;
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
        checkCost();
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
        GameObject go = Instantiate(turret, currentSpawner.transform.position, currentSpawner.transform.rotation * Quaternion.Euler(-90f, 0f, -180f));
        if (currentSpawner.CompareTag("OffenseSpawner"))
        {
            manager.turretSpawners.Remove(currentSpawner);
        }
        else if (currentSpawner.CompareTag("SupportSpawner"))
        {
            manager.supportSpawners.Remove(currentSpawner);
            if (go.GetComponent<Mortar>())
            {
                go.transform.Rotate(0f, 0f, -90f);
            }
            else if (go.GetComponent<Outpost>() != null || go.GetComponent<Scout>() != null || go.GetComponent<MiningDrill>() != null || go.GetComponent<Healer>() != null)
            {
                go.transform.Rotate(0f, 0f, 90f);
            }
        }
        manager.currency -= cost;
        manager.numberOfTurrets++;
        Destroy(currentSpawner);
    }

    void checkCost()
    {
        if (turret.GetComponent<Turret>() != null)
            cost = turret.GetComponent<Turret>().costDeployment;
        else if (turret.GetComponent<Healer>() != null)
            cost = turret.GetComponent<Healer>().costDeployment;
    }
}
