using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public bool isMapCreated = false;
    public List<GameObject> grounds = new List<GameObject>();
    public List<GroundSpawner> groundSpawners = new List<GroundSpawner>();
    public List<Waypoint> waypoints = new List<Waypoint>();

    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float maxZ;
    public float minZ;

    public float currency;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (groundSpawners.Count == 0)
        {
            isMapCreated = true;
        }
        if (isMapCreated == true)
        {
            setBoundaries();
        }
    }

    void setBoundaries()
    {
        setX();
        setY();
        setZ();
    }

    void setX()
    {
        maxX = grounds[0].transform.position.x;
        minX = grounds[0].transform.position.x;
        foreach (GameObject ground in grounds)
        {
            if (ground.transform.position.x > maxX)
                maxX = ground.transform.position.x;
            if (ground.transform.position.x < minX)
                minX = ground.transform.position.x;
        }
    }

    void setY()
    {
        maxY = grounds[0].transform.position.y;
        minY = grounds[0].transform.position.y;
        foreach (GameObject ground in grounds)
        {
            if (ground.transform.position.y > maxY)
                maxY = ground.transform.position.y;
            if (ground.transform.position.y < minY)
                minY = ground.transform.position.y;
        }
    }

    void setZ()
    {
        maxZ = grounds[0].transform.position.z;
        minZ = grounds[0].transform.position.z;
        foreach (GameObject ground in grounds)
        {
            if (ground.transform.position.z > maxZ)
                maxZ = ground.transform.position.z;
            if (ground.transform.position.z < minZ)
                minZ = ground.transform.position.z;
        }
    }
}
