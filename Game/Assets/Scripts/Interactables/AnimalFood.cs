public class AnimalFood : Interactable
{
    protected override void Action()
    {
        RuntimeStuff.GetSingleton<Inventory>().AddMeat();
        Destroy(this.transform.parent.gameObject);
    }

    protected override void Start()
    {
        this.floatingText = "Press F to grab food";
        base.Start();

        // Disable interactions: animal only turns into food when dead
        this.Disable();
    }

    public void EnableInteraction()
    {
        this.Enable();
    }
}
