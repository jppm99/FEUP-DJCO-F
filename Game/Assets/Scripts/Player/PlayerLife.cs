using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Image healthImage;
    private Text healthText;

    [SerializeField]
    private float maxHealth;
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        healthImage = GameObject.Find("HealthImage").GetComponent<Image>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthImage.fillAmount = health / maxHealth;
        healthText.text = health.ToString() + " / " + maxHealth.ToString();
    }

    public void decreaseHealth(float damage)
    {
        health -= damage;
    }
}
