﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    public float costDeployment;
    public float range;
    public float rotationSpeedScale;
    public float rotationSpeed;

    public float fireRate;
    public float fireCountdown;

    // STATS
    public string turretName;
    public int maxHealth;
    public int health;
    public int turretsInRange;
    public int healRate;
    private int healCountdown;

    public GameObject board;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spinHalo(0, rotationSpeed);
        spinHalo(1, -rotationSpeed/2);

        if (healCountdown <= 0f)
        {
            repairTurrets();
            healCountdown = healRate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void spinHalo(int index, float speed)
    {
        Transform halo = transform.GetChild(index);
        halo.Rotate(new Vector3(0, 0, 1) * speed * rotationSpeedScale * Time.deltaTime);
    }

    void repairTurrets()
    {
        GameObject[] offensive = GameObject.FindGameObjectsWithTag("OffenseTurret");
        GameObject[] support = GameObject.FindGameObjectsWithTag("SupportTurret");

        if (offensive.Length > 0)
        {
            foreach (GameObject turret in offensive)
            {
                if (turret.GetComponent<Turret>() != null && turret.GetComponent<Turret>().health < turret.GetComponent<Turret>().maxHealth && Vector3.Distance(transform.position, turret.transform.position) <= range)
                {
                    turret.GetComponent<Turret>().health += healRate;
                }

            }
        }

        if (support.Length > 0)
        {
            foreach (GameObject turret in support)
            {
                if (turret.GetComponent<Healer>() != null && turret.GetComponent<Healer>().health < turret.GetComponent<Healer>().maxHealth && Vector3.Distance(transform.position, turret.transform.position) <= range)
                {
                    turret.GetComponent<Healer>().health += healRate;
                }
            }
        }

        if (health < maxHealth)
            health += healRate;
    }


}