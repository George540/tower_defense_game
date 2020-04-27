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
    public GameObject spawner;
    public GameObject hillSpotter;
    public int blockCount = 1000;

    private int randomIndex;
    public int blockIndex = 3;
    private bool isColliding;

    private int[] numbers = { 3, 5, 7, 9};


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
            DestroySpawner();
        }

        if (blockIndex < 0)
        {
            blockIndex = numbers[Random.Range(0, 4)];
            //blockIndex = 3;
        }
        hillSpotter.transform.position = transform.position;
        hillSpotter.transform.rotation = transform.rotation;
    }

    void SpawnBlock()
    {
        if (blockIndex > 0)
        {
            randomIndex = 1;
        }
        else if (blockIndex == 0)
        {
           randomIndex = Random.Range(2, 7);
           //randomIndex = 2;
        }
        blockIndex--;


        if (currentIndex == 4)
        {
            transform.Rotate(0, 90, 0, Space.Self);
        }
        else if (currentIndex == 5)
        {
            transform.Rotate(0, -90, 0, Space.Self);
        }
        else if (currentIndex == 6)
        {
            transform.Rotate(0, 90, 0, Space.Self);
            Spawner(180f);
        }
        else if (currentIndex == 2)
        {
            blockIndex--;
            transform.Translate(30, 30, 0);
            GameObject go = Instantiate(groundPrefab[currentIndex], transform.position, transform.rotation);
            go.gameObject.transform.Rotate(-90, 180, 0);
            go.gameObject.tag = "Block";
            go.gameObject.AddComponent<BoxCollider>();
            go.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            go.gameObject.GetComponent<BoxCollider>().size = new Vector3(0.019f, 0.019f, 0.019f);
            go.gameObject.AddComponent<Rigidbody>();
            go.gameObject.GetComponent<Rigidbody>().useGravity = false;
            go.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            go.gameObject.AddComponent<Ground>();
            go.gameObject.GetComponent<Ground>().SetIndex(currentIndex);
            transform.Translate(0, 30, 0);
        }
        else if (currentIndex == 3)
        {
            blockIndex--;
            transform.Translate(30, -30, 0);
            GameObject go = Instantiate(groundPrefab[currentIndex], transform.position, transform.rotation);
            go.gameObject.transform.Rotate(-90, 0, 0);
            go.gameObject.tag = "Block";
            go.gameObject.AddComponent<BoxCollider>();
            go.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            go.gameObject.GetComponent<BoxCollider>().size = new Vector3(0.019f, 0.019f, 0.019f);
            go.gameObject.AddComponent<Rigidbody>();
            go.gameObject.GetComponent<Rigidbody>().useGravity = false;
            go.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            go.gameObject.AddComponent<Ground>();
            go.gameObject.GetComponent<Ground>().SetIndex(currentIndex);
            transform.Translate(0, -30, 0);
        }



        transform.Translate(30, 0, 0);

        SetBlock(randomIndex);
        SpawnBlockConditions(randomIndex);

        currentIndex = randomIndex;
        previousBlock = currentBlock;
    }

    // Sets appropriate angle for specified block to match ends
    void SpawnBlockConditions(int index)
    {
        if (index == 0)
        {
            currentBlock.gameObject.transform.Rotate(0, 180, 0);
        }
        else if (index == 4)
        {
            currentBlock.gameObject.transform.Rotate(0, 0, 90);
        }
        else if (index == 5)
        {
            currentBlock.gameObject.transform.Rotate(0, 0, 180);
        }
        else if (index == 6)
        {
            currentBlock.gameObject.transform.Rotate(0, 0, 180);
        }
        else if (index == 2)
        {
            currentBlock.gameObject.transform.Rotate(0, 0, 180);
        }

    }

    // Check Collisions of Spawner with other Spawners and Blocks
    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Block") && !isColliding))
        {
            if (currentBlock.GetComponent<Ground>().GetIndex() > 3)
            {
                SpawnBlock();
            }
            Destroy(currentBlock);
            SetBlock(0);
            Debug.Log("BLOCK ENCOUNTERED");
            Destroy(gameObject);
            isColliding = true;
        }
        if (other.gameObject.CompareTag("BlockSpawner"))
        {
            DestroySpawner();
            Debug.Log("SPAWNER ENCOUNTERED");
        }
    }

    void SetBlock(int index)
    {
        currentBlock = Instantiate(groundPrefab[index], transform.position, transform.rotation);
        currentBlock.gameObject.transform.Rotate(-90, 0, 0);
        currentBlock.gameObject.tag = "Block";
        currentBlock.gameObject.AddComponent<BoxCollider>();
        currentBlock.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        if (index != 2 || index != 3)
        {
            currentBlock.gameObject.GetComponent<BoxCollider>().size = new Vector3(0.019f, 0.019f, 0.019f);
        }
        else
        {
            currentBlock.gameObject.GetComponent<BoxCollider>().size = new Vector3(0.019f, 0.019f, 0.039f);
        }
        currentBlock.gameObject.AddComponent<Rigidbody>();
        currentBlock.gameObject.GetComponent<Rigidbody>().useGravity = false;
        currentBlock.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        currentBlock.gameObject.AddComponent<Ground>();
        currentBlock.gameObject.GetComponent<Ground>().SetIndex(index);
        blockCount--;
        Debug.Log("BLOCK SPAWNED");
    }

    void Spawner(float angleY)
    {
        GameObject go = null;
        go = Instantiate(spawner, transform.position, transform.rotation);
        go.transform.Rotate(0, angleY, 0);
        go.gameObject.tag = "BlockSpawner";
        go.AddComponent<GroundSpawner>();

        for (int i = 0; i < groundPrefab.Length; i++)
        {
            go.GetComponent<GroundSpawner>().groundPrefab[i] = groundPrefab[i];
        }
        go.GetComponent<GroundSpawner>().currentIndex = 1;
        go.GetComponent<GroundSpawner>().randomSide = 0;
        go.GetComponent<GroundSpawner>().spawner = spawner;
        go.GetComponent<GroundSpawner>().blockIndex = 3;
        GameObject go2 = Instantiate(hillSpotter, transform.position, transform.rotation);
        go.GetComponent<GroundSpawner>().hillSpotter = go2;
        go.GetComponent<GroundSpawner>().hillSpotter.AddComponent<HillChecker>();
        go.GetComponent<GroundSpawner>().hillSpotter.GetComponent<HillChecker>().setCollided(false);
        go.GetComponent<GroundSpawner>().blockCount = blockCount;
    }

    void DestroySpawner()
    {
        Destroy(currentBlock);
        SetBlock(0);
        Destroy(gameObject);
    }

}
