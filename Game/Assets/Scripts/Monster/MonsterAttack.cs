using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public float atackRadius;
    public float atackInterval;
    public int atackDamage;
    private float timeSinceLastAtack;

    private GameObject player;
    private Transform playerTransform;

    void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.transform;
        timeSinceLastAtack = 1f;
    }

    /**
     * Every physics update, increments a counter that starts with the atack interval value,
     * this allows the monster to atack as soon as it gets near enough to the player. When 
     * the monster atacks, the counter resets and it only atacks after the defined
     * atackInterval
     */
    void FixedUpdate()
    {
        timeSinceLastAtack = Math.Min(atackInterval, timeSinceLastAtack + Time.fixedDeltaTime);

        if(timeSinceLastAtack >= atackInterval)
        {
            float dist = Vector3.Distance(playerTransform.position, transform.position);

            if (dist < atackRadius)
            {
                atackPlayer();
                timeSinceLastAtack = 0;
            }
        }
    }

    void atackPlayer()
    {
        PlayerLife life = player.GetComponent<PlayerLife>();

        life.decreaseHealth(atackDamage);
    }
}
