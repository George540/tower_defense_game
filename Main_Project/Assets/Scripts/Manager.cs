﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public bool isMapCreated = false;
    public List<GameObject> grounds = new List<GameObject>();
    public List<GroundSpawner> groundSpawners = new List<GroundSpawner>();
    public List<Waypoint> waypoints = new List<Waypoint>();
    public List<GameObject> terminals = new List<GameObject>();

    public List<GameObject> turretSpawners = new List<GameObject>();
    public List<GameObject> supportSpawners = new List<GameObject>();
    public bool isBuilding = false;

    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float maxZ;
    public float minZ;

    public GameObject currentTurret;
    public float currency;
    public int numberOfTurrets;
    public float baseHealth = 50;
    public float totalBaseHealth;

    public int numberOfWaves;
    public int currentWave = 0;
    public bool isWavePlaying;
    public float extraCredits;
    public bool isExtraGiven;

    // Random Island Stuff
    public GameObject islandCounter;
    public float medianX;
    public float medianY;
    public float medianZ;

    public GameObject startButton;
    private bool isGamePaused;
    public float chronoScale;

    // STEALTH WAVES
    public int[] stealthWaves = new int[5];
    // Start is called before the first frame update
    void Start()
    {
        chronoScale = Time.timeScale;
        isGamePaused = false;
        startButton = GameObject.Find("Canvas").transform.Find("StartWave").gameObject;
        Invoke("spawnIslandCounter", 0.5f);
        Invoke("setHealth", 0.5f);
        Invoke("setWaves", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (groundSpawners.Count == 0)
        {
            isMapCreated = true;
        }
        if (isMapCreated == true)
        {
            setBoundaries();
            setMedians();
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (startButton.GetComponent<StartWave>().getisWavePlaying() == false && enemies.Length == 0)
        {
            var tempColor = startButton.GetComponent<Button>().GetComponent<Image>().color;
            tempColor.a = 1f;
            startButton.GetComponent<Button>().GetComponent<Image>().color = tempColor;
            startButton.GetComponent<Button>().interactable = true;
            startButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = "START WAVE";
            isWavePlaying = false;
            giveExtraCredits();
        }
    }

    void setBoundaries()
    {
        setX();
        setY();
        setZ();
    }

    void setX()
    {
        maxX = grounds[0].transform.position.x;
        minX = grounds[0].transform.position.x;
        foreach (GameObject ground in grounds)
        {
            if (ground.transform.position.x > maxX)
                maxX = ground.transform.position.x;
            if (ground.transform.position.x < minX)
                minX = ground.transform.position.x;
        }
    }

    void setY()
    {
        maxY = grounds[0].transform.position.y;
        minY = grounds[0].transform.position.y;
        foreach (GameObject ground in grounds)
        {
            if (ground.transform.position.y > maxY)
                maxY = ground.transform.position.y;
            if (ground.transform.position.y < minY)
                minY = ground.transform.position.y;
        }
    }

    void setZ()
    {
        maxZ = grounds[0].transform.position.z;
        minZ = grounds[0].transform.position.z;
        foreach (GameObject ground in grounds)
        {
            if (ground.transform.position.z > maxZ)
                maxZ = ground.transform.position.z;
            if (ground.transform.position.z < minZ)
                minZ = ground.transform.position.z;
        }
    }

    void setHealth()
    {
        totalBaseHealth = baseHealth * terminals.Count;
    }

    void setWaves()
    {
        if (terminals.Count <= 1)
        {
            numberOfWaves = 20;
        }
        else if (terminals.Count > 1 && terminals.Count < 3)
        {
            numberOfWaves = 30;
        }
        else
        {
            numberOfWaves = 40;
        }

        for (int i = 0; i < stealthWaves.Length; i++)
        {
            stealthWaves[i] = Random.Range(8, numberOfWaves + 4);
        }
    }

    void setMedians()
    {
        float sumX = 0;
        float sumY = 0;
        float sumZ = 0;

        foreach (GameObject ground in grounds)
        {
            sumX += ground.transform.position.x;
            sumY += ground.transform.position.y;
            sumZ += ground.transform.position.z;
        }

        medianX = sumX / grounds.Count;
        medianY = sumY / grounds.Count;
        medianZ = sumZ / grounds.Count;
    }

    void spawnIslandCounter()
    {
        Instantiate(islandCounter, new Vector3(medianX, medianY, medianZ), transform.rotation);
    }

    public bool isPaused()
    {
        return isGamePaused;
    }

    public void setPaused(bool paused)
    {
        isGamePaused = paused;
    }

    void giveExtraCredits()
    {
        if (isExtraGiven == false)
        {
            currency += extraCredits;
            isExtraGiven = true;
        }
    }

    public bool isWaveStealthy()
    {
        for (int i = 0; i < stealthWaves.Length; i++)
        {
            if (currentWave == stealthWaves[i])
                return true;
        }
        return false;
    }
}
