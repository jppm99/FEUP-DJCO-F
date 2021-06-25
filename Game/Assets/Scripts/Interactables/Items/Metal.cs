using UnityEngine;

public class Metal : Interactable
{
    public int spawnDelay;
    [Range(0, 100)]
    public int randomness;
    private Inventory inventory;
    protected override void Start()
    {
        this.inventory = RuntimeStuff.GetSingleton<Inventory>();

        // Must be set before start runs
        this.floatingText = "Press F to collect metal";

        base.Start();        
    }

    protected override void Action()
    {
        GetComponents<FMODUnity.StudioEventEmitter>()[0].Play();
        this.inventory.AddMetal();
        Debug.Log("Added to inventory, current count: " + this.inventory.GetMetalCount());

        float delay = this.spawnDelay * ((Random.Range(-this.randomness, this.randomness+1) + 100) / 100f);
        StartCoroutine(this.DisableForDuration(delay));
    }
}
