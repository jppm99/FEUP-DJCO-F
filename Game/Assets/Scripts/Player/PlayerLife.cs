using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Image healthImage;
    private Text healthText;

    private Image foodImage;
    private Text foodText;

    [SerializeField]
    private float maxHealth;
    private float health;
    [SerializeField]
    private float healthLossDelay;

    [SerializeField]
    private float maxFood;
    private float food;
    [SerializeField]
    private float foodLossDelay;
    private float nextActionTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        healthImage = GameObject.Find("HealthImage").GetComponent<Image>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        foodImage = GameObject.Find("FoodImage").GetComponent<Image>();
        foodText = GameObject.Find("FoodText").GetComponent<Text>();

        health = maxHealth;
        food = maxFood;
    }

    // Update is called once per frame
    void Update()
    {
        healthImage.fillAmount = health / maxHealth;
        healthText.text = health.ToString() + " / " + maxHealth.ToString();

        foodImage.fillAmount = food / maxFood;
        foodText.text = food.ToString() + " / " + maxFood.ToString();

        decreaseElementOverTime(ref food, foodLossDelay);

        if (food == 0)
            decreaseElementOverTime(ref health, healthLossDelay);

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
