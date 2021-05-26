using System.Collections.Generic;
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

    public bool GetLightsState(int zone)
    {
        return this.GetZone(zone).GetComponent<ZoneLightSystem>().GetState();
    }
    #endregion

    #endregion

    #region GAME_STATE
    public void ApplyState(bool continueGame)
    {
        if(!continueGame)
        {
            this.gameState.NewGame();
            // Quick hack to spawn the boss (that is on zone 4)
            this.GetZone(4).GetComponent<Spawner>().SpawnMonsters(new List<MonsterData>());
        }
        else
        {
            if (!this.gameState.HasData())
            {
                // Quick hack to spawn the boss (that is on zone 4)
                this.GetZone(4).GetComponent<Spawner>().SpawnMonsters(new List<MonsterData>());
                return;
            }

            // Player stuff
            Vector3 playerPosition, playerRotation;
            float health, sanity;
            (playerPosition, playerRotation, health, sanity) = this.gameState.GetPlayerInfo();
            this.player.SetPosition(playerPosition);
            this.player.SetRotation(playerRotation);
            this.player.SetHealth(health);
            this.player.SetSanity(sanity);

            // Time of day
            Vector3 sunLocation, sunRotation;
            (sunLocation, sunRotation) = this.gameState.GetSunInfo();
            this.lightingManager.SetSunPosition(sunLocation, sunRotation);

            // Generators
            bool generator1, generator2, generator3, generator4;
            (generator1, generator2, generator3, generator4) = this.gameState.GetGeneratorsState();
            this.GetZone(1).GetComponent<ZoneLightSystem>().SetState(generator1);
            this.GetZone(2).GetComponent<ZoneLightSystem>().SetState(generator2);
            this.GetZone(3).GetComponent<ZoneLightSystem>().SetState(generator3);
            this.GetZone(4).GetComponent<ZoneLightSystem>().SetState(generator4);

            // Monsters
            List<MonsterData> monsters_zone1, monsters_zone2, monsters_zone3, monsters_zone4;
            (monsters_zone1, monsters_zone2, monsters_zone3, monsters_zone4) = this.gameState.GetMonstersInfo();
            this.GetZone(1).GetComponent<Spawner>().SpawnMonsters(monsters_zone1);
            this.GetZone(2).GetComponent<Spawner>().SpawnMonsters(monsters_zone2);
            this.GetZone(3).GetComponent<Spawner>().SpawnMonsters(monsters_zone3);
            this.GetZone(4).GetComponent<Spawner>().SpawnMonsters(monsters_zone4);

            // Animals
            List<AnimalData> animals_zone1, animals_zone2, animals_zone3, animals_zone4;
            (animals_zone1, animals_zone2, animals_zone3, animals_zone4) = this.gameState.GetAnimalsInfo();
            this.GetZone(1).GetComponent<Spawner>().SpawnAnimals(animals_zone1);
            this.GetZone(2).GetComponent<Spawner>().SpawnAnimals(animals_zone2);
            this.GetZone(3).GetComponent<Spawner>().SpawnAnimals(animals_zone3);
            this.GetZone(4).GetComponent<Spawner>().SpawnAnimals(animals_zone4);
        }
    }

    #region GETTERS
    public (bool, bool, bool, bool) GetGeneratorsState()
    {
        return (
            this.GetZone(1).GetComponent<ZoneLightSystem>().GetState(),
            this.GetZone(2).GetComponent<ZoneLightSystem>().GetState(),
            this.GetZone(3).GetComponent<ZoneLightSystem>().GetState(),
            this.GetZone(4).GetComponent<ZoneLightSystem>().GetState()
        );
    }

    public void SaveMonstersInfo()
    {
        this.GetZone(1).GetComponent<Spawner>().SaveMonsters();
        this.GetZone(2).GetComponent<Spawner>().SaveMonsters();
        this.GetZone(3).GetComponent<Spawner>().SaveMonsters();
        this.GetZone(4).GetComponent<Spawner>().SaveMonsters();
    }
    
    public void SaveAnimalsInfo()
    {
        this.GetZone(1).GetComponent<Spawner>().SaveAnimals();
        this.GetZone(2).GetComponent<Spawner>().SaveAnimals();
        this.GetZone(3).GetComponent<Spawner>().SaveAnimals();
        this.GetZone(4).GetComponent<Spawner>().SaveAnimals();
    }

    public (Vector3, Vector3) GetSunInfo()
    {
        return this.lightingManager.GetSunPosition();
    }

    public (Vector3, Vector3, float, float) GetPlayerInfo()
    {
        return (this.player.GetPosition(), this.player.GetRotation(), this.player.GetHealth(), this.player.GetSanity());
    }
    #endregion
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
