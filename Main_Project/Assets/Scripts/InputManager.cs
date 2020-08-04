using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] navigators = GameObject.FindGameObjectsWithTag("EnemyNavigator");

            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            foreach (GameObject navigator in navigators)
            {
                Destroy(navigator);
            }
        }
    }
}
