using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private bool inventoryEnabled;

    public GameObject inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventoryEnabled = false;
    }

    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            GameObject obj = this.FindClosestInteractable();
            Interactable i = obj.GetComponent<Interactable>();
            
            if(i != null) i.Interact();
        }

        if (Input.GetKeyDown(KeyCode.I))
            inventoryEnabled = !inventoryEnabled;

        if (inventoryEnabled)
            inventory.SetActive(true);
        else
            inventory.SetActive(false);
    }

    // thanks: https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    private GameObject FindClosestInteractable()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Interactable");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
