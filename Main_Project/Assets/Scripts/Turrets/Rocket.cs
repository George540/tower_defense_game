using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet
{
    public float explosiveAreaRadius;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyBullet", lifespan);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            explode();
            destroyBullet();
        }
    }

    void explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosiveAreaRadius);
        foreach(Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().health -= damage;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosiveAreaRadius);
    }
}
