using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float damage;
    public float lifespan;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyBullet", lifespan);
    }

    public float getBulletSpeed()
    {
        return bulletSpeed;
    }

    public void setBulletSpeed(float speed)
    {
        bulletSpeed = speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<Enemy>().health -= damage;
        destroyBullet();
    }


    protected void destroyBullet()
    {
        Destroy(gameObject);
    }
}
