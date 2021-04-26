using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Image healthImage;
    private Text healthText;

    private Image sanityImage;
    private Text sanityText;

    [SerializeField]
    private float maxHealth;
    private float health;
    [SerializeField]
    private float healthLossDelay; //interval of time between losses
    [SerializeField]
    private float healthLossAmount; //loss amount
    [SerializeField]
    private float healthLevelPercent; //level of life where player starts loosing sanity
    private float nextActionTimeSanity = 0.0f;

    [SerializeField]
    private float maxSanity;
    private float sanity;
    [SerializeField]
    private float sanityLossDelay; //interval of time between losses
    [SerializeField]
    private float sanityLossAmount; //loss amount
    private float nextActionTimeHealth = 0.0f;

    private bool isDay;

    // Start is called before the first frame update
    void Start()
    {
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
        changeElementOverTime(ref health, healthLossDelay, healthLossAmount, ref nextActionTimeHealth, -1);

        //If health is below a certain level, sanity will decreaxse from time to time
        if (health <= maxHealth * healthLevelPercent/100)
            changeElementOverTime(ref sanity, sanityLossDelay, sanityLossAmount, ref nextActionTimeSanity, -1);

        //If it's daytime, sanity will increase from time to time
        else if(isDay)
            changeElementOverTime(ref sanity, sanityLossDelay, sanityLossAmount, ref nextActionTimeSanity, 1);

        //If it's not daytime, sanity will decrease from time to time
        else if (!isDay)
            changeElementOverTime(ref sanity, sanityLossDelay, sanityLossAmount, ref nextActionTimeSanity, -1);

    }

    public void decreaseHealth(float damage)
    {
        health -= damage;
    }

    public void setDay(bool isDay)
    {
        this.isDay = isDay;
    }

    //1 to increase, -1 to decrease
    private void changeElementOverTime(ref float element, float delay, float amount, ref float nextActionTime, int increase)
    {
        if (Time.time > nextActionTime && element >= 1)
        {
            element = element + amount * increase;
            nextActionTime += delay;
        }
    }
}
