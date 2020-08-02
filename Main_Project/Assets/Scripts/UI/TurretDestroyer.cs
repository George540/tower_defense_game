using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretDestroyer : MonoBehaviour
{
    public Manager manager;
    public Text errorText;

    public Camera cam;
    public GameObject button;

    public GameObject currentTurret = null;
    public GameObject offenseSpawner;
    public GameObject supportSpawner;

    private GameObject currentSelector;
    public GameObject selector;

    private GameObject currentRanger;
    public GameObject ranger;

    public Canvas canvas;
    public GameObject inspector;
    public GameObject board;
    public GameObject currentBoard;

    public GameObject[] spawners = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
        {
            getClickUpdateSpawnTurret();
        }
    }

    void getClickUpdateSpawnTurret()
    {
        if (Input.GetMouseButtonUp(0) && currentTurret == null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 5)
                {
                    return;
                }
                if ((hit.transform.gameObject.CompareTag("OffenseTurret") || hit.transform.gameObject.CompareTag("SupportTurret")) && hit.transform.gameObject.layer == 9 && currentTurret != hit.transform.gameObject)
                {
                    currentTurret = hit.transform.gameObject;
                    currentSelector = Instantiate(selector, currentTurret.transform.position + new Vector3(0f, 15f, 0f), currentTurret.transform.rotation);
                    currentRanger = Instantiate(ranger, currentTurret.transform.position + new Vector3(0f, 0f, 0f), currentTurret.transform.rotation * Quaternion.Euler(90f, 0f, 0f));
                    
                    if (hit.transform.gameObject.CompareTag("OffenseTurret"))
                    {
                        currentSelector.GetComponent<Renderer>().material.color = Color.red;
                        currentRanger.GetComponent<Renderer>().material.color = Color.red;
                        var tempColor = currentRanger.GetComponent<Renderer>().material.color;
                        tempColor.a = 0.5f;
                        currentRanger.GetComponent<Renderer>().material.color = tempColor;
                    }
                    else if (hit.transform.gameObject.CompareTag("SupportTurret"))
                    {
                        currentSelector.GetComponent<Renderer>().material.color = Color.blue;
                        currentRanger.GetComponent<Renderer>().material.color = Color.blue;
                        var tempColor = currentRanger.GetComponent<Renderer>().material.color;
                        tempColor.a = 0.5f;
                        currentRanger.GetComponent<Renderer>().material.color = tempColor;
                    }

                    if (currentTurret.GetComponent<Turret>() != null)
                        currentRanger.transform.localScale = new Vector3(currentTurret.GetComponent<Turret>().range * 2, ranger.transform.localScale.y, currentTurret.GetComponent<Turret>().range * 2);
                    //else if (currentTurret.GetComponent<Healer>() != null)
                    //    currentRanger.transform.localScale = new Vector3(currentTurret.GetComponent<Healer>().range * 2, ranger.transform.localScale.y, currentTurret.GetComponent<Healer>().range * 2);

                    currentBoard = Instantiate(board, inspector.transform.position, inspector.transform.rotation);
                    currentBoard.transform.SetParent(canvas.transform);
                    currentBoard.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    currentBoard.gameObject.GetComponent<TurretBoard>().turret = currentTurret;
                    currentBoard.transform.SetSiblingIndex(0);

                    return;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && currentTurret != null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            currentTurret = null;
            Destroy(currentSelector);
            Destroy(currentRanger);
            Destroy(currentBoard);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 5)
                {
                    return;
                }
                if ((hit.transform.gameObject.CompareTag("OffenseTurret") || hit.transform.gameObject.CompareTag("SupportTurret")) && hit.transform.gameObject.layer == 9 && currentTurret != hit.transform.gameObject)
                {
                    currentTurret = hit.transform.gameObject;
                    currentSelector = Instantiate(selector, currentTurret.transform.position + new Vector3(0f, 15f, 0f), currentTurret.transform.rotation);
                    currentRanger = Instantiate(ranger, currentTurret.transform.position + new Vector3(0f, 0f, 0f), currentTurret.transform.rotation * Quaternion.Euler(90f, 0f, 0f));

                    if (hit.transform.gameObject.CompareTag("OffenseTurret"))
                    {
                        currentSelector.GetComponent<Renderer>().material.color = Color.red;
                        currentRanger.GetComponent<Renderer>().material.color = Color.red;
                        var tempColor = currentRanger.GetComponent<Renderer>().material.color;
                        tempColor.a = 0.5f;
                        currentRanger.GetComponent<Renderer>().material.color = tempColor;
                    }
                    else if (hit.transform.gameObject.CompareTag("SupportTurret"))
                    {
                        currentSelector.GetComponent<Renderer>().material.color = Color.blue;
                        currentRanger.GetComponent<Renderer>().material.color = Color.blue;
                        var tempColor = currentRanger.GetComponent<Renderer>().material.color;
                        tempColor.a = 0.5f;
                        currentRanger.GetComponent<Renderer>().material.color = tempColor;
                    }

                    if (currentTurret.GetComponent<Turret>() != null)
                        currentRanger.transform.localScale = new Vector3(currentTurret.GetComponent<Turret>().range * 2, ranger.transform.localScale.y, currentTurret.GetComponent<Turret>().range * 2);
                    //else if (currentTurret.GetComponent<Healer>() != null)
                    //    currentRanger.transform.localScale = new Vector3(currentTurret.GetComponent<Healer>().range * 2, ranger.transform.localScale.y, currentTurret.GetComponent<Healer>().range * 2);

                    currentBoard = Instantiate(board, inspector.transform.position, inspector.transform.rotation);
                    currentBoard.transform.SetParent(canvas.transform);
                    currentBoard.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    currentBoard.gameObject.GetComponent<TurretBoard>().turret = currentTurret;
                    currentBoard.transform.SetSiblingIndex(0);

                    return;
                }
            }
            
        }
    }

    public void DestroyTurret()
    {
        GameObject go;
        if (currentTurret.CompareTag("OffenseTurret"))
        {
            go = Instantiate(spawners[0], currentTurret.transform.position, currentTurret.transform.rotation * Quaternion.Euler(-90f, 90f, 90f));
            manager.turretSpawners.Add(go);
        }
        else if (currentTurret.CompareTag("SupportTurret"))
        {
            go = Instantiate(spawners[1], currentTurret.transform.position, currentTurret.transform.rotation * Quaternion.Euler(-90f, 00f, 90f));
            if (currentTurret.GetComponent<Mortar>() != null || currentTurret.GetComponent<Scout>() != null || currentTurret.GetComponent<Healer>() != null || currentTurret.GetComponent<MiningDrill>() != null || currentTurret.GetComponent<Outpost>() != null)
            {
                go.transform.Rotate(0f, 0f, 0f);
                if (currentTurret.GetComponent<Mortar>() != null || currentTurret.GetComponent<Scout>() != null || currentTurret.GetComponent<Healer>() != null || currentTurret.GetComponent<MiningDrill>() != null || currentTurret.GetComponent<Outpost>() != null)
                    go.transform.Rotate(0f, 90f, 0f);
            }
            if (currentTurret.GetComponent<SniperTurret>() != null)
                go.transform.Rotate(0f, 0f, 90f);
            manager.supportSpawners.Add(go);
        }
        manager.numberOfTurrets--;
        manager.currency += currentTurret.GetComponent<Turret>().costDeployment/2;
        manager.currency = Mathf.Floor(manager.currency);

        Destroy(currentSelector);
        Destroy(currentRanger);
        Destroy(currentTurret);
        Destroy(currentBoard);
    }
}
