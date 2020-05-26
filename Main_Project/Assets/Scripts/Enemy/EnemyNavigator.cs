using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigator : MonoBehaviour
{
    public Manager manager;
    public GameObject enemyObject;
    public float speed;
    public GameObject currentWaypoint;
    public bool canRotate = false;
    public const float OFFSET = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
        speed = enemyObject.GetComponent<Enemy>().moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
        rotate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Waypoint"))
        {
            currentWaypoint = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Waypoint"))
        {
            currentWaypoint = null;
            canRotate = false;
        }
    }

    void rotate()
    {
        if (currentWaypoint != null && Vector3.Distance(currentWaypoint.transform.position, transform.position) < OFFSET && canRotate == false)
        {
            canRotate = true;
            transform.position = currentWaypoint.transform.position;
            if (Vector3.Distance(currentWaypoint.transform.position, transform.position) == 0)
            {
                if (currentWaypoint.gameObject.GetComponent<Waypoint>().getIndex() == 0)
                {
                    manager.totalBaseHealth--;
                    Destroy(enemyObject);
                    Destroy(gameObject);
                }
                else if (currentWaypoint.gameObject.GetComponent<Waypoint>().getIndex() == 2)
                {
                    if (currentWaypoint.gameObject.GetComponent<Waypoint>().name == "Waypoint1")
                    {
                        transform.Rotate(0f, 0f, 45f);
                    }
                    else if (currentWaypoint.gameObject.GetComponent<Waypoint>().name == "Waypoint2")
                    {
                        transform.Rotate(0f, 0f, -45f);
                    }
                }
                else if (currentWaypoint.gameObject.GetComponent<Waypoint>().getIndex() == 3)
                {
                    if (currentWaypoint.gameObject.GetComponent<Waypoint>().name == "Waypoint1")
                    {
                        transform.Rotate(0f, 0f, -45f);
                    }
                    else if (currentWaypoint.gameObject.GetComponent<Waypoint>().name == "Waypoint2")
                    {
                        transform.Rotate(0f, 0f, 45f);
                    }
                }
                else if (currentWaypoint.gameObject.GetComponent<Waypoint>().getIndex() == 4)
                {
                    transform.Rotate(0f, -90f, 0f);
                }
                else if (currentWaypoint.gameObject.GetComponent<Waypoint>().getIndex() == 5)
                {
                    transform.Rotate(0f, 90f, 0f);
                }
                else if (currentWaypoint.gameObject.GetComponent<Waypoint>().getIndex() == 6)
                {
                    int random = Random.Range(0, 2);
                    if (random == 0)
                    {
                        transform.Rotate(0f, -90f, 0f);
                    }
                    else if (random == 1)
                    {
                        transform.Rotate(0f, 90f, 0f);
                    }
                }
            }
        }
    }
}
