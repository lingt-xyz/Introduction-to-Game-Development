using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrateManager : MonoBehaviour
{
    [SerializeField]
    List<Crate> crateList;
    [SerializeField]
    int maxCrates;

    Crate currentlySelected;
    CameraMovement camMovement;
    GameObject crateSpawner;

    [SerializeField]
    float maxSpawnOffset;

    [SerializeField]
    Crate cratePrefab;

    void Start ()
    {
        crateList = new List<Crate>();
        GameObject[] allCrates = GameObject.FindGameObjectsWithTag("Crate");
        foreach(GameObject obj in allCrates)
        {
            crateList.Add (obj.GetComponent<Crate>());
        }
        camMovement = Camera.main.GetComponent<CameraMovement>();
        crateSpawner = GameObject.FindGameObjectWithTag("CrateSpawner");
    }
    
    void Update()
    {
        // Selecting a new crate
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast (ray, out hit))
            {
                if(hit.collider.tag == "Crate")
                {
                    if(currentlySelected != null)
                    {
                        currentlySelected.SetSelected(false);
                    }
                    currentlySelected = hit.collider.GetComponent<Crate>();
                    currentlySelected.SetSelected (true);
                    camMovement.FocusTarget(currentlySelected.gameObject.transform);
                }
            }
        }

        // Spawn a new crate
        if(Input.GetKeyDown (KeyCode.Space))
        {
            float spawnX = Random.Range (-maxSpawnOffset, maxSpawnOffset);
            float spawnY = Random.Range (-maxSpawnOffset, maxSpawnOffset);
            float spawnZ = Random.Range (-maxSpawnOffset, maxSpawnOffset);
            Vector3 offset = new Vector3(spawnX, spawnY, spawnZ);
            Vector3 spawnLocation = crateSpawner.transform.position + offset;

            Crate newCrate = Instantiate(cratePrefab, spawnLocation, Quaternion.identity) as Crate;
            crateList.Add (newCrate);
        }

        // Limit number of crates that can be spawned
        if(crateList.Count > 1 && crateList.Count > maxCrates)
        {
            Crate toBeRemoved = crateList[0];
            crateList.Remove (toBeRemoved);
            toBeRemoved.SetSelected(false);
            Destroy (toBeRemoved.gameObject);

            if(camMovement.GetTarget() == toBeRemoved.transform)
            {
                crateList[1].SetSelected(true);
                camMovement.FocusTarget(crateList[1].transform);
            }
        }
    }
}
