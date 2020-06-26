using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerEnemy : Enemy
{
    public GameObject healVeil;
    public float range;
    public float healRate;
    public float healingRate;
    private float healingCountdown;

    // Update is called once per frame
    void Update()
    {
        transform.position = navigator.transform.position;
        rotate();

        checkHealth();
        spinHalo();
        
        if (healingCountdown <= 0)
        {
            healNearby();
            healingCountdown = healingRate;
        }
        healingCountdown -= Time.deltaTime;
    }

    void spinHalo()
    {
        Transform halo = healVeil.transform;
        halo.Rotate(new Vector3(0, 0, 1) * 200 * Time.deltaTime * Time.timeScale);
    }

    void healNearby()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy.GetComponent<Enemy>() != null && Vector3.Distance(transform.position, enemy.transform.position) <= range)
                {
                    enemy.GetComponent<Enemy>().health += healRate;
                }
            }
        }

        if (health < healthMax)
        {
            health += healRate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
