using UnityEngine;

public class LightPoint : MonoBehaviour
{
    private Light[] lights;

    void Start()
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
