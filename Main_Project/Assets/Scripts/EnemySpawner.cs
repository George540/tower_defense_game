using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float timer = 4;
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
            yield return new WaitForSeconds(timer);
        }
    }
}
