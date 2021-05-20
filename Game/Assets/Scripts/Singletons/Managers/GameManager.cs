using UnityEngine;

public class GameManager : MonoBehaviour, ISingleton
{
    public bool isDaytime = false;
    private GameObject[] zones = new GameObject[4];
    private PlayerAPI player;
    private GameState gameState;
    private LightingManager lightingManager;

    void Awake()
    {
        this.register();
    }
    
    private void Start()
    {
        this.player = RuntimeStuff.GetSingleton<PlayerAPI>();
        this.gameState = RuntimeStuff.GetSingleton<GameState>();
        this.lightingManager = RuntimeStuff.GetSingleton<LightingManager>();

        this.ApplyState(true);
    }

    #region DAYTIME
    public bool IsDaytime()
    {
        return this.isDaytime;
    }

    public void SetDaytime(bool isDaytime)
    {
        this.isDaytime = isDaytime;

        foreach(GameObject zone in this.zones)
        {
            zone.GetComponent<ZoneLightSystem>().SetDaytime(isDaytime);
        }
    }
    #endregion

    #region ZONES

    public void RegisterZone(GameObject gameObject, int zone)
    {
        this.zones[zone - 1] = gameObject;
    }

    public GameObject GetZone(int zone)
    {
        return this.zones[zone - 1];
    }

    #region LIGHTS
    public void TurnOnZoneLights(int zone, bool flicker = false)
    {
        this.GetZone(zone).GetComponent<ZoneLightSystem>().SetState(true, flicker);
    }
    #endregion

    #endregion

    #region GAME_STATE
    public void ApplyState(bool continueGame)
    {
        if(!continueGame) this.gameState.NewGame();
        else
        {
            if(!this.gameState.HasData()) return;

            // Player stuff
            Vector3 playerPosition, playerRotation;
            float health, sanity;
            (playerPosition, playerRotation, health, sanity) = this.gameState.GetPlayerInfo();
            this.ApplyPlayerInfo(playerPosition, playerRotation, health, sanity);

            // Time of day
            Vector3 sunLocation, sunRotation;
            (sunLocation, sunRotation) = this.gameState.GetSunInfo();
            this.lightingManager.SetSunPosition(sunLocation, sunRotation);

            //TODO
            Debug.LogWarning("TODO");
        }
    }

    public (Vector3, Vector3) GetSunInfo()
    {
        return this.lightingManager.GetSunPosition();
    }

    public void SetSunInfo(Vector3 pos, Vector3 rot)
    {
        this.lightingManager.SetSunPosition(pos, rot);
    }

    /// <summary>
    /// Gets the player's state
    /// </summary>
    /// <returns>
    /// (v3 Pos, v3 Rot, Health, Sanity)
    /// </returns>
    public (Vector3, Vector3, float, float) GetPlayerInfo()
    {
        return (this.player.GetPosition(), this.player.GetRotation(), this.player.GetHealth(), this.player.GetSanity());
    }
    public void ApplyPlayerInfo(Vector3 pos, Vector3 rot, float health, float sanity)
    {
        this.player.SetPosition(pos);
        this.player.SetRotation(rot);
        this.player.SetHealth(health);
        this.player.SetSanity(sanity);
    }
    #endregion

    /**
     * DON'T USE
     * This shouldn't be public but it must be so that the interface enforces it's existence
     */
    public void register()
    {
        RuntimeStuff.AddSingleton<GameManager>(this);
    }
}
