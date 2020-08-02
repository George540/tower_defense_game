using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineDispenser : Turret
{
    public GameObject nozzle;
    public GameObject waveManager;
    public float thrust;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
        waveManager = GameObject.Find("StartWave");
        health = maxHealth;
    }

    void Update()
    {
        checkHealth();
        FindTarget();

        if (fireCountdown <= 0f && waveManager.GetComponent<Button>().GetComponent<Image>().color.a == 0)
        {
            Fire();
            fireCountdown = 1.0f / fireRate;

            float random = Random.Range(-6f, 6f);
            target.Translate(0f, random, 0f);
            Vector3 currentPosition = target.transform.localPosition;
            currentPosition.y = Mathf.Clamp(currentPosition.y, -0.1f, 0.1f);
            target.localPosition = currentPosition;
        }
        if (waveManager.GetComponent<Button>().GetComponent<Image>().color.a == 1)
        {
            fireCountdown = 1.0f / fireRate;
            Vector3 currentPosition = target.transform.localPosition;
            currentPosition.y = 0f;
            target.localPosition = currentPosition;
        }
        else
        {
            fireCountdown -= Time.deltaTime;
        }
    }

    public void FindTarget()
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    override public void Fire()
    {
        float random = Random.Range(-20f, 20f);
        GameObject bul = Instantiate(bullet, nozzle.transform.position, Quaternion.Euler(-90f, 0f, -90f + random));
        bul.GetComponent<Rigidbody>().AddForce(nozzle.transform.forward * (thrust + Random.Range(-40f, 70f)));
        //bul.GetComponent<Rigidbody>().velocity = nozzle.transform.right * bul.GetComponent<Bullet>().getBulletSpeed();
    }
}
