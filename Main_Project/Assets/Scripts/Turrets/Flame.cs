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
        
    }

    public int getFlameDamage()
    {
        return flameDamage;
    }
}
