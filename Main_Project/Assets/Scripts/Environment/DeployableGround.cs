using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class DeployableGround : Ground
{
    public Manager manager;
    public GameObject[] turretSpawnEmpties = new GameObject[6];
    public GameObject offenseIndicator;
    public GameObject[] vegetation = new GameObject[4];
    public GameObject[] secondaryVegetation = new GameObject[4];

    public int rand1;
    public int rand2;
    public int rand3;
    public int rand4;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
        if (index == 1)
        {
            rand1 = Random.Range(0, turretSpawnEmpties.Length/2);
            rand2 = Random.Range(turretSpawnEmpties.Length/2, turretSpawnEmpties.Length);

            spawnTurretIndicator(rand1);
            spawnTurretIndicator(rand2);

            do
            {
                rand3 = Random.Range(0, turretSpawnEmpties.Length);
            } while (rand3 == rand1 || rand3 == rand2);

            int chance = Random.Range(0, 3);
            if (chance == 1)
                spawnVegetation();

            do
            {
                rand4 = Random.Range(0, turretSpawnEmpties.Length);
            } while (rand4 == rand1 || rand4 == rand2 || rand4 == rand3);

            chance = Random.Range(0, 3);
            if (chance == 1 || chance == 2)
                spawnSecondary();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnTurretIndicator(int i)
    {
        GameObject go = Instantiate(offenseIndicator, turretSpawnEmpties[i].transform.position, turretSpawnEmpties[i].transform.rotation);
        manager.turretSpawners.Add(go);
        go.SetActive(false);
    }

    void spawnVegetation()
    {
        int rand = Random.Range(0, vegetation.Length);
        if (rand == 4)
            Instantiate(vegetation[rand], turretSpawnEmpties[rand3].transform.position, UnityEngine.Quaternion.Euler(-90, 0, Random.Range(0, 360)));
        else if (rand == 2)
            Instantiate(vegetation[rand], turretSpawnEmpties[rand3].transform.position + new UnityEngine.Vector3(0, 2f, 0), UnityEngine.Quaternion.Euler(-90, 0, Random.Range(0, 360)));
        else
            Instantiate(vegetation[rand], turretSpawnEmpties[rand3].transform.position + new UnityEngine.Vector3(0, -2f, 0), UnityEngine.Quaternion.Euler(-90, 0, Random.Range(0, 360)));
    }

    void spawnSecondary()
    {
        int rand = Random.Range(0, vegetation.Length);
        if (rand == 1 && rand == 3)
            Instantiate(secondaryVegetation[rand], turretSpawnEmpties[rand4].transform.position, UnityEngine.Quaternion.Euler(-90, 0, Random.Range(0, 360)));
        else if (rand == 0)
            Instantiate(secondaryVegetation[rand], turretSpawnEmpties[rand4].transform.position + new UnityEngine.Vector3(0, -0.5f, 0), UnityEngine.Quaternion.Euler(-90, 0, Random.Range(0, 360)));
        else
            Instantiate(secondaryVegetation[rand], turretSpawnEmpties[rand4].transform.position + new UnityEngine.Vector3(0, 0.4f, 0), UnityEngine.Quaternion.Euler(-90, 0, Random.Range(0, 360)));
    }
}
