using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextIndicator : MonoBehaviour
{
    public Text currencyText;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currencyText.GetComponent<Text>().text = "Currency: " + manager.GetComponent<Manager>().currency.ToString();
    }
}
