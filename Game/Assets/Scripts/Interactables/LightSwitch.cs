using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
{
    private GameManager gameManager;
    private int zone;
    protected override void Action()
    {
        // add call to condition check and continue if it holds
        this.gameManager.TurnOnZoneLights(this.zone);
        this.Disable();
    }

    protected override void Start()
    {
        this.gameManager = RuntimeStuff.GetSingleton<GameManager>();
        this.zone = this.transform.GetComponentInParent<Zone>().GetZone();

        base.Start();
    }

    protected override void FixedUpdate() 
    {
        // probably add a condition check here (new function)
        //    if true update floating text

        base.FixedUpdate();
    }
}
