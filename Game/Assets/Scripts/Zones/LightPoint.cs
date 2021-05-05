using UnityEngine;

public class LightPoint : MonoBehaviour
{
    private Light[] lights;

    void Awake()
    {
        this.lights = this.transform.GetComponentsInChildren<Light>();
    }

    public void SetState(bool isOn)
    {
        foreach(Light light in this.lights)
        {
            light.enabled = isOn;
        }
    }
}
