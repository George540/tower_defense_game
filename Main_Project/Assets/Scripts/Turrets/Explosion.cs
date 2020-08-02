using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float scale;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroy", 0.8f);
    }

    void destroy()
    {
        Destroy(gameObject);
    }

}
