using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet
{
    public GameObject explosionArea;
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
            GameObject go = Instantiate(explosionArea, transform.position, Quaternion.identity);
            go.GetComponent<Explosion>().scale = 15f;
            destroyBullet();
        }
    }
}
