using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipBoard : MonoBehaviour
{
    private Manager manager;
    private Text textBox;
    public bool isTipWave;

    void Start()
    {
        manager = FindObjectOfType<Manager>();
        textBox = transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        checkVisibility();
        updateText();
    }

    void updateText()
    {
        if (manager.currentWave > 1)
        {
            if (manager.currentWave == 2)
            {
                textBox.text = "They are ramping up the speed! An Auto Blaster can help kill them faster";
            }
            else if (manager.currentWave == 3)
            {
                textBox.text = "Our intelligence agency uncovered information of larger vehicles being sent. We must ramp up our defense!";
            }
            else if (manager.currentWave == 4)
            {
                textBox.text = "Some of them are getting through! If deploying a more powerful turret it not an option, mines and hedgehogs can come in handy as a last resort.";
            }
            else if (manager.currentWave == 6)
            {
                textBox.text = "More foes have appeared! Looks like they deployed fast Flankers and their armored Carriers. We must set up the explosives!";
            }
            else if (manager.currentWave == 7)
            {
                textBox.text = "A top secret vehicle has been spotted in the upcomming waves. It appears to have stealth technology, making it undetectable by most of our turrets. They may catch us by surprise.";
            }
            else if (manager.currentWave == 12)
            {
                textBox.text = "It appears they have realized their fatality rates. They deployed Mobile Repair Bots to move past our defenses.";
            }
            else if (manager.currentWave == 16)
            {
                textBox.text = "They are sending bigger vehicles in faster rates. We must increase our Support Stations and production efforts.";
            }
            else if (manager.currentWave == 22)
            {
                textBox.text = "Emergency Alert! Armed Hovercrafts have been spotted. Deploy more Repair Stations! It would be a great time to have multiple defense hotspots.";
            }
            else if (manager.currentWave == 27)
            {
                textBox.text = "They detected our Mine Fields! They are sending modified Mine Sweepers to absorb the mines. Do not let them reach them.";
            }
            else if (manager.currentWave == 32)
            {
                textBox.text = "Looks like they are sending everything they have left at once. Be ready to spawn new turrets in a moment's notice.";
            }
            else if (manager.currentWave == 33)
            {
                textBox.text = "Destroyers with massive cannons have been spotted! Flames and Explosives should pin them down.";
            }
            else if (manager.currentWave == 39)
            {
                textBox.text = "Our radars have detected a massive vehicle comming in! It has multiple turrets and cannons that can break through our defense! Deploy everything we have!";
            }
            else
            {
                GetComponent<Image>().enabled = false;
                textBox.enabled = false;
            }
        }
    }

    void checkVisibility()
    {
        if (manager.currentWave > 1)
        {
            if (manager.currentWave == 2 || manager.currentWave == 3 || manager.currentWave == 4 || manager.currentWave == 6 || manager.currentWave == 7 || manager.currentWave == 12 ||
                manager.currentWave == 16 || manager.currentWave == 22 || manager.currentWave == 27 || manager.currentWave == 32 || manager.currentWave == 33 || manager.currentWave == 39)
            {
                isTipWave = true;
            }
            else
            {
                isTipWave = false;
            }
        }
    }
}
