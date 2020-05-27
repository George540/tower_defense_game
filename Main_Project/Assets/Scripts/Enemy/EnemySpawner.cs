using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public Manager manager;
    public GameObject enemyNavigator;
    public GameObject[] enemies;
    public GameObject startButton;

    public float timer;
    public float timerCooldown;

    public int numberOfEnemies;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
        startButton = GameObject.Find("Canvas").transform.Find("StartWave").gameObject;
        timer = 3;
        setNumberOfEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfEnemies == 0)
        {
            startButton.gameObject.GetComponent<StartWave>().setisWavePlaying(false);
            Destroy(gameObject);
        }
        if (timerCooldown <= 0)
        {
            SpawnEnemy(pickEnemy());
            setTimer();
            timerCooldown = timer;
        }
        timerCooldown -= Time.deltaTime;
    }

    void SpawnEnemy(int index)
    {
        GameObject go = Instantiate(enemyNavigator, transform.position, transform.rotation);
        GameObject go2 = Instantiate(enemies[index], transform.position, transform.rotation);
        go.GetComponent<EnemyNavigator>().enemyObject = go2;
        go2.GetComponent<Enemy>().navigator = go;
        numberOfEnemies--;
    }

    int pickEnemy()
    {
        int picker = 0;
        if (manager.currentWave > 0 && manager.currentWave < 4)
        {
            picker = 0;
        }
        else if (manager.currentWave > 3 && manager.currentWave < 8)
        {
            int rand = Random.Range(1, 11);
            if (rand >= 3 && rand <= 5)
            {
                picker = 1;
            }
            else
            {
                picker = 0;
            }
        }
        else if (manager.currentWave > 7 && manager.currentWave < 16)
        {
            int rand = Random.Range(1, 11);
            if (rand >= 3 && rand <= 5)
            {
                picker = 1;
            }
            else if (rand >= 6 && rand <= 8)
            {
                picker = 2;
            }
            else
            {
                picker = 0;
            }
        }
        else
        {
            picker = Random.Range(0, enemies.Length);
        }
        return picker;
    }

    void setNumberOfEnemies()
    {
        if (manager.currentWave == 1)
        {
            numberOfEnemies = (int)Random.Range(10, 16);
        }
        else if (manager.currentWave == 2)
        {
            numberOfEnemies = (int)Random.Range(20, 30);
        }
        else if (manager.currentWave > 2 && manager.currentWave < 5)
        {
            numberOfEnemies = (int)Random.Range(40, 60);
        }
        else if (manager.currentWave > 4 && manager.currentWave < 10)
        {
            numberOfEnemies = (int)Random.Range(70, 100);
        }
        else if (manager.currentWave > 9 && manager.currentWave < 15)
        {
            numberOfEnemies = (int)Random.Range(100, 130);
        }
        else if (manager.currentWave > 14 && manager.currentWave < 20)
        {
            numberOfEnemies = (int)Random.Range(130, 170);
        }
        else if (manager.currentWave > 19 && manager.currentWave < 26)
        {
            numberOfEnemies = (int)Random.Range(140, 170);
        }
    }

    void setTimer()
    {
        if (manager.currentWave <= 2)
        {
            timer = 3;
        }
        else if (manager.currentWave > 2 && manager.currentWave < 5)
        {
            timer = Random.Range(0.8f, 2f);
        }
        else if (manager.currentWave > 4 && manager.currentWave < 10)
        {
            timer = Random.Range(0.6f, 1.5f);
        }
        else if (manager.currentWave > 9 && manager.currentWave < 15)
        {
            timer = Random.Range(0.7f, 5f);
        }
        else if (manager.currentWave > 14 && manager.currentWave < 20)
        {
            timer = Random.Range(0.4f, 1f);
        }
        else if (manager.currentWave > 19 && manager.currentWave < 26)
        {
            timer = Random.Range(0.2f, 1.3f);
        }
    }
}
