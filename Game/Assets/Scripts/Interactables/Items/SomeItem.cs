using UnityEngine;

public class SomeItem : Interactable
{
    private Inventory inventory;
    protected override void Start()
    {
        this.inventory = RuntimeStuff.GetSingleton<Inventory>();

        // Must be set before start runs
        this.actionString = "grab";

        base.Start();
    }

    protected override void Action()
    {
        this.inventory.AddItemQqlCoisa();
        
        Debug.Log("Added to inventory, current count: " + this.inventory.GetItemQqlCoisaCount());
    }
}
