using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public Transform target;
    public float range;
    public Transform partToRotate;
    public Transform partToRotate2;
    public float turnSpeed;

    public GameObject bullet;
    public float fireRate;
    public float fireCountdown;
    public bool isAimingAtTarget;
    public bool isRangeIncreased;

    public GameObject nozzle;

    private string[] tags = new[] { "OffenseTurret", "SupportTurret" };
    void Start()
    {
        
    }

    public void UpdateTarget()
    {
        GameObject[] enemies = FindObjectWithTags(tags);

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

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }

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
        if (target.CompareTag(tags[0]) || target.CompareTag(tags[1]))
        {
            fireCountdown -= Time.deltaTime;
            FindTarget(target);
        }

        if (fireCountdown <= 0f && target != null && isAimingAtTarget == true && (target.CompareTag(tags[0]) || target.CompareTag(tags[1])))
        {
            Fire();
            fireCountdown = 1.0f / fireRate;
        }
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

    void Fire()
    {
        GameObject bul = Instantiate(bullet, nozzle.transform.position, nozzle.transform.rotation * Quaternion.Euler(0f, 0f, -90f));
        bul.GetComponent<Rigidbody>().velocity = nozzle.transform.right * bul.GetComponent<EnemyBullet>().getBulletSpeed();
    }

    GameObject[] FindObjectWithTags(IEnumerable<string> tags)
    {
        return FindObjectsOfType(typeof(GameObject)).Cast<GameObject>().Where(go => tags.Contains(go.tag)).ToArray();
    }

    void isTargeting()
    {
        Debug.DrawRay(nozzle.transform.position, nozzle.transform.right * range, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(nozzle.transform.position, nozzle.transform.right, out hit, range))
        {
            if (hit.transform.CompareTag(tags[0]) || hit.transform.CompareTag(tags[1]))
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
