using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : MonoBehaviour
{
    public int health;
    public int damage;

    void Update()
    {
        checkHealth();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().health -= damage;
            health -= other.GetComponent<Enemy>().pushDamage;
        }
    }

    void checkHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
