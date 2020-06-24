using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : Turret
{
    public GameObject nozzle;
    public LineRenderer lineRenderer;
    public int laserDamage;

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();
        isTargeting();
        if (target == null)
        {
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }
            return;
        }
        if (target.CompareTag("Enemy") && childTarget != null)
        {
            FindTarget(target);
        }

        if (target != null && target.CompareTag("Enemy") && isAimingAtTarget == true)
        {
            Fire();
        }
    }

    public override void FindTarget(Transform target)
    {
        Vector3 direction = target.gameObject.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public override void Fire()
    {
        if (lineRenderer.enabled == true)
        {
            lineRenderer.SetPosition(0, nozzle.transform.position);
            lineRenderer.SetPosition(1, target.transform.GetChild(1).GetChild(1).position);
            if (fireCountdown <= 0)
            {
                target.GetComponent<Enemy>().health -= laserDamage;
                fireCountdown = fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
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
                lineRenderer.enabled = true;
            }
            else
            {
                lineRenderer.enabled = false;
                isAimingAtTarget = false;
            }
        }
        else if (!Physics.Raycast(nozzle.transform.position, nozzle.transform.right, out hit, range))
        {
            lineRenderer.enabled = false;
            isAimingAtTarget = false;
        }
    }
}
