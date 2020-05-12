using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyNavigator;
    public GameObject enemy;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        GameObject go = Instantiate(enemyNavigator, transform.position, transform.rotation);
        GameObject go2 = Instantiate(enemy, transform.position, transform.rotation);
        go.GetComponent<EnemyNavigator>().enemyObject = go2;
        go2.GetComponent<Enemy>().navigator = go;
        go2.GetComponent<Enemy>().turnSpeed = 15f;
        go2.gameObject.transform.localScale = new Vector3(1.0f, Random.Range(0.5f, 4.0f), 1.0f);
        //go.GetComponent<EnemyNavigator>().speed = Random.Range(10, 30);
        go.GetComponent<EnemyNavigator>().speed = 20;
        timer = Random.Range(3, 4);
    }
}
