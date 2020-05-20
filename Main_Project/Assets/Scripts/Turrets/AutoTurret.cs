using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurret : Turret
{
    public GameObject nozzle;
    // Start is called before the first frame update
    void Start()
    {
        fireRate = 20f;
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
        float random1 = Random.Range(-1, 1);
        float random2 = Random.Range(-1, 1);
        float random3 = Random.Range(-1, 1);
        GameObject bul = Instantiate(bullet, nozzle.transform.position, nozzle.transform.rotation * Quaternion.Euler(random1, random2, -90f + random3));
        bul.GetComponent<Rigidbody>().velocity = nozzle.transform.right * bul.GetComponent<Bullet>().getBulletSpeed();
    }
}
