using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    // Starting buttons
    public GameObject startButton;
    private bool isGamePaused;
    public float chronoScale;
    public GameObject tipBoard;

    // STEALTH WAVES
    public int[] stealthWaves = new int[5];

    // GAME OVER
    private bool canGameOver;
    private bool hasSongPlayed;
    public GameObject gameOverBG;
    public GameObject gameOverText;
    public GameObject gameOverButton;



    // Start is called before the first frame update
    void Start()
    {
        chronoScale = Time.timeScale;
        isGamePaused = false;
        hasSongPlayed = false;
        startButton = GameObject.Find("Canvas").transform.Find("StartWave").gameObject;
        Invoke("spawnIslandCounter", 0.5f);
        Invoke("setWaves", 0.5f);
        Invoke("setHealth", 1.0f);
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
        if (startButton.GetComponent<StartWave>().getisWavePlaying() == false && enemies.Length == 0 && currentWave <= 40)
        {
            var tempColor = startButton.GetComponent<Button>().GetComponent<Image>().color;
            tempColor.a = 1f;
            startButton.GetComponent<Button>().GetComponent<Image>().color = tempColor;
            startButton.GetComponent<Button>().interactable = true;
            startButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = "START WAVE";
            if (tipBoard.GetComponent<TipBoard>().isTipWave)
            {
                tipBoard.GetComponent<Image>().enabled = true;
                tipBoard.transform.GetChild(0).GetComponent<Text>().enabled = true;
            }
            isWavePlaying = false;
            giveExtraCredits();
        }

        if (totalBaseHealth <= 0)
        {
            totalBaseHealth = 0;
            if (canGameOver == true)
            {
                setGameOver();
            }
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
        if (terminals.Count > 0)
            totalBaseHealth = baseHealth * terminals.Count;
        else
            totalBaseHealth = 100f;

        canGameOver = true;
    }

    void setWaves()
    {
        numberOfWaves = 40;
        /*
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
        */

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

    private void setGameOver()
    {
        if (gameOverBG.GetComponent<AudioSource>().isPlaying == false && hasSongPlayed == false)
        {
            gameOverBG.GetComponent<AudioSource>().Play();
            hasSongPlayed = true;
        }
            
        gameOverText.GetComponent<TextMeshProUGUI>().text = "GAME OVER";
        gameOverButton.GetComponent<Image>().enabled = true;
        gameOverButton.GetComponent<Button>().enabled = true;
        gameOverButton.transform.GetChild(0).GetComponent<Text>().enabled = true;
        gameOverBG.GetComponent<Image>().enabled = true;
    }

    public void setVictory()
    {
        gameOverBG.GetComponent<Image>().enabled = true;
        if (gameOverText.GetComponent<AudioSource>().isPlaying == false && hasSongPlayed == false)
        {
            gameOverText.GetComponent<AudioSource>().Play();
            hasSongPlayed = true;
        }
        gameOverText.GetComponent<TextMeshPro>().text = "VICTORY!";
        gameOverButton.GetComponent<Image>().enabled = true;
        gameOverButton.GetComponent<Button>().enabled = true;
        gameOverText.transform.GetChild(0).GetComponent<Text>().enabled = true;
    }
}
