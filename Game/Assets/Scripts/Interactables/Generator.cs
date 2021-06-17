using UnityEngine;

public class Generator : Interactable
{
    [SerializeField] private float distanceToPlayer;
    private GameManager gameManager;
    private int zone;
    private Inventory inventory;
    private bool hasBeenFixed, markerHasBeenRemoved = false;

    protected override void Action()
    {
        if(!this.gameManager.GetLightsState(this.zone) && this.CanFix())
        {
            RuntimeStuff.GetSingleton<CameraManager>().PlayCutscene(this.zone);
            this.gameManager.TurnOnZoneLights(this.zone, true);
            this.UpdateFloatingText("");
            this.RemoveMarkerFromMinimap();

            // Saving game (checkpoint)
            RuntimeStuff.GetSingleton<Inventory>().SaveData();
            RuntimeStuff.GetSingleton<GameState>().SaveData();

            // Sounds
            GetComponents<FMODUnity.StudioEventEmitter>()[0].Play();
        }
    }

    protected override void Start()
    {
        this.gameManager = RuntimeStuff.GetSingleton<GameManager>();
        this.inventory = RuntimeStuff.GetSingleton<Inventory>();
        this.zone = this.transform.GetComponentInParent<Zone>().GetZone();

        this.maxDistanceToPlayer = this.distanceToPlayer;

        this.UpdateGeneratorText(fromStart:true);

        base.Start();
    }

    protected override void FixedUpdate() 
    {
        if(Time.timeScale == 0) return;

        if(this.gameManager.GetLightsState(this.zone))
        {
            this.UpdateFloatingText("");
            this.RemoveMarkerFromMinimap();
        }
        else if(!this.gameManager.GetLightsState(this.zone) && this.CanFix())
        {
            this.UpdateFloatingText("Press F to fix");
        }

        base.FixedUpdate();
    }

    public void UpdateGeneratorText(bool fromStart = false)
    {
        string g1 = "";
        string g2 = "First you need to find something";
        string g3 = "You must build the the tool";
        string g4 = "Somewhere there is a monster that will drop you the tool you need";

        if(!fromStart && this.gameManager.GetLightsState(this.zone))
        {
            this.UpdateFloatingText("");
            this.RemoveMarkerFromMinimap();
        }
        else if(fromStart || !this.CanFix())
        {
            // Set floating text for when the player cannot fix the generator
            switch (this.zone)
            {
                case 1:
                    // No need for text because it is always fixable
                    this.UpdateFloatingText(g1);
                    break;
                case 2:
                    this.UpdateFloatingText(g2);
                    break;
                case 3:
                    this.UpdateFloatingText(g3);
                    break;
                case 4:
                    this.UpdateFloatingText(g4);
                    break;
                default:
                    this.UpdateFloatingText("This shouldn't happen");
                    break;
            }
        }
        else
        {
            this.UpdateFloatingText("Press F to fix");
        }
    }

    private bool CanFix()
    {
        switch (this.zone)
        {
            case 1:
                // Zone 1's generator doesn't need anything to be fixed
                return true;
            case 2:
                // Needs a hidden item
                if(inventory.GetHiddenGeneratorItem()) return true;
                else return false;
            case 3:
                // Needs a item buildable by the player
                if(inventory.GetBuildableGeneratorItem()) return true;
                else return false;
            case 4:
                // Needs the item dropped by a monster
                if(inventory.GetMonsterGeneratorItem()) return true;
                else return false;
            default:
                return false;
        }
    }

    private void RemoveMarkerFromMinimap()
    {
        if(markerHasBeenRemoved) return;
        markerHasBeenRemoved = true;
        GameObject marker = gameObject.GetComponentInChildren<KeepSameRotationAsPlayer>()?.gameObject;
        if(marker != null && marker.activeInHierarchy) marker.SetActive(false);
    }
}
