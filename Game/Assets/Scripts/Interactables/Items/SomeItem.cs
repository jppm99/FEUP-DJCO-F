using UnityEngine;

public class SomeItem : Interactable
{
    protected override void Start()
    {
        // Must be set before start runs
        this.actionString = "grab";

        base.Start();
    }

    protected override void Action()
    {
        Inventory inventory = RuntimeStuff.GetSingleton<Inventory>();
        inventory.AddItemQqlCoisa();
        
        Debug.Log("Added to inventory, current count: " + inventory.GetItemQqlCoisaCount());
    }
}
