using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Manager manager;
    public GameObject navigator;
    public float turnSpeed;
    public float moveSpeed;
    public float health;
    protected float healthMax;
    public float currencyDrop;

    public int pushDamage;
    public bool isStealthy;
    public bool detectsMines;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
        healthMax = health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = navigator.transform.position;
        rotate();

        checkHealth();
    }

    protected void rotate()
    {
        Vector3 rotation = Quaternion.Lerp(transform.rotation, navigator.transform.rotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, rotation.z);
    }

    protected void checkHealth()
    {
        if (health <= 0)
        {
            manager.currency += currencyDrop;
            if (healthMax <= 6000 && GetComponent<AudioSource>() != null)
            {
                GetComponent<AudioSource>().Play();
                manager.setVictory();
            }
            Destroy(navigator);
            Destroy(gameObject);
        }
        else if (health > healthMax)
        {
            health = healthMax;
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
