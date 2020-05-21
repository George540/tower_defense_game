using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject navigator;
    public float turnSpeed;
    public float health = 100;
    public float currencyDrop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = navigator.transform.position;
        rotate();

        checkHealth();
    }

    void rotate()
    {
        Vector3 rotation = Quaternion.Lerp(transform.rotation, navigator.transform.rotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void checkHealth()
    {
        if (health <= 0)
        {
            Destroy(navigator);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Explosion"))
        {
            health -= 500f;
        }
    }
}
