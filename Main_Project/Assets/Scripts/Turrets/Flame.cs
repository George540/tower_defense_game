using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public FlameTurret turret;
    public int flameDamage;
    void Start()
    {
        flameDamage = turret.fireDamage;
    }

    void Update()
    {
        checkDamage();
        var main = GetComponent<ParticleSystem>().main;
        if (Time.timeScale == 1)
        {
            main.startSize = 5;
        }
        else if (Time.timeScale > 1)
        {
            main.startSize = 6;
        }
    }

    public int getFlameDamage()
    {
        return flameDamage;
    }

    void checkDamage()
    {
        if (Time.timeScale != 1)
            flameDamage = (int)(Time.timeScale);
        else
            flameDamage = turret.fireDamage;
    }
}
