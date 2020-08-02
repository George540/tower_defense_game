using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningDrill : Turret
{
    private Transform drill;
    private float originalY;
    public float strength;
    public float timeRate;
    public float currencyRate;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("MANAGER").GetComponent<Manager>();
        drill = transform.GetChild(0);
        originalY = drill.position.y;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();
        if (fireCountdown <= 0 && manager.isWavePlaying)
        {
            manager.currency += currencyRate;
            fireCountdown = fireRate;
        }
        if (manager.isWavePlaying)
        {
            fireCountdown -= Time.deltaTime;
            pumpDrill();
        }
        else
        {
            drill.position = new Vector3(transform.position.x, originalY, transform.position.z);
        }
    }

    void pumpDrill()
    {
        drill.position = new Vector3(transform.position.x, originalY + ((float)Mathf.Sin(Time.time * timeRate * Time.timeScale) * strength), transform.position.z);
    }
}
