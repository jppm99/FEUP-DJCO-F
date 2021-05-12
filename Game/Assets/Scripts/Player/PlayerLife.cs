using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    Image healthImage;
    Text healthText;
    Image sanityImage;
    Text sanityText;

    float health;
    float sanity;
    float nextActionTimeHealth = 0.0f;
    float nextActionTimeSanity = 0.0f;
    bool isDay;
    GameObject[] lightSources;

    [Header("Health Variables")]
    [SerializeField] float maxHealth;
    [SerializeField] float healthLossDelay; //interval of time between losses
    [SerializeField] float healthLossAmount; //loss amount
    [SerializeField] float healthLevelPercent; //level of life where player starts loosing sanity
    [Space(10)]

    [Header("Sanity Variables")]
    [SerializeField] float maxSanity;
    [SerializeField] float sanityLossDelay; //interval of time between losses
    [SerializeField] float sanityLossAmount; //loss amount
    [SerializeField] float sanityLossAmountWithLight; //loss amount when in presence of light
    [SerializeField] float sanityRecoverDelay; //recover amount
    [SerializeField] float sanityRecoverAmount; //recover amount
    [SerializeField] float lightSourceDistance; //distance from which the player must be from a light source to decrease the amount of sanity that he looses during night time

    

    // Start is called before the first frame update
    void Start()
    {
        lightSources = GameObject.FindGameObjectsWithTag("LightSource");

        healthImage = GameObject.Find("HealthImage").GetComponent<Image>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        sanityImage = GameObject.Find("SanityImage").GetComponent<Image>();
        sanityText = GameObject.Find("SanityText").GetComponent<Text>();

        health = maxHealth;
        sanity = maxSanity;
    }

    // Update is called once per frame
    void Update()
    {
        healthImage.fillAmount = health / maxHealth;
        healthText.text = health.ToString() + " / " + maxHealth.ToString();

        sanityImage.fillAmount = sanity / maxSanity;
        sanityText.text = sanity.ToString() + " / " + maxSanity.ToString();

        //Health will decreaxse from time to time
        changeElementOverTime(ref health, healthLossDelay, healthLossAmount, maxHealth, ref nextActionTimeHealth, -1);

        //If health is below a certain level, sanity will decrease from time to time
        if (health <= maxHealth * healthLevelPercent / 100)
        {
            changeElementOverTime(ref sanity, sanityLossDelay, sanityLossAmount, maxSanity, ref nextActionTimeSanity, -1);
            gameObject.GetComponent<PlayerMovement>().setNotBeingAbleToRun(true);
        }

        //If it's daytime, sanity will increase from time to time
        else if (isDay)
        {
            changeElementOverTime(ref sanity, sanityRecoverDelay, sanityRecoverAmount, maxSanity, ref nextActionTimeSanity, 1);
            gameObject.GetComponent<PlayerMovement>().setNotBeingAbleToRun(false);
        }

        //If it's NOT daytime, sanity will decrease from time to time
        else if (!isDay)
        {
            //if the player is close to a light source his sanity will decrease slower
            if (checkCloseLightSources())
                changeElementOverTime(ref sanity, sanityLossDelay, sanityLossAmountWithLight, maxSanity, ref nextActionTimeSanity, -1);

            //if the player is NOT close to a light source his sanity will decrease faster
            else
                changeElementOverTime(ref sanity, sanityLossDelay, sanityLossAmount, maxSanity, ref nextActionTimeSanity, -1);

            gameObject.GetComponent<PlayerMovement>().setNotBeingAbleToRun(false);
        }

        //TODO: Add post processing filter that relates sanity level with camera distortion

    }

    public void decreaseHealth(float damage)
    {
        health -= damage;
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
        if (Time.time > nextActionTime && element > 0)
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
                && Mathf.Abs(lightSources[i].transform.position.z - transform.position.z) <= lightSourceDistance)
                return true;
        }

        return false;
    }
}
