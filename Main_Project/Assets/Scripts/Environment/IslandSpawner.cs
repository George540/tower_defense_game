using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour
{
    private bool canSpawn = true;

    public 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Island"))
        {
            canSpawn = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        canSpawn = true;
    }

    public bool getCanSpawn()
    {
        return canSpawn;
    }

    public void setCanSpawn(bool spawn)
    {
        canSpawn = spawn;
    }
}
