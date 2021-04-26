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
    private float healthLossDelay;

    [SerializeField]
    private float maxSanity;
    private float sanity;
    [SerializeField]
    private float sanityLossDelay;
    private float nextActionTime = 0.0f;

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

        decreaseElementOverTime(ref sanity, sanityLossDelay);

        if (sanity == 0)
        {
            decreaseElementOverTime(ref health, healthLossDelay);

        }

    }

    public void decreaseHealth(float damage)
    {
        health -= damage;
    }

    private void decreaseElementOverTime(ref float element, float delay)
    {
        if (Time.time > nextActionTime && element >= 1)
        {
            element -= 1f;
            nextActionTime += delay;
        }
    }
}
