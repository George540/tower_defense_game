using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployableIsland : Ground
{
    public GameObject[] turretSpawnEmpties = new GameObject[4];
    public GameObject supportIndicator;
    public GameObject[] vegetation = new GameObject[4];

    public int rand1;
    public int rand2;
    public int rand3;
    // Start is called before the first frame update
    void Start()
    {
        if (index == 7)
        {
            rand1 = Random.Range(0, turretSpawnEmpties.Length / 2);
            rand2 = Random.Range(turretSpawnEmpties.Length / 2, turretSpawnEmpties.Length);

            spawnTurretIndicator(rand1);
            spawnTurretIndicator(rand2);

            do
            {
                rand3 = Random.Range(0, turretSpawnEmpties.Length);
            } while (rand3 == rand1 || rand3 == rand2);

            int chance = Random.Range(0, 3);
            if (chance == 1)
                spawnVegetation(rand3);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnTurretIndicator(int i)
    {
        Instantiate(supportIndicator, turretSpawnEmpties[i].transform.position, turretSpawnEmpties[i].transform.rotation);
    }

    void spawnVegetation(int i)
    {
        int rand = Random.Range(0, vegetation.Length);
        if (rand == 3 && rand == 4)
            Instantiate(vegetation[rand], turretSpawnEmpties[rand3].transform.position, UnityEngine.Quaternion.Euler(-90, 0, Random.Range(0, 360)));
        else
            Instantiate(vegetation[rand], turretSpawnEmpties[rand3].transform.position + new UnityEngine.Vector3(0, -2f, 0), UnityEngine.Quaternion.Euler(-90, 0, Random.Range(0, 360)));
    }
}
