using System.Collections;
using UnityEngine;

public class RadioTowerLight : MonoBehaviour
{
    [Range(0.1f, 10)] [SerializeField] private float blinkDuration;
    private Light[] lights;
    private bool isTurnedOn = false;

    void Awake()
    {
        this.lights = this.transform.GetComponentsInChildren<Light>();
        this.SetLightState(false);
    }

    public void TurnOn()
    {
        if(isTurnedOn) return;

        isTurnedOn = true;
        StartCoroutine(BlinkingLights(this.blinkDuration));
    }

    private IEnumerator BlinkingLights(float blinkingDuration)
    {
        while(true)
        {
            this.SetLightState(true);

            yield return new WaitForSecondsRealtime(blinkingDuration);

            this.SetLightState(false);

            yield return new WaitForSecondsRealtime(blinkingDuration);
        }
    }

    private void SetLightState(bool isOn)
    {
        foreach(Light light in this.lights)
        {
            light.enabled = isOn;
        }
    }
}
