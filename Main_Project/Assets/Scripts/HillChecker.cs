using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillChecker : MonoBehaviour
{

    bool isCollided = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            isCollided = true;
        }
    }

    public bool getCollided()
    {
        return isCollided;
    }

    public void setCollided(bool col)
    {
        isCollided = col;
    }
}
