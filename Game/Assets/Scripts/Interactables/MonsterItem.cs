public class MonsterItem : Interactable
{
    private Inventory inventory;
    protected override void Start()
    {
        this.inventory = RuntimeStuff.GetSingleton<Inventory>();

        // The monster only needs to exist if the player does not already have the item
        if(this.inventory.GetMonsterGeneratorItem()) Destroy(this.transform.parent.gameObject);
        this.floatingText = "Press F to grab tool";
        base.Start();

        // Disable interactions: monster only drops item when dead
        this.Disable();
    }
    
    protected override void Action()
    {
        this.inventory.AddMonsterGeneratorItem();
        Destroy(this.transform.parent.gameObject);
    }

    public void EnableInteraction()
    {
        this.Enable();
    }
}
