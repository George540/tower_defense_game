using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurret : Turret
{
    public GameObject nozzle;

    // Update is called once per frame
    void Update()
    {
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
            FindTarget(childTarget);
        }

        if (fireCountdown <= 0f && target != null && target.CompareTag("Enemy") && isAimingAtTarget == true)
        {
            Fire();
            fireCountdown = 1.0f / fireRate;
        }
    }

    override public void Fire()
    {
        GameObject bul = Instantiate(bullet, nozzle.transform.position, nozzle.transform.rotation * Quaternion.Euler(0f, 0f, -90f));
        bul.GetComponent<Rigidbody>().velocity = nozzle.transform.right * bul.GetComponent<Bullet>().getBulletSpeed();
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
