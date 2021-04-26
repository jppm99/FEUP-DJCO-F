using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public Transform center;
    public int secondsPerDay, secondsPerNight;
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    private float maxHeight, daytimeStep, nighttimeStep;
    private bool isDaytime = false;
    private GameManager gameManager;
    private PlayerLife playerLife;

    private void Start()
    {
        playerLife = GameObject.Find("Player").gameObject.GetComponent<PlayerLife>();

        this.maxHeight = Vector3.Distance(this.transform.position, this.center.transform.position);
        this.gameManager = RuntimeStuff.GetSingleton<GameManager>();

        this.daytimeStep = 360f / this.secondsPerDay;
        this.nighttimeStep = 360f / this.secondsPerNight;
    }

    private void FixedUpdate()
    {
        if (Preset == null) return;

        float heightPercentage = this.transform.position.y / this.maxHeight;

        if(heightPercentage < 0 && this.isDaytime)
        {
            playerLife.setDay(false);
            this.isDaytime = false;
            this.gameManager.SetDaytime(this.isDaytime);
        }
        else if(heightPercentage > 0 && !this.isDaytime)
        {
            playerLife.setDay(true);
            this.isDaytime = true;
            this.gameManager.SetDaytime(this.isDaytime);
        }

        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate((heightPercentage + 1) / 2f);
        RenderSettings.fogColor = Preset.FogColor.Evaluate((heightPercentage + 1) / 2f);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate((heightPercentage + 1) / 2f);

            DirectionalLight.transform.RotateAround(this.center.position, new Vector3(0, 0, 1), (Time.fixedDeltaTime * (heightPercentage > 0 ? this.daytimeStep : this.nighttimeStep)));
        }
    }


    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
