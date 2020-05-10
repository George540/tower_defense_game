using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        index = gameObject.transform.parent.GetComponent<Ground>().GetIndex();
    }

    public int getIndex()
    {
        return index;
    }

    public void setIndex(int i)
    {
        index = i;
    }
}
