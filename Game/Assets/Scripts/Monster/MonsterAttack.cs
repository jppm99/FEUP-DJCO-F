using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public float atackDistance;
    public float atackInterval;
    public int atackDamage;
    private bool canAttack = true;

    private GameObject player;
    private Transform playerTransform;
    PlayerLife life;

    Animator monsterAnimator;


    void Start()
    {
        monsterAnimator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player");
        playerTransform = player.transform;
        life = player.GetComponent<PlayerLife>();
    }

    void FixedUpdate()
    {
        if(canAttack)
        {
            float dist = Vector3.Distance(playerTransform.position, transform.position);

            if (dist < atackDistance)
            {
                monsterAnimator.SetTrigger("attack");
                canAttack = false;
            }
        }
    }


    void AttackPlayer()
    {
        float dist = Vector3.Distance(playerTransform.position, transform.position);

        if (dist < atackDistance)
            life.decreaseHealth(atackDamage);
    }

    void CanAttack()
    {
        canAttack = true;
    }

    public float getAttackDistance()
    {
        return atackDistance;
    }
}
