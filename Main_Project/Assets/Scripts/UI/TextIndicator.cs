using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextIndicator : MonoBehaviour
{
    public Text currencyText;
    public Text baseHealth;
    public Text waveNumber;
    public Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isMapCreated == true)
        {
            currencyText.GetComponent<Text>().text = "Currency: " + manager.currency.ToString();
            baseHealth.GetComponent<Text>().text = "Health: " + manager.totalBaseHealth.ToString() + " (" + manager.terminals.Count.ToString() + ")";
            waveNumber.GetComponent<Text>().text = "Wave: " + manager.currentWave.ToString() + "/" + manager.numberOfWaves.ToString();
        }
    }
}
