using UnityEngine;

public class HiddenItem : Interactable
{
    private Inventory inventory;
    public void Setup()
    {
        this.inventory = RuntimeStuff.GetSingleton<Inventory>();

        // The item only needs to exist if the player does not already have it
        if(this.inventory.GetHiddenGeneratorItem()) Destroy(this.gameObject);

        // Must be set before start runs
        this.UpdateFloatingText("Press F to grab part");
    }

    protected override void Action()
    {
        this.inventory.AddHiddenGeneratorItem();
        
        Debug.Log("Added hidden item to inventory");
        
        Destroy(this.gameObject);
    }
}
