using UnityEngine;

public class ZoneLightSystem : MonoBehaviour
{
    [SerializeField]
    private Transform zoneLightsParent;
    private LightPoint[] lights;
    private bool state = false;

    void Start()
    {
        this.lights = this.zoneLightsParent.GetComponentsInChildren<LightPoint>();
        this.SetState(this.state);
    }

    public void SetState(bool state)
    {
        this.state = state;

        this.SetLights(state);
    }

    private void SetLights(bool state)
    {
        foreach(LightPoint light in this.lights)
        {
            light.SetState(state);
        }
    }

    public void SetDaytime(bool isDay)
    {
        if(this.state == false) return;

        this.SetLights(!isDay);
    }

    public bool GetState()
    {
        return this.state;
    }
}
