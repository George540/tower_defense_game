using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outpost : Turret
{
    public Transform antena;
    public int divisor;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("increaseRange", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        antena.Rotate(0f, 0f, 1f * Time.timeScale);
    }

    public void increaseRange()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("OffenseTurret");

        // Finding nearest target
        foreach (GameObject turret in turrets)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, turret.transform.position);
            if (distanceToEnemy < range && turret.GetComponent<Turret>().isRangeIncreased == false)
            {
                turret.GetComponent<Turret>().range += range / divisor;
                turret.GetComponent<Turret>().isRangeIncreased = true;
            }
        }
    }
}
