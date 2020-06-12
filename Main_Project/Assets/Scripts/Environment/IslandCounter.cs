using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandCounter : MonoBehaviour
{
    public Manager manager;
    public GameObject islandSpawner;
    public int numberOfIslands;
    private const int islandOFFSET = 5;
    // Start is called before the first frame update
    void Start()
    {
        numberOfIslands = 30;
        manager = FindObjectOfType<Manager>();
        numberOfIslands = Random.Range(numberOfIslands - islandOFFSET, numberOfIslands + islandOFFSET);
        transform.position = new Vector3(manager.medianX, manager.medianY, manager.medianZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfIslands > 0 && manager.isMapCreated == true)
        {
            float choose = Random.Range(0.0f, 1.0f);
            float posX = 0, posY = 0, posZ = 0;
            if (choose < 0.5f)
            {
                float chance = Random.Range(0.0f, 1.0f);
                if (chance < 0.5)
                {
                    posX = Random.Range(manager.minX - 30, manager.minX - 100);
                }
                else
                {
                    posX = Random.Range(manager.maxX + 30, manager.maxX + 100);
                }

                chance = Random.Range(0.0f, 1.0f);
                if (chance < 0.5)
                {
                    posY = Random.Range(manager.minY - 100, manager.minY - 300);
                }
                else
                {
                    posY = Random.Range(manager.maxY + 100, manager.maxY + 300);
                }

                posZ = Random.Range(-300, 300);
            }
            else
            {
                posX = Random.Range(-300, 300);

                float chance = Random.Range(0.0f, 1.0f);
                if (chance < 0.5)
                {
                    posY = Random.Range(manager.minY - 100, manager.minY - 300);
                }
                else
                {
                    posY = Random.Range(manager.maxY + 100, manager.maxY + 300);
                }

                chance = Random.Range(0.0f, 1.0f);
                if (chance < 0.5)
                {
                    posZ = Random.Range(manager.minZ - 30, manager.minZ - 100);
                }
                else
                {
                    posZ = Random.Range(manager.maxZ + 30, manager.maxZ + 100);
                }
            }
            

            Instantiate(islandSpawner, transform.position + new Vector3(posX, posY, posZ), transform.rotation);
            numberOfIslands--;
        }
        
    }
}
