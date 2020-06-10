using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class IslandRandomSpawner : MonoBehaviour
{
    public int numberOfIslands;
    public int OFFSET;
    public Manager manager;
    public GameObject turretIndicator;
    public GameObject island;
    public GameObject currentIsland;

    public bool canSpawnIsland;

    private const int islandOFFSET = 5;
    private int randY;

    // Start is called before the first frame update
    void Start()
    {
        numberOfIslands = Random.Range(numberOfIslands - islandOFFSET, numberOfIslands + islandOFFSET);
        transform.GetComponent<BoxCollider>().size = new Vector3(89f, 90f, 89f);
        //canSpawnIsland = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        calculateIslandCoords();
        spawnIsland();
        //destroySpawner();
    }

    /*
    void spawnTurretIndicators(int children)
    {
        for (int i = 0; i < children; i++)
        {
            Instantiate(turretIndicator, currentIsland.transform.GetChild(i).position, currentIsland.transform.GetChild(i).rotation);
        }
    }
    */

    void calculateIslandCoords()
    {
        if (manager.isMapCreated && numberOfIslands > 0)
        {
            transform.GetComponent<BoxCollider>().size = new Vector3(89f, manager.maxY - manager.minY + 300f, 89f);
            int OFFSET = (int)transform.GetComponent<BoxCollider>().size.y;
            int randX = Random.Range(-10, 10) * 30;
            randY = (int)Random.Range(-transform.GetComponent<BoxCollider>().size.y/30, transform.GetComponent<BoxCollider>().size.y/30) * 30;
            int randZ = Random.Range(-10, 10) * 30;
            transform.position = new Vector3(randX, 0, randZ);
        }
    }

    void spawnIsland()
    {
        if (canSpawnIsland == true && numberOfIslands > 0 && manager.isMapCreated)
        {
            currentIsland = Instantiate(island, new Vector3(transform.position.x, randY, transform.position.z), transform.rotation);
            //spawnTurretIndicators(currentIsland.transform.childCount);
            numberOfIslands--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Island") || other.gameObject.CompareTag("Block"))
            canSpawnIsland = false;
    }

    private void OnTriggerExit(Collider other)
    {
        canSpawnIsland = true;
    }

    void destroySpawner()
    {
        if (numberOfIslands == 0)
            Destroy(gameObject);
    }
}
