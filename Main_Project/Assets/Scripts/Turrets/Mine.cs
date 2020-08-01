using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public int damage;
    public int explosiveAreaRadius;
    public ParticleSystem explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<Enemy>().detectsMines == false))
        {
            explode();
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("MineDetector"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block")) {
            if (gameObject.GetComponent<Rigidbody>() != null)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                Destroy(rb);
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
        }
    }

    void explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosiveAreaRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy") && collider.gameObject.GetComponent<Enemy>().detectsMines == false)
            {
                collider.gameObject.GetComponent<Enemy>().health -= damage;
            }
        }
    }
}
