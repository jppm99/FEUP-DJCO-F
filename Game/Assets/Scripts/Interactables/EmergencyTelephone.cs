using UnityEngine;

public class EmergencyTelephone : Interactable
{
    [SerializeField] private float distanceToPlayer;
    public RadioTowerLight towerLight;
    private GameManager gameManager;
    private bool hasPower = false;

    protected override void Action()
    {
        if(this.hasPower)
        {
            RuntimeStuff.GetSingleton<CameraManager>().PlayFinalCutscene();
        }
    }

    protected override void Start()
    {
        this.gameManager = RuntimeStuff.GetSingleton<GameManager>();
        this.maxDistanceToPlayer = this.distanceToPlayer;

        this.UpdateFloatingText("Not enough power...");

        base.Start();
    }

    protected override void FixedUpdate()
    {
        if(
            !this.hasPower &&
            this.gameManager.GetLightsState(1) &&
            this.gameManager.GetLightsState(2) &&
            this.gameManager.GetLightsState(3) &&
            this.gameManager.GetLightsState(4)
        ) {
            this.hasPower = true;
            towerLight.TurnOn();
            this.UpdateFloatingText("Press F to call Home");
        }

        base.FixedUpdate();
    }
}
