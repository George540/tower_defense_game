using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyBullet", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getBulletSpeed()
    {
        return bulletSpeed;
    }

    public void setBulletSpeed(float speed)
    {
        bulletSpeed = speed;
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        destroyBullet();
        if (other.gameObject.CompareTag("Enemy"))
            other.GetComponent<Enemy>().health -= 50;
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<Enemy>().health -= damage;
        destroyBullet();
    }


    void destroyBullet()
    {
        Destroy(gameObject);
    }
}
