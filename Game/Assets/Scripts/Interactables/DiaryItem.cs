using UnityEngine;

public class DiaryItem : Interactable
{
    private Inventory inventory;

    public void Setup()
    {
        this.inventory = RuntimeStuff.GetSingleton<Inventory>();

        // The item only needs to exist if the player does not already have it
        if(this.inventory.GetDiary()) Destroy(this.gameObject);

        // Must be set before start runs
        this.UpdateFloatingText("Press F to grab book");
    }

    protected override void Action()
    {
        GetComponents<FMODUnity.StudioEventEmitter>()[0].Play();

        this.inventory.AddDiary();
        
        Debug.Log("Added diary item to inventory");
        
        Destroy(this.gameObject);
    }
}
