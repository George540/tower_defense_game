using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class StartWave : MonoBehaviour
{
    public Manager manager;
    public GameObject enemySpawner;
    public GameObject tipBoard;
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
        var tempColor = GetComponent<Button>().GetComponent<Image>().color;
        tempColor.a = 0f;
        GetComponent<Button>().GetComponent<Image>().color = tempColor;
        GetComponent<Button>().interactable = false;
        transform.GetChild(0).GetComponent<Text>().text = "";
        manager.isWavePlaying = true;
        manager.isExtraGiven = false;
        if (manager.currentWave < manager.numberOfWaves)
        {
            Instantiate(enemySpawner, new Vector3(-120f, 15.5f, 0f), Quaternion.identity);
            manager.currentWave++;
        }

        GameObject[] mines = GameObject.FindGameObjectsWithTag("Mine");
        foreach (GameObject mine in mines)
        {
            Destroy(mine);
        }

        tipBoard.GetComponent<Image>().enabled = false;
        tipBoard.transform.GetChild(0).GetComponent<Text>().enabled = false;
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
