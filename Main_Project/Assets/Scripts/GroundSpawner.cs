using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject[] groundPrefab = new GameObject[6];
    public int currentIndex;
    public int randomSide;
    public GameObject currentBlock;
    public GameObject previousBlock;
    public GameObject spawner;

    private int randomIndex;
    public int blockCounter = 3;
    private bool isColliding;

    private float countdown = 0.3f;
    private int[] numbers = { 3, 5, 7, 9, 11};


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f && isColliding == false)
        {
            SpawnBlock();
            countdown = 0.3f;
        }
        countdown -= Time.deltaTime;
        if (blockCounter < 0)
        {
            blockCounter = numbers[Random.Range(0, 5)];
        }
    }

    void SpawnBlock()
    {
        GameObject go = null;
        if (blockCounter > 0)
        {
            randomIndex = 1;
        }
        else if (blockCounter == 0)
        {
           randomIndex = Random.Range(2, 5);
           //randomIndex = 2;
        }
        blockCounter--;

        if (currentIndex == 2)
        {
            transform.Rotate(0, 90, 0, Space.Self);
        }
        else if (currentIndex == 3)
        {
            transform.Rotate(0, -90, 0, Space.Self);
        }
        else if (currentIndex == 4)
        {
            transform.Rotate(0, 90, 0, Space.Self);
            //Instantiate(spawner, transform.position, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
            go = Spawner();
        }

        transform.Translate(30, 0, 0);
        Debug.Log("TRANSLATED SPAWNER");
        SetBlock(randomIndex);
        SpawnBlockConditions(randomIndex);

        currentIndex = randomIndex;
        previousBlock = currentBlock;
        //yield return new WaitForSeconds(1f);
    }

    // Sets appropriate angle for specified block to match ends
    void SpawnBlockConditions(int index)
    {
        if (index == 0)
        {
            currentBlock.gameObject.transform.Rotate(0, 180, 0);
        }
        else if (index == 2)
        {
            currentBlock.gameObject.transform.Rotate(0, 0, 90);
        }
        else if (index == 3)
        {
            currentBlock.gameObject.transform.Rotate(0, 0, 180);
        }
        else if (index == 4)
        {
            currentBlock.gameObject.transform.Rotate(0, 0, 180);
        }
    }

    // Check Collisions of Spawner with other Spawners and Blocks
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block") && !isColliding)
        {
            Destroy(gameObject);
            isColliding = true;
            //randomIndex = 0;
            transform.Translate(30, 0, 0);
            SetBlock(0);
            Debug.Log("BLOCK ENCOUNTERED");
        }
        if (other.gameObject.CompareTag("BlockSpawner"))
        {
            Destroy(currentBlock);
            SetBlock(0);
            SpawnBlockConditions(randomIndex);
            Destroy(gameObject);
        }
    }

    void SetBlock(int index)
    {
        currentBlock = Instantiate(groundPrefab[index], transform.position, transform.rotation);
        currentBlock.gameObject.transform.Rotate(-90, 0, 0);
        currentBlock.gameObject.tag = "Block";
        currentBlock.gameObject.AddComponent<BoxCollider>();
        currentBlock.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        currentBlock.gameObject.GetComponent<BoxCollider>().size = new Vector3(0.019f, 0.019f, 0.019f);
        currentBlock.gameObject.AddComponent<Rigidbody>();
        currentBlock.gameObject.GetComponent<Rigidbody>().useGravity = false;
        currentBlock.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        currentBlock.gameObject.AddComponent<Ground>();
        currentBlock.gameObject.GetComponent<Ground>().SetIndex(index);
        Debug.Log("BLOCK SPAWNED");
    }

    GameObject Spawner()
    {
        GameObject go = null;
        go = Instantiate(spawner, transform.position, transform.rotation);
        go.gameObject.tag = "BlockSpawner";
        go.transform.Rotate(0, 180, 0);
        go.AddComponent<GroundSpawner>();

        for (int i = 0; i < groundPrefab.Length; i++)
        {
            go.GetComponent<GroundSpawner>().groundPrefab[i] = groundPrefab[i];
        }
        go.GetComponent<GroundSpawner>().currentIndex = 1;
        go.GetComponent<GroundSpawner>().randomSide = 0;
        go.GetComponent<GroundSpawner>().spawner = spawner;
        go.GetComponent<GroundSpawner>().blockCounter = 3;

        return go;
    }


}
