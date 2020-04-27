using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            GameObject go = Instantiate(enemy, transform.position, transform.rotation);
            go.gameObject.tag = "Enemy";
            go.gameObject.transform.localScale = new Vector3(1.0f, Random.Range(0.5f, 2.0f), 1.0f);
            timer = Random.Range(1, 4);
            yield return new WaitForSeconds(timer);
        }
    }
}
