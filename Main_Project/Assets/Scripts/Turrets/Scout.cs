﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : Turret
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("detectStealthy", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void detectStealthy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Finding nearest target
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < range && enemy.GetComponent<Enemy>().isStealthy == true)
            {
                enemy.GetComponent<Enemy>().isStealthy = false;
            }
        }
    }
}
