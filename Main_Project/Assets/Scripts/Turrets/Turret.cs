using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Turret : MonoBehaviour
{
    protected Manager manager;
    public GameObject spawner;

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
    public bool isRangeIncreased;

    // STATS
    public string turretName;
    public int maxHealth;
    public int health;
    public int enemiesDestroyed;
    public int assists;

    public GameObject board;
    private AudioSource fireSound;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
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

        if (nearestEnemy != null && shortestDistance <= range && nearestEnemy.GetComponent<Enemy>().isStealthy == false)
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

    protected void checkHealth()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0)
        {
            DestroyTurret();
        }
    }

    protected void DestroyTurret()
    {
        GameObject go;
        if (gameObject.CompareTag("OffenseTurret"))
        {
            go = Instantiate(spawner, transform.position, transform.rotation * Quaternion.Euler(-90f, 90f, 90f));
            manager.turretSpawners.Add(go);
        }
        else if (gameObject.CompareTag("SupportTurret"))
        {
            go = Instantiate(spawner, gameObject.transform.position, gameObject.transform.rotation * Quaternion.Euler(-90f, 00f, 90f));
            if (gameObject.GetComponent<Mortar>() != null || gameObject.GetComponent<Scout>() != null || gameObject.GetComponent<Healer>() != null || gameObject.GetComponent<MiningDrill>() != null || gameObject.GetComponent<Outpost>() != null)
            {
                go.transform.Rotate(0f, 0f, 90f);
                if (gameObject.GetComponent<Scout>() != null || gameObject.GetComponent<Healer>() != null || gameObject.GetComponent<MiningDrill>() != null || gameObject.GetComponent<Outpost>() != null)
                {
                    go.transform.Rotate(0f, -90f, 0f);
                }
            }
            if (gameObject.GetComponent<SniperTurret>() != null)
                go.transform.Rotate(0f, 0f, 90f);
            else if (gameObject.GetComponent<Mortar>() != null)
                go.transform.Rotate(0f, 90f, 00f);
            manager.supportSpawners.Add(go);
        }
        manager.numberOfTurrets--;
        manager.currency +=costDeployment / 2;
        manager.currency = Mathf.Floor(manager.currency);

        if (gameObject.GetComponent<Mortar>() != null)
        {
            Destroy(gameObject.GetComponent<Mortar>().mortarTarget);
            gameObject.GetComponent<Mortar>().mortarTarget = null;
            gameObject.GetComponent<Mortar>().target = null;
        }
        if (GameObject.Find("TurretSelector(Clone)") != null)
            Destroy(GameObject.Find("TurretSelector(Clone)"));
        if (GameObject.Find("TurretRanger(Clone)") != null)
            Destroy(GameObject.Find("TurretRanger(Clone)"));
        if (GameObject.FindGameObjectWithTag("Inspector"))
            Destroy(GameObject.FindGameObjectWithTag("Inspector"));
        Destroy(gameObject);
    }
}
