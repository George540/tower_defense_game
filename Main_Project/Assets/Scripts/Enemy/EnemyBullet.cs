using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
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
        if (collision.gameObject.CompareTag("OffenseTurret") || collision.gameObject.CompareTag("SupportTurret"))
            collision.gameObject.GetComponent<Turret>().health -= (int)damage;
        if (collision.gameObject.CompareTag("Bullet"))
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        destroyBullet();
    }


    protected void destroyBullet()
    {
        Destroy(gameObject);
    }
}
