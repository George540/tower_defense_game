using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject[] groundPrefab = new GameObject[6];
    public int currentIndex;
    public int randomSide;
    public GameObject currentBlock;
    public GameObject spawner;

    private int randomIndex;
    public int blockCounter = 3;
    private bool isColliding;

    private float countdown = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f && isColliding == false)
        {
            SpawnEnemy();
            countdown = 0.5f;
        }
        countdown -= Time.deltaTime;
        if (blockCounter < 0)
        {
            blockCounter = Random.Range(3,10);
        }
    }

    void SpawnEnemy()
    {
        GameObject go = null;
        if (blockCounter > 0)
        {
            randomIndex = 1;
        }
        else if (blockCounter == 0)
        {
           randomIndex = Random.Range(2, 5);
           // randomIndex = 4;
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
            go = Instantiate(spawner, transform.position, transform.rotation);
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
        }

        transform.Translate(30, 0, 0);
        currentBlock = Instantiate(groundPrefab[randomIndex], transform.position, transform.rotation);
        currentBlock.gameObject.transform.Rotate(-90, 0, 0);
        currentBlock.gameObject.tag = "Block";
        currentBlock.gameObject.AddComponent<BoxCollider>();
        currentBlock.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        SpawnBlockConditions(randomIndex);

        currentIndex = randomIndex;
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            Destroy(gameObject);
            isColliding = true;
            //randomIndex = 0;
            transform.Translate(30, 0, 0);
            currentBlock = Instantiate(groundPrefab[0], transform.position, transform.rotation);
            currentBlock.gameObject.transform.Rotate(-90, 0, 0);
            currentBlock.gameObject.tag = "Block";
            currentBlock.gameObject.AddComponent<BoxCollider>();
            currentBlock.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            Debug.Log("BLOCK ENCOUNTERED");
        }
    }

}
