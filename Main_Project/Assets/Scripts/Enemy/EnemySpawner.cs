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

    // Boss
    public GameObject bossEnemy;


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
                if (manager.currentWave <= 39)
                    SpawnEnemy(pickEnemy());
                else
                    SpawnEnemy(bossEnemy);
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
        // Wave 1: Prompt player to place the blaster turret
        int picker = 0;
        if (manager.currentWave > 0 && manager.currentWave < 4)
        {
            picker = 0;
        }
        // Wave 3: Prompt player to place the auto turret
        // Wave 4: Warn player to watch out about bigger turrets
        else if (manager.currentWave > 3 && manager.currentWave < 7)
        {
            int rand = Random.Range(1, 20);
            if (rand >= 3 && rand <= 8)
            {
                picker = 1;
            }
            else
            {
                picker = 0;
            }
        }
        // Wave 7: Warn player to watch out for stealthy waves
        // Wave 8: Warn player to watch out for flankers and their carriers
        else if (manager.currentWave > 6 && manager.currentWave < 13)
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
            else if (rand == 14)
            {
                picker = 5;
            }
            else
            {
                picker = 0;
            }
        }
        // Wave 13: Warn player about repair hovercrafts
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
            else if (rand >= 13 && rand <= 15)
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
        // Wave 17: Warn player about ramping up the armored hovercrafts
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
        // Wave 23: Warn player about hovercrafts being armed and have already healers up
        else if (manager.currentWave > 22 && manager.currentWave < 28)
        {
            int rand = Random.Range(1, 30);
            if (rand >= 3 && rand <= 15)
            {
                picker = 1;
            }
            else if (rand >= 12 && rand <= 16)
            {
                picker = 2;
            }
            else if (rand >= 17 && rand <= 20)
            {
                picker = 3;
            }
            else if (rand >= 22 && rand <= 25)
            {
                picker = 5;
            }
            else if (rand == 28 || rand == 29)
            {
                picker = 6;
            }
            else
            {
                picker = 0;
            }
        }
        // Wave 28: Warn player about miners sweeping through minefields
        else if (manager.currentWave > 27 && manager.currentWave < 34)
        {
            int rand = Random.Range(1, 30);
            if (rand >= 3 && rand <= 10)
            {
                picker = 1;
            }
            else if (rand >= 11 && rand <= 14)
            {
                picker = 2;
            }
            else if (rand >= 15 && rand <= 18)
            {
                picker = 3;
            }
            else if (rand >= 18 && rand <= 22)
            {
                picker = 5;
            }
            else if (rand == 23 || rand == 24)
            {
                picker = 4;
            }
            else if (rand >= 25 || rand <= 27)
            {
                picker = 6;
            }
            else if (rand == 28)
            {
                picker = 7;
            }
            else
            {
                picker = 0;
            }
        }
        // Wave 34: Warn player about destroyers and stealthy hovercrafts
        else if (manager.currentWave > 33)
        {
            picker = Random.Range(0, enemies.Length);
        }
        return picker;
        // Wave 40: Boss wave. Warn player about spawning backups and having great defense
    }

    void setNumberOfEnemies()
    {
        if (manager.isWaveStealthy())
        {
            numberOfEnemies = Random.Range(20, 40);
        }
        else
        {
            if (manager.currentWave == 1)
            {
                numberOfEnemies = Random.Range(10, 16);
            }
            else if (manager.currentWave == 2)
            {
                numberOfEnemies = Random.Range(20, 30);
            }
            else if (manager.currentWave > 2 && manager.currentWave < 5)
            {
                numberOfEnemies = Random.Range(40, 60);
            }
            else if (manager.currentWave > 4 && manager.currentWave < 10)
            {
                numberOfEnemies = Random.Range(70, 110);
            }
            else if (manager.currentWave > 9 && manager.currentWave < 15)
            {
                if (manager.currentWave >= 11 && manager.currentWave < 14)
                    numberOfEnemies = Random.Range(30, 60);
                else
                    numberOfEnemies = Random.Range(60, 80);
            }
            else if (manager.currentWave > 14 && manager.currentWave < 20)
            {

                if (manager.currentWave == 18)
                    numberOfEnemies = Random.Range(60, 90);
                else
                    numberOfEnemies = Random.Range(130, 170);
            }
            else if (manager.currentWave > 19 && manager.currentWave < 26)
            {
                numberOfEnemies = Random.Range(40, 90);
            }
            else if (manager.currentWave > 25 && manager.currentWave < 32)
            {
                if (manager.currentWave == 28)
                    numberOfEnemies = Random.Range(30, 60);
                else
                    numberOfEnemies = Random.Range(80, 120);
            }
            else if (manager.currentWave > 31 && manager.currentWave < 38)
            {
                numberOfEnemies = Random.Range(80, 100);
            }
            else if (manager.currentWave == 38 || manager.currentWave == 39)
            {
                numberOfEnemies = Random.Range(120, 140);
            }
            else if (manager.currentWave == 40)
            {
                numberOfEnemies = 1;
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
            else if (manager.currentWave > 2 && manager.currentWave < 6)
            {
                if (manager.currentWave == 4)
                    timer = 0.3f;
                else
                    timer = Random.Range(0.8f, 2f);
            }
            else if (manager.currentWave > 5 && manager.currentWave < 11)
            {
                timer = Random.Range(0.4f, 1.0f);
            }
            else if (manager.currentWave > 10 && manager.currentWave < 15)
            {
                timer = Random.Range(0.7f, 5f);
            }
            else if (manager.currentWave > 14 && manager.currentWave < 20)
            {
                timer = Random.Range(0.3f, 0.8f);
            }
            else if (manager.currentWave > 19 && manager.currentWave < 26)
            {
                timer = Random.Range(0.05f, 1.3f);
            }
            else if (manager.currentWave > 25 && manager.currentWave < 32)
            {
                timer = Random.Range(0.05f, 0.9f);
            }
            else if (manager.currentWave > 31 && manager.currentWave < 38)
            {
                if(manager.currentWave == 35)
                    timer = Random.Range(0.01f, 0.04f);
                else
                    timer = Random.Range(0.08f, 1.5f);
            }
            else if (manager.currentWave == 38 || manager.currentWave == 39)
            {
                timer = Random.Range(0.01f, 0.03f);
            }
            else if (manager.currentWave == 40)
            {
                timer = Random.Range(4f, 7f);
            }
        }
    }
}
