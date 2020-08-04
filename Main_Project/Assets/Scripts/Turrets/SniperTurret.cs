using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTurret : Turret
{
    public GameObject nozzle;
    public int damage;
    public ParticleSystem smoke;

    void Start()
    {
        manager = FindObjectOfType<Manager>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();
        UpdateTarget();
        isTargeting();
        if (target == null)
        {
            fireCountdown = 1.0f / fireRate;
            return;
        }
        if (target.CompareTag("Enemy") && childTarget != null)
        {
            fireCountdown -= Time.deltaTime;
            FindTarget(target);
        }

        if (fireCountdown <= 0f && target != null && target.CompareTag("Enemy") && isAimingAtTarget == true)
        {
            Fire();
            fireCountdown = 1.0f / fireRate;
        }
    }

    public new void UpdateTarget()
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

    override public void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(nozzle.transform.position, nozzle.transform.right, out hit, range))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.gameObject.GetComponent<Enemy>().health -= damage;
            }
        }
        smoke.Play();
        /*
        GameObject bul = Instantiate(bullet, nozzle.transform.position, nozzle.transform.rotation * Quaternion.Euler(0f, 0f, -90f));
        bul.GetComponent<Rigidbody>().velocity = nozzle.transform.right * bul.GetComponent<Bullet>().getBulletSpeed();
        */
    }

    void isTargeting()
    {
        Debug.DrawRay(nozzle.transform.position, nozzle.transform.right * range, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(nozzle.transform.position, nozzle.transform.right, out hit, range))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                isAimingAtTarget = true;
            }
            else
            {
                isAimingAtTarget = false;
            }
        }
        else if (!Physics.Raycast(nozzle.transform.position, nozzle.transform.right, out hit, range))
        {
            isAimingAtTarget = false;
        }
    }
}
