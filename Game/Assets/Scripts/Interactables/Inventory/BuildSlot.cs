using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class BuildSlot : MonoBehaviour
{
    public GameObject buildable;
    public string item;

    private Dictionary<string, RequirementSlot> requirements;
    private GameObject canvas;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    // SetRequirements is called when the game is started
    public void SetRequirements()
    {
        requirements = new Dictionary<string, RequirementSlot>();

        RequirementSlot[] requirementsList = buildable.GetComponentsInChildren<RequirementSlot>();

        for (int i = 0; i < requirementsList.Length; i++) {
            requirements.Add(requirementsList[i].item, requirementsList[i]);
        }
    }

    // UpdateRequirements is called when there are changes in the items in the inventory
    public void UpdateRequirements(string item, int count)
    {
        if (requirements.ContainsKey(item))
            requirements[item].UpdateSatisfied(count);
    }

    // BuildItem is called when the button to build is clicked
    public void BuildItem()
    {
        bool satisfied = true;

        // Check if requirements are satisfied
        for (int i = 0; i < requirements.Count; i++) {
            if (!requirements.ElementAt(i).Value.GetSatisfied()) {
                satisfied = false;
                break;
            }
        }

        if (satisfied)
            canvas.GetComponent<InventoryUI>().BuildItem(this);
    }
}
