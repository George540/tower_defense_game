using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierEnemy : Enemy
{
    public Transform flankerSpawner;
    public GameObject flanker;
    public GameObject flankerNavigator;
    public float spawnRate;
    public float spawnCooldown;

    // Update is called once per frame
    void Update()
    {
        transform.position = navigator.transform.position;
        rotate();

        checkHealth();

        if (spawnCooldown <= 0 && transform.localRotation.eulerAngles.z == 0)
        {
            Invoke("deployFlanker", 0.3f);
            spawnCooldown = spawnRate + Random.Range(-0.5f, 0.5f);
        }
        spawnCooldown -= Time.deltaTime;
    }

    void deployFlanker()
    {
        GameObject go = Instantiate(flankerNavigator, transform.position, transform.rotation);
        GameObject go2 = Instantiate(flanker, transform.position, transform.rotation);
        go.GetComponent<EnemyNavigator>().enemyObject = go2;
        go2.GetComponent<Enemy>().navigator = go;
    }

}
