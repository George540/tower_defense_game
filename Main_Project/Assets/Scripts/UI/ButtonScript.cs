using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    Text buttonText;
    // Start is called before the first frame update
    void Start()
    {
        buttonText = transform.GetComponentInChildren<Text>();
        buttonText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
