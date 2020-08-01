using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealerBoard : TurretBoard
{

    void Update()
    {
        setSpecsHealer();
    }

    void setSpecsHealer()
    {
        if (turret != null)
        {
            transform.GetChild(1).gameObject.GetComponent<Text>().text = turret.GetComponent<Turret>().turretName;
            transform.GetChild(2).gameObject.GetComponent<Text>().text = "(Support)";
            transform.GetChild(3).gameObject.GetComponent<Text>().text = turret.GetComponent<Turret>().health.ToString();
            transform.GetChild(4).gameObject.GetComponent<Text>().text = "Range: " + turret.GetComponent<Turret>().range.ToString();
            transform.GetChild(5).gameObject.GetComponent<Text>().text = "Heal: " + turret.GetComponent<Healer>().health;
        }
    }
}
