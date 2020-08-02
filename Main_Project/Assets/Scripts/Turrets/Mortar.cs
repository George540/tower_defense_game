using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : Turret
{
    public GameObject mortarTarget;
    public GameObject nozzle;
    public Material[] materials = new Material[2];
    public int damage;
    private Camera cam;

    private ParticleSystem mortarFire;
    public ParticleSystem explosion;
    public float explosionPositionRadius;
    public float explosiveAreaRadius;

    // Start is called before the first frame update
    void Start()
    {
        mortarFire = transform.GetChild(1).GetComponent<ParticleSystem>();
        manager = FindObjectOfType<Manager>();
        health = maxHealth;
        cam = GameObject.Find("CameraTranslator").transform.GetChild(0).GetChild(0).GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isWavePlaying)
            checkMortarFire();
        if (target != null && target.GetComponent<MortarTarget>().isTargetSet)
            FindTarget();

        if (fireCountdown <= 0 && isAimingAtTarget)
        {
            Fire();
            fireCountdown = fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    public void FindTarget()
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed * Time.timeScale).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if ((transform.rotation.y - lookRotation.eulerAngles.y) <= System.Math.Abs(3))
        {
            isAimingAtTarget = true;
        }
        else
        {
            isAimingAtTarget = false;
        }
    }

    override public void Fire()
    {
        if (manager.isWavePlaying && target.GetComponent<MortarTarget>().isTargetSet)
        {
            mortarFire.Play();
            float tempTime = fireRate;
            Invoke("createExplosion", 2f);
        }
    }

    void checkMortarFire()
    {
        transform.GetChild(1).position = nozzle.transform.position;
        transform.GetChild(1).rotation = nozzle.transform.rotation * Quaternion.Euler(-90f, 0f, 0f);
    }

    void createExplosion()
    {
        float randX = Random.Range(-explosionPositionRadius, explosionPositionRadius);
        float randZ = Random.Range(-explosionPositionRadius, explosionPositionRadius);
        Collider[] colliders = Physics.OverlapSphere(mortarTarget.transform.position + new Vector3(randX, 0f, randZ), explosiveAreaRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().health -= damage;
            }
        }
        Instantiate(explosion, mortarTarget.transform.position + new Vector3(randX, 0f, randZ), Quaternion.identity);
    }
}
