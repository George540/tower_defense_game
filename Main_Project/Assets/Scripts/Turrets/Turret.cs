﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Turret : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public float costDeployment;
    public Transform target;
    public Transform childTarget;
    public float range = 25f;
    public Transform partToRotate;
    public Transform partToRotate2;
    public float turnSpeed;

    public GameObject bullet;
    public float fireRate;
    public float fireCountdown;
    public bool isAimingAtTarget;

    // STATS
    public string turretName;
    public int maxHealth;
    public int health;
    public int enemiesDestroyed;
    public int assists;

    public GameObject board;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Finding nearest target
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            if (target.childCount > 0)
                childTarget = nearestEnemy.transform.GetChild(0);
        }
        else
        {
            target = null;
            childTarget = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //UpdateTarget();
        if (target == null)
        {
            return;
        }
        if (target.CompareTag("Enemy") && childTarget != null)
        {
            FindTarget(childTarget);
        }

        /*
        if (fireCountdown <= 0f && target != null && target.CompareTag("Enemy"))
        {
            Invoke("Fire", 0.3f);
            fireCountdown = 1.0f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
        */
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public virtual void FindTarget(Transform target)
    {
        Vector3 direction = target.gameObject.transform.position - transform.position - new Vector3(0.0f, 2.5f, 0.0f);
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        Vector3 rotation2 = Quaternion.Lerp(partToRotate2.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        partToRotate2.rotation = Quaternion.Euler(rotation2.x, rotation2.y, 0f);
    }

    public virtual void Fire()
    {

    }
}
