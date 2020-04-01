using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    public int index = 0;
    private bool isDestroyed;

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
        if (other.gameObject.CompareTag("Block") && !other.gameObject.GetComponent<Ground>().isDestroyed)
        {
            if (index == 0)
            {
                if (other.gameObject.GetComponent<Ground>().GetIndex() == 0)
                {
                    other.gameObject.GetComponent<Ground>().isDestroyed = true;
                    Destroy(other.gameObject);
                }
                else
                {

                }
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
