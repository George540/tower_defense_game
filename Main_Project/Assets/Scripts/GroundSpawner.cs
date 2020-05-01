using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject[] groundPrefab = new GameObject[7];
    public int currentIndex;
    public int randomSide;
    public GameObject currentBlock;
    public GameObject previousBlock;
    public int blockCount = 100;

    private int randomIndex;
    public int blockIndex = 3;
    private bool isColliding;

    private int[] numbers = { 3, 5, 7, 9};
    private int[] indices1 = { 2, 3, 4, 5, 6 }; //0-5
    private int[] indices2 = { 4, 5, 6 }; //0-3


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
            //go.transform.Translate(-30f, 0f, 0f);
            Destroy(gameObject);
        }

        if (blockIndex < 0)
        {
            blockIndex = numbers[Random.Range(0, 4)];
            //blockIndex = 3;
        }
    }

    void SpawnBlock()
    {

        if (blockIndex > 0)
        {
            randomIndex = 1;
        }
        else if (blockIndex == 0)
        {
            if (blockCount > 1)
            {
                randomIndex = indices1[Random.Range(0, 5)];
            }
            else if (blockCount == 1)
            {
                randomIndex = indices2[Random.Range(0, 3)];
            }
        }
        blockIndex--;

        if (randomIndex == 2)
        {
            blockIndex--;
            SetBlock(randomIndex);
            currentBlock.gameObject.transform.Rotate(0, 180, 0);
            transform.Translate(30, 30, 0);
            SetBlock(randomIndex);
            currentBlock.gameObject.transform.Rotate(0, 180, 0);
            transform.Translate(30, 30, 0);
            currentIndex = randomIndex;
            return;
        }
        else if (randomIndex == 3)
        {
            blockIndex--;
            transform.Translate(0, -30, 0);
            SetBlock(randomIndex);
            transform.Translate(30, -30, 0);
            SetBlock(randomIndex);
            currentIndex = randomIndex;
            transform.Translate(30, 0, 0);
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
            SetBlock(randomIndex);
            transform.Rotate(0, 90, 0, Space.Self);
            transform.Translate(30, 0, 0);
            GroundSpawner go = Instantiate(this, transform.position, transform.rotation);
            go.transform.Rotate(0f, 180f, 0f);
            go.transform.Translate(60, 0, 0);
            go.randomIndex = 1;
            go.blockCount = blockCount;
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
        if ((other.gameObject.CompareTag("Block") && !isColliding))
        {
            SetBlock(0);
            Debug.Log("BLOCK ENCOUNTERED");
            Destroy(gameObject);
            isColliding = true;
        }
        else if (other.gameObject.CompareTag("BlockSpawner"))
        {
            DestroySpawner();
            Debug.Log("SPAWNER ENCOUNTERED");
        }
    }

    void SetBlock(int index)
    {
        currentBlock = Instantiate(groundPrefab[index], transform.position, transform.rotation);
        if (index == 4)
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
        Debug.Log("BLOCK SPAWNED");
    }

    void DestroySpawner()
    {
        Destroy(currentBlock);
        Instantiate(groundPrefab[0], transform.position + new Vector3(-30, 0, 0), transform.rotation);
        Destroy(gameObject);
    }

}
