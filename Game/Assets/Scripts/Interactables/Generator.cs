using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Interactable
{
    [SerializeField] private float distanceToPlayer;
    private GameManager gameManager;
    private int zone;
    private Inventory inventory;
    private bool fixable = false, hasBeenFixed;

    
    protected override void Action()
    {
        if(!hasBeenFixed && this.CanFix())
        {
            RuntimeStuff.GetSingleton<CameraManager>().PlayCutscene(this.zone);
            this.gameManager.TurnOnZoneLights(this.zone);
            this.hasBeenFixed = true;
            this.UpdateFloatingText("");
        }
    }

    protected override void Start()
    {
        this.gameManager = RuntimeStuff.GetSingleton<GameManager>();
        this.inventory = RuntimeStuff.GetSingleton<Inventory>();
        this.zone = this.transform.GetComponentInParent<Zone>().GetZone();

        this.hasBeenFixed = false;
        this.maxDistanceToPlayer = this.distanceToPlayer;

        // Set floating text for when the player cannot fix the generator
        switch (this.zone)
        {
            case 1:
                // No need for text because it is always fixable
                this.UpdateFloatingText("");
                break;
            case 2:
                this.UpdateFloatingText("First you need to find something");
                break;
            case 3:
                this.UpdateFloatingText("You must build the the tool");
                break;
            case 4:
                this.UpdateFloatingText("Somewhere there is a monster that will drop you the tool you need");
                break;
            default:
                this.UpdateFloatingText("This shouldn't happen");
                break;
        }

        base.Start();
    }

    protected override void FixedUpdate() 
    {
        if(!fixable && !hasBeenFixed && this.CanFix())
        {
            fixable = true;
            this.UpdateFloatingText("Press F to fix");
        }

        base.FixedUpdate();
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
}
