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

    //Stealth
    public GameObject stealthEnemy;


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
            if (!manager.isWaveStealthy())
            {
                SpawnEnemy(pickEnemy());
                setTimer();
                timerCooldown = timer;
            }
            else {
                SpawnEnemy(stealthEnemy);
                setTimer();
                timerCooldown = Random.Range(2f, 3f);
            }
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

    void SpawnEnemy(GameObject enemy)
    {
        GameObject go = Instantiate(enemyNavigator, transform.position, transform.rotation);
        GameObject go2 = Instantiate(enemy, transform.position, transform.rotation);
        go.GetComponent<EnemyNavigator>().enemyObject = go2;
        go2.GetComponent<Enemy>().navigator = go;
        numberOfEnemies--;
    }

    int pickEnemy()
    {
        int picker = 0;
        if (manager.currentWave > 0 && manager.currentWave < 4)
        {
            picker = 6;
        }
        else if (manager.currentWave > 3 && manager.currentWave < 8)
        {
            int rand = Random.Range(1, 20);
            if (rand >= 3 && rand <= 8)
            {
                picker = 1;
            }
            else if (rand == 10)
            {
                picker = 3;
            }
            else
            {
                picker = 0;
            }
        }
        else if (manager.currentWave > 7 && manager.currentWave < 13)
        {
            int rand = Random.Range(1, 20);
            if (rand >= 3 && rand <= 5)
            {
                picker = 1;
            }
            else if (rand >= 6 && rand <= 8)
            {
                picker = 2;
            }
            else if (rand >= 9 && rand <= 11)
            {
                picker = 3;
            }
            else if (rand == 14)
            {
                picker = 5;
            }
            else
            {
                picker = 0;
            }
        }
        else if (manager.currentWave > 12 && manager.currentWave < 17)
        {
            int rand = Random.Range(1, 25);
            if (rand >= 3 && rand <= 7)
            {
                picker = 1;
            }
            else if (rand >= 8 && rand <= 12)
            {
                picker = 2;
            }
            else if (rand >= 9 && rand <= 11)
            {
                picker = 3;
            }
            else if (rand == 20 || rand == 21)
            {
                picker = 5;
            }
            else
            {
                picker = 0;
            }
        }
        else if (manager.currentWave > 16 && manager.currentWave < 23)
        {
            int rand = Random.Range(1, 30);
            if (rand >= 3 && rand <= 9)
            {
                picker = 1;
            }
            else if (rand >= 10 && rand <= 12)
            {
                picker = 2;
            }
            else if (rand >= 15 && rand <= 19)
            {
                picker = 3;
            }
            else if (rand >= 22 && rand <= 24)
            {
                picker = 5;
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
        if (manager.isWaveStealthy())
        {
            numberOfEnemies = (int)Random.Range(20, 40);
        }
        else
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
                numberOfEnemies = (int)Random.Range(70, 110);
            }
            else if (manager.currentWave > 9 && manager.currentWave < 15)
            {
                numberOfEnemies = (int)Random.Range(60, 80);
            }
            else if (manager.currentWave > 14 && manager.currentWave < 20)
            {
                numberOfEnemies = (int)Random.Range(130, 170);
            }
            else if (manager.currentWave > 19 && manager.currentWave < 26)
            {
                numberOfEnemies = (int)Random.Range(80, 140);
            }
        }
    }

    void setTimer()
    {
        if (manager.isWaveStealthy())
        {
            timer = Random.Range(1.5f, 3f);
        }
        else
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
                timer = Random.Range(0.5f, 1.1f);
            }
            else if (manager.currentWave > 9 && manager.currentWave < 15)
            {
                timer = Random.Range(0.7f, 5f);
            }
            else if (manager.currentWave > 14 && manager.currentWave < 20)
            {
                timer = Random.Range(0.3f, 1f);
            }
            else if (manager.currentWave > 19 && manager.currentWave < 26)
            {
                timer = Random.Range(0.2f, 1.3f);
            }
        }
    }
}
