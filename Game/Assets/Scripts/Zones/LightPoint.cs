using System.Collections;
using UnityEngine;

public class LightPoint : MonoBehaviour
{
    private Light[] lights;
    [SerializeField] private float flickeringDelay, flickeringDuration;
    [SerializeField] [Range(0, 1000)] private float flickeringDurationRandomness;

    void Awake()
    {
        this.lights = this.transform.GetComponentsInChildren<Light>();
    }

    public void SetState(bool isOn)
    {
        if(isOn)
        {
            float duration = this.flickeringDuration * ((Random.Range(-this.flickeringDurationRandomness, this.flickeringDurationRandomness) / 200) + 1);
            StartCoroutine(this.FlickerLights(this.flickeringDelay, duration));
        }
        else
        {
            foreach(Light light in this.lights)
            {
                light.enabled = isOn;
            }
        }
    }

    private IEnumerator FlickerLights(float initialDelay, float flickeringDuration)
    {
        if(flickeringDuration < 0) flickeringDuration = 0;
        
        yield return new WaitForSeconds(initialDelay);

        float currTime = Time.unscaledTime, delay;
        while(Time.unscaledTime < currTime + flickeringDuration)
        {
            // ON delay
            delay = Random.Range(0.03f, 0.08f);
            foreach(Light light in this.lights)
            {
                light.enabled = true;
            }
            
            yield return new WaitForSeconds(delay);
            
            // OFF delay
            delay = Random.Range(0.3f, 0.5f);
            foreach(Light light in this.lights)
            {
                light.enabled = false;
            }
            
            yield return new WaitForSeconds(delay);
        }

        // Lights stay ON in the end
        foreach(Light light in this.lights)
        {
            light.enabled = true;
        }
    }

    public bool IsOn()
    {
        return this.lights.Length > 0 && this.lights[0].enabled;
    }
}
