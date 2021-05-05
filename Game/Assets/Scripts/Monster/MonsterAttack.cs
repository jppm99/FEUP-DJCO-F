using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public float atackRadius;
    public float atackInterval;
    public int atackDamage;
    private bool canAttack = true;

    private GameObject player;
    private Transform playerTransform;
    PlayerLife life;


    void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.transform;
        life = player.GetComponent<PlayerLife>();
    }

    void Update()
    {

        if(canAttack)
        {
            float dist = Vector3.Distance(playerTransform.position, transform.position);

            if (dist < atackRadius)
            {
                atackPlayer();
                StartCoroutine(WaitToAttack(atackInterval));
            }
        }
    }

    void atackPlayer()
    {

        life.decreaseHealth(atackDamage);
    }

    private IEnumerator WaitToAttack(float waitTime)
    {
        canAttack = false;
        yield return new WaitForSeconds(waitTime);
        canAttack = true;

    }
}
