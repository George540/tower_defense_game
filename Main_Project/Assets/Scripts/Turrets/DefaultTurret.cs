using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DefaultTurret : Turret
{
    public GameObject nozzle;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1f;
        InvokeRepeating("UpdateTarget", 0.2f, 0.4f);
        nozzle = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
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
        GameObject bul = Instantiate(bullet, nozzle.transform.GetChild(0).position, nozzle.transform.GetChild(0).rotation);
        //bul.GetComponent<Bullet>().setBulletSpeed(150);
        bul.GetComponent<Rigidbody>().velocity = nozzle.transform.GetChild(0).right * bul.GetComponent<Bullet>().getBulletSpeed();

        bul = Instantiate(bullet, nozzle.transform.GetChild(1).position, nozzle.transform.GetChild(1).rotation);
        //bul.GetComponent<Bullet>().setBulletSpeed(150);
        bul.GetComponent<Rigidbody>().velocity = nozzle.transform.GetChild(1).right * bul.GetComponent<Bullet>().getBulletSpeed();
    }
}
