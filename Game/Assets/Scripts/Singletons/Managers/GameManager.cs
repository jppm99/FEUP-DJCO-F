using UnityEngine;

public class GameManager : MonoBehaviour, ISingleton
{
    public bool isDaytime = false;
    private GameObject[] zones = new GameObject[4];
    void Awake()
    {
        this.register();
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
    public void TurnOnZoneLights(int zone)
    {
        this.GetZone(zone).GetComponent<ZoneLightSystem>().SetState(true);
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
