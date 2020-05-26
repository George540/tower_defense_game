using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class StartWave : MonoBehaviour
{
    public Manager manager;
    public GameObject enemySpawner;
    private bool isWavePlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void start()
    {
        setisWavePlaying(true);
        gameObject.SetActive(false);
        if (manager.currentWave < manager.numberOfWaves)
        {
            Instantiate(enemySpawner, new Vector3(-60f, 15.5f, 0f), Quaternion.identity);
            manager.currentWave++;
        }
    }

    public bool getisWavePlaying()
    {
        return isWavePlaying;
    }

    public void setisWavePlaying(bool started)
    {
        isWavePlaying = started;
    }
}
