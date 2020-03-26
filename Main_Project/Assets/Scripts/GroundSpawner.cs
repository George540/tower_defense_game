using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public Transform[] groundPrefab = new Transform[6];
    public Transform spawnPoint;
    public Transform checker;
    public int currentIndex;
    public int randomSide;
    public Transform currentBlock;

    private Vector3 tempTrans;
    private Quaternion tempRot;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        checker.transform.rotation = spawnPoint.rotation;
        checker.transform.position = spawnPoint.position;
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            int randomIndex = Random.Range(1, 4);
            if (currentIndex == 2)
            {
                transform.Rotate(0, 90, 0, Space.Self);
            }
            else if (currentIndex == 3)
            {
                transform.Rotate(0, -90, 0, Space.Self);
            }
            else if (currentIndex == 4) {

            }
            transform.Translate(30, 0, 0);
            currentBlock = Instantiate(groundPrefab[randomIndex], transform.position, transform.rotation);
            currentBlock.Rotate(-90, 0, 0);
            currentBlock.gameObject.tag = "Block";
            SpawnBlockConditions(randomIndex);

            currentIndex = randomIndex;
            yield return new WaitForSeconds(0.5f);
        }
        //Instantiate(groundPrefab[randomIndex], spawnPoint.position + adderX, Quaternion.Euler(-90, 0, 0));
        //transform.position += adderX;
    }

    void SpawnBlockConditions(int index)
    {
        if (index == 0)
        {
            currentBlock.Rotate(0, 180, 0);
        }
        else if (index == 2)
        {
            currentBlock.Rotate(0, 0, 90);
        }
        else if (index == 3)
        {
            currentBlock.Rotate(0, 0, 180);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
           
        }
    }

    void CheckNumber()
    {

    }
}
