using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTurret : Turret
{
    public LayerMask ignoreLayer;
    public GameObject nozzle;
    public ParticleSystem flame;
    public int fireDamage;

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();
        isTargeting();
        if (target == null)
        {
            if (flame.emission.enabled == true)
            {
                flame.Stop();
            }
            return;
        }
        if (target.CompareTag("Enemy") && childTarget != null)
        {
            FindTarget(childTarget);
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
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed * Time.timeScale).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public override void Fire()
    {
        if (flame.isPlaying == false)
            flame.Play();
    }

    void isTargeting()
    {
        Debug.DrawRay(nozzle.transform.position, nozzle.transform.right * range, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(nozzle.transform.position, nozzle.transform.right, out hit, range, ~ignoreLayer))
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
        else if (!Physics.Raycast(nozzle.transform.position, nozzle.transform.right, out hit, range, ~ignoreLayer))
        {
            isAimingAtTarget = false;
        }
    }
}
