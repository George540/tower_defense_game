using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningDrill : Turret
{
    public Manager manager;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (fireCountdown <= 0)
        {
            manager.currency += currencyRate;
            fireCountdown = fireRate;
        }
        if (manager.isWavePlaying)
        {
            fireCountdown -= Time.deltaTime * Time.timeScale;
            pumpDrill();
        }
        else
        {
            fireCountdown = fireRate;
            drill.position = new Vector3(transform.position.x, originalY, transform.position.z);
        }
    }

    void pumpDrill()
    {
        drill.position = new Vector3(transform.position.x, originalY + ((float)Mathf.Sin(Time.time * timeRate * Time.timeScale) * strength), transform.position.z);
    }
}
