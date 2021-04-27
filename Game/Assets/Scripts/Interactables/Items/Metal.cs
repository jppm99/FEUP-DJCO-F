using System.Collections;
using UnityEngine;

public class Metal : Interactable
{
    public int spawnDelay;
    private Inventory inventory;
    private MeshRenderer[] meshRenderers;
    protected override void Start()
    {
        this.inventory = RuntimeStuff.GetSingleton<Inventory>();

        // Must be set before start runs
        this.actionString = "grab metal";

        base.Start();
        
        this.meshRenderers = this.GetComponentsInChildren<MeshRenderer>();
    }

    protected override void Action()
    {
        this.inventory.AddMetal();
        Debug.Log("Added to inventory, current count: " + this.inventory.GetMetalCount());

        StartCoroutine(this.DisableForDuration(this.spawnDelay));
    }

    private IEnumerator DisableForDuration(float duration)
    {
        this.isActive = false;
        foreach(MeshRenderer mr in this.meshRenderers)
        {
            mr.enabled = false;
        }

        yield return new WaitForSeconds(duration);

        foreach(MeshRenderer mr in this.meshRenderers)
        {
            mr.enabled = true;
        }
        this.isActive = true;
    }
}
