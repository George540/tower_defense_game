using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public Manager manager;
    public GameObject[] groundPrefab = new GameObject[8];
    public GameObject turretIndicator;
    public int currentIndex;
    public int randomSide;
    public GameObject currentBlock;
    public GameObject previousBlock;
    public int blockCount = 100;

    private int randomIndex;
    public int blockIndex = 3;
    private bool isColliding;

    private int[] numbers = { 3, 5, 7}; // number of consecutive straight blocks in a row

    public IslandSpawner[] islandSpawners = new IslandSpawner[2];
    private bool canSpawnIsland = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding == false && blockCount > 0)
        {
            SpawnBlock();
        }
        if (blockCount == 0)
        {
            GameObject go = Instantiate(groundPrefab[0], transform.position, transform.rotation);
            manager.groundSpawners.Remove(this);
            Destroy(gameObject);
        }
    }

    void SpawnBlock()
    {

        if (blockIndex > 0)
        {
            randomIndex = 1;
            blockIndex--;
        }
        else if (blockIndex == 0)
        {
            blockIndex = numbers[Random.Range(0, 3)];

            int randomMin = 2;
            int randomizer = Random.Range(0, 2);
            if (randomizer == 1)
                randomMin = 4;
            
            if (blockCount > 5)
            {
                randomIndex = Random.Range(randomMin, 7);
            }
            else
            {
                randomIndex = Random.Range(randomMin, 5);
            }
        }
        if (randomIndex == 1)
        {
            int random1 = Random.Range(0, 2); // pick between 1 and 2 to choose which side to spawn the island;
            int random2 = Random.Range(0, 10); // get the chance to spawn an island
            canSpawnIsland = islandSpawners[0].getCanSpawn() && islandSpawners[1].getCanSpawn();

            if (canSpawnIsland && random2 < 2)
            {
                currentBlock = Instantiate(groundPrefab[7], islandSpawners[random1].transform.position, islandSpawners[random1].transform.rotation);
                if (currentBlock.transform.childCount > 0)
                    spawnTurretIndicators(currentBlock.transform.childCount);
                manager.grounds.Add(currentBlock);
            }
        }
        else if (randomIndex == 2)
        {
            blockIndex--;
            SetBlock(randomIndex);
            blockCount--;
            currentBlock.transform.Rotate(0f, 180f, 0f);
            transform.Translate(60, 60, 0);
            currentIndex = randomIndex;
            return;
        }
        else if (randomIndex == 3)
        {
            blockIndex--;
            SetBlock(randomIndex);
            blockCount--;
            currentBlock.transform.Rotate(0f, 180f, 0f);
            transform.Translate(60, -60, 0);
            currentIndex = randomIndex;
            return;
        }
        else if (randomIndex == 4)
        {
            transform.Rotate(0, -90, 0, Space.Self);
        }
        else if (randomIndex == 5)
        {
            transform.Rotate(0, 90, 0, Space.Self);
        }
        else if (randomIndex == 6)
        {
            blockIndex--;
            blockCount /= 2;
            SetBlock(randomIndex);
            transform.Rotate(0, 90, 0, Space.Self);
            transform.Translate(30, 0, 0);
            GroundSpawner go = Instantiate(this, transform.position, transform.rotation);
            go.blockCount = blockCount;
            go.transform.Rotate(0f, 180f, 0f);
            go.transform.Translate(60, 0, 0);
            go.randomIndex = 1;
            go.blockCount = blockCount;
            manager.groundSpawners.Add(go);
            currentIndex = randomIndex;
            return;
        }

        SetBlock(randomIndex);
        currentIndex = randomIndex;
        transform.Translate(30, 0, 0);
    }

    // Check Collisions of Spawner with other Spawners and Blocks
    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Island")) && !isColliding)
        {
            SetBlock(0);
            //Debug.Log("BLOCK ENCOUNTERED");
            Destroy(gameObject);
            isColliding = true;
        }
        else if (other.gameObject.CompareTag("BlockSpawner"))
        {
            DestroySpawner();
            //Debug.Log("SPAWNER ENCOUNTERED");
        }
    }

    void SetBlock(int index)
    {
        currentBlock = Instantiate(groundPrefab[index], transform.position, transform.rotation);
        currentBlock.gameObject.name = currentBlock.gameObject.name + blockCount;
        manager.grounds.Add(currentBlock);


        if (index == 1)
        {
            if (currentBlock.transform.childCount > 0)
                spawnTurretIndicators(currentBlock.transform.childCount);
        }
        else if (index == 4)
        {
            currentBlock.transform.Rotate(0f, -90f, 0f, Space.Self);
        }
        else if (index == 5)
        {
            currentBlock.transform.Rotate(0f, 90f, 0f, Space.Self);
        }
        else if (index == 5)
        {
            currentBlock.transform.Rotate(0f, 90f, 0f, Space.Self);
        }
        else if (index == 6)
        {
            currentBlock.transform.Rotate(0f, 180f, 0f, Space.Self);
        }

        blockCount--;
        previousBlock = currentBlock;
        //Debug.Log("BLOCK SPAWNED");
    }

    void DestroySpawner()
    {
        manager.groundSpawners.Remove(this);
        Destroy(currentBlock);
        Instantiate(groundPrefab[0], transform.position + new Vector3(-30, 0, 0), transform.rotation);
        Destroy(gameObject);
    }

    void spawnTurretIndicators(int children)
    {
        for(int i = 0; i < children; i++)
        {
            Instantiate(turretIndicator, currentBlock.transform.GetChild(i).position, currentBlock.transform.GetChild(i).rotation);
        }
    }

}
