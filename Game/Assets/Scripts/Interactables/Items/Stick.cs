using UnityEngine;

public class Stick : Interactable
{
    public int spawnDelay;
    [Range(0, 100)]
    public int randomness;
    private Inventory inventory;
    protected override void Start()
    {
        this.inventory = RuntimeStuff.GetSingleton<Inventory>();

        // Must be set before start runs
        this.actionString = "grab stick";

        base.Start();   
    }

    protected override void Action()
    {
        this.inventory.AddStick();
        Debug.Log("Added to inventory, current count: " + this.inventory.GetStickCount());

        float delay = this.spawnDelay * ((Random.Range(-this.randomness, this.randomness+1) + 100) / 100f);
        StartCoroutine(this.DisableForDuration(delay));
    }
}
