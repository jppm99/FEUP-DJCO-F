using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PlayerLife : MonoBehaviour
{
    Image healthImage;
    Text healthText;
    Image sanityImage;
    Text sanityText;

    public float health;
    public float sanity;
    float nextActionTimeHealth = 0.0f;
    float nextActionTimeSanity = 0.0f;
    bool isDay;
    LensDistortion lensDistortion;
    ChromaticAberration chromaticAberration;
    Vignette vignette;
    PlayerMovement playerMovement;
    GameObject[] lightSources;
    [SerializeField]
    GameObject cabinLight;

    [FMODUnity.EventRef]
    public string healthSound;
    private FMOD.Studio.EventInstance healthSoundInstance;

    [Header("Health Variables")]
    [SerializeField] float maxHealth;
    [SerializeField] float healthLossDelay; //interval of time between losses
    [SerializeField] float healthLossAmount; //loss amount
    [SerializeField] float healthLevelPercent; //level of life where player starts loosing sanity
    [Space(10)]

    [Header("Sanity Variables")]
    [SerializeField] float maxSanity;
    [SerializeField] [Range(0, 1)] float sanityEffectStartsAt; // 0-1 amount of full sanity at which low sanity post processing effect starts at
    [SerializeField] float sanityLossDelay; //interval of time between losses
    // [SerializeField] float sanityLossDelayWithLight; //interval of time between losses when in presence of light
    [SerializeField] float sanityRecoverDelay; //recover amount
    [SerializeField] float lightSourceDistance; //distance from which the player must be from a light source to decrease the amount of sanity that he looses during night time
    float sanityLossAmount = 1; //loss amount
    float sanityRecoverAmount = 1; //recover amount
    private float _time = 0;


    void Awake()
    {
        lightSources = GameObject.FindGameObjectsWithTag("LightSource");

        healthImage = GameObject.Find("HealthImage").GetComponent<Image>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        sanityImage = GameObject.Find("SanityImage").GetComponent<Image>();
        sanityText = GameObject.Find("SanityText").GetComponent<Text>();

        this.playerMovement = gameObject.GetComponent<PlayerMovement>();

        PostProcessVolume volume = Camera.main.GetComponentInChildren<PostProcessVolume>();
        volume.profile.TryGetSettings<LensDistortion>(out this.lensDistortion);
        volume.profile.TryGetSettings<ChromaticAberration>(out this.chromaticAberration);
        volume.profile.TryGetSettings<Vignette>(out this.vignette);

        health = maxHealth;
        sanity = maxSanity;

        healthSoundInstance = FMODUnity.RuntimeManager.CreateInstance(healthSound);
        healthSoundInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        healthSoundInstance.setParameterByName("Health", 100*health/80);


        this._time += Time.deltaTime;

        healthImage.fillAmount = health / maxHealth;
        healthText.text = health.ToString() + " / " + maxHealth.ToString();

        sanityImage.fillAmount = sanity / maxSanity;
        sanityText.text = sanity.ToString() + " / " + maxSanity.ToString();

        //Health will decrease from time to time
        changeElementOverTime(ref health, healthLossDelay, healthLossAmount, maxHealth, ref nextActionTimeHealth, -1);

        if(health <= 0)
        {
            GameObject.FindObjectOfType<MenuUI>().playerHasLost();
        }

        //If health is below a certain level, sanity will decrease from time to time
        if (health <= maxHealth * healthLevelPercent / 100)
        {
            changeElementOverTime(ref sanity, sanityLossDelay, sanityLossAmount, maxSanity, ref nextActionTimeSanity, -1);
            this.playerMovement.setNotBeingAbleToRun(true);
        }

        //If it's daytime, sanity will increase from time to time
        else if (isDay /*|| (Mathf.Abs(cabinLight.transform.position.x - transform.position.x) <= lightSourceDistance && Mathf.Abs(cabinLight.transform.position.z - transform.position.z) <= lightSourceDistance)*/)
        {
            changeElementOverTime(ref sanity, sanityRecoverDelay, sanityRecoverAmount, maxSanity, ref nextActionTimeSanity, 1);
            this.playerMovement.setNotBeingAbleToRun(false);
        }

        //If it's NOT daytime, sanity will decrease from time to time
        else if (!isDay)
        {
            //if the player is close to a light source his sanity will decrease slower
            if (checkCloseLightSources())
                changeElementOverTime(ref sanity, sanityRecoverDelay, sanityRecoverAmount, maxSanity, ref nextActionTimeSanity, 1);
                // changeElementOverTime(ref sanity, sanityLossDelayWithLight, sanityLossAmount, maxSanity, ref nextActionTimeSanity, -1);

            //if the player is NOT close to a light source his sanity will decrease faster
            else
                changeElementOverTime(ref sanity, sanityLossDelay, sanityLossAmount, maxSanity, ref nextActionTimeSanity, -1);

            this.playerMovement.setNotBeingAbleToRun(false);
        }

        this.LowSanityEffect();
    }

    private void LowSanityEffect()
    {
        // If game is running normally and sanity is lower than sanityEffectStartsAt -> effect strength is the percentage of sanity/sanityEffectStartsAt
        float amount = 0;
        if(this.sanity / this.maxSanity < this.sanityEffectStartsAt && Time.timeScale > 0)
        {
            amount = (this.sanity / (this.maxSanity * this.sanityEffectStartsAt)) * -1 + 1;
        }

        float chromaticAberrationMax = 1f;
        float lensDistortionMax = -70f;
        float vignetteMax = 0.5f;

        this.chromaticAberration.intensity.value = amount * chromaticAberrationMax;
        this.lensDistortion.intensity.value = amount * lensDistortionMax;
        this.vignette.intensity.value = amount * vignetteMax;
    }

    public void decreaseHealth(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            healthSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    public void IncreaseHealth(float increase)
    {
        if (health + increase > maxHealth)
            health = maxHealth;
        else
            health = health + increase;

      
    }

    public void setDay(bool isDay)
    {
        this.isDay = isDay;
    }

    //1 to increase, -1 to decrease
    private void changeElementOverTime(ref float element, float delay, float amount, float maxAmount, ref float nextActionTime, int increase)
    {
        if (this._time > nextActionTime && element > 0)
        {
            if(!(increase == 1 && element >= maxAmount))
            {
                element = element + amount * increase;
                nextActionTime += delay;
            }
            
        }
    }

    private bool checkCloseLightSources()
    {
        for(int i = 0; i < lightSources.Length; i++)
        {
            if (Mathf.Abs(lightSources[i].transform.position.x - transform.position.x) <= lightSourceDistance
                && Mathf.Abs(lightSources[i].transform.position.z - transform.position.z) <= lightSourceDistance
                && lightSources[i].GetComponent<LightPoint>().IsOn())
                return true;
        }

        return false;
    }

    public float GetHealth()
    {
        return this.health;
    }
    public void SetHealth(float h)
    {
        this.health = h;
    }
    public float GetSanity()
    {
        return this.sanity;
    }
    public void SetSanity(float s)
    {
        this.sanity = s;
    }
}
