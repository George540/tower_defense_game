using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTurret : Turret
{
    public GameObject nozzle;
    // Start is called before the first frame update
    void Start()
    {
        fireRate = 0.5f;
        InvokeRepeating("UpdateTarget", 0.2f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        if (target == gameObject.transform.GetChild(0))
        {
            FindTarget(target);
        }
        if (target.CompareTag("Enemy") && childTarget != null)
        {
            FindTarget(childTarget);
        }
        if (fireCountdown <= 0f && target != null && target.CompareTag("Enemy"))
        {
            Invoke("Fire", 0.3f);
            fireCountdown = 1.0f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    override public void Fire()
    {
        GameObject bul = Instantiate(bullet, nozzle.transform.position, nozzle.transform.rotation * Quaternion.Euler(0f, 0f, -90f));
        //bul.GetComponent<Bullet>().setBulletSpeed(400);
        bul.GetComponent<Rigidbody>().velocity = nozzle.transform.right * bul.GetComponent<Bullet>().getBulletSpeed();
    }
}
