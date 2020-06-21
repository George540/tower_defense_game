using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Manager manager;
    public GameObject navigator;
    public float turnSpeed;
    public float moveSpeed;
    public float health = 100;
    public float currencyDrop;

    public FlameTurret flamethrower;
    public int pushDamage;
    public bool isStealthy;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
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
            manager.currency += currencyDrop;
            Destroy(navigator);
            Destroy(gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Flame"))
        {
            health -= other.gameObject.GetComponent<Flame>().getFlameDamage();
        }
    }
}
