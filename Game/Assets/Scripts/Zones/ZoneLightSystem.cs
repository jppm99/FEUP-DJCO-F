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

        foreach(LightPoint light in this.lights)
        {
            light.SetState(state);
        }
    }

    public bool GetState()
    {
        return this.state;
    }
}
