using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public GameObject turret;
    public bool isLookingAtTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.gameObject == turret.GetComponent<Turret>().target)
        {
            isLookingAtTarget = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && other.gameObject == turret.GetComponent<Turret>().target)
        {
            isLookingAtTarget = false;
        }
    }
}
