using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    public int index = 0;
    private bool isDestroyed;
    public List<Waypoint> waypoints = new List<Waypoint>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Block") && !collision.gameObject.GetComponent<Ground>().isDestroyed) || collision.gameObject.CompareTag("Island"))
        {
            if (index == 0 && collision.gameObject.GetComponent<Ground>().GetIndex() == 0)
            {
                collision.gameObject.GetComponent<Ground>().isDestroyed = true;
                Destroy(collision.gameObject);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Block") && !other.gameObject.GetComponent<Ground>().isDestroyed) || other.gameObject.CompareTag("Island"))
        {
            if (index == 0 && other.gameObject.GetComponent<Ground>().GetIndex() == 0)
            {
                other.gameObject.GetComponent<Ground>().isDestroyed = true;
                Destroy(other.gameObject);
            }
        }
    }

    public void SetIndex(int i)
    {
        index = i;
    }

    public int GetIndex()
    {
        return index;
    }

    public void SetDestroyed(bool isD)
    {
        isDestroyed = isD;
    }

    public bool GetDestroyed()
    {
        return isDestroyed;
    }
}
