public class MonsterItem : Interactable
{
    private Inventory inventory;
    protected override void Start()
    {
        base.Start();

        // Disable interactions: monster only drops item when dead
        this.Disable();
    }

    public void Setup()
    {
        // The monster only needs to exist if the player does not already have the item
        this.inventory = RuntimeStuff.GetSingleton<Inventory>();
        if(this.inventory.GetMonsterGeneratorItem()) Destroy(this.transform.parent.gameObject);
        this.UpdateFloatingText("Press F to grab tool");
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
