using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    public Manager manager;
    public float timer;
    public GameObject fastButton;
    public GameObject normalButton;
    public GameObject pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        //manager = GameObject.Find("MANAGER").GetComponent<Manager>();
        //fastButton = GameObject.Find("SpeedUp");
        //normalButton = GameObject.Find("Normal");
        //pauseButton = GameObject.Find("Pause");
        normalButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setTimer()
    {
        if (fastButton.activeInHierarchy == true)
        {
            fastButton.SetActive(false);
            normalButton.SetActive(true);
        }
        else if (normalButton.activeInHierarchy == true)
        {
            normalButton.SetActive(false);
            fastButton.SetActive(true);
        }
        Time.timeScale = timer;
    }

    public void setFast()
    {
        fastButton.SetActive(false);
        normalButton.SetActive(true);
        Time.timeScale = 5;
    }
    public void setNormal()
    {
        normalButton.SetActive(false);
        fastButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void setPause()
    {
        manager.setPaused(true);
        if (fastButton.activeInHierarchy == true)
        {
            fastButton.SetActive(false);
            normalButton.SetActive(true);
        }
        Time.timeScale = 0;
    }
}
