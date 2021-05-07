public class MonsterItem : Interactable
{
    protected override void Action()
    {
        RuntimeStuff.GetSingleton<Inventory>().AddMonsterGeneratorItem();
        Destroy(this.gameObject);
    }

    protected override void Start()
    {
        this.floatingText = "Press F to grab tool";
        base.Start();

        // Disable interactions: monster only drops item when dead
        this.Disable();
    }

    public void EnableInteraction()
    {
        this.Enable();
    }
}
