using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class IslandRandomSpawner : MonoBehaviour
{
    public GameObject island;
    private bool canSpawnIsland = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (canSpawnIsland == true)
        {
            spawnIsland();
            Destroy(gameObject);
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

    void spawnIsland()
    {
        Instantiate(island, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
