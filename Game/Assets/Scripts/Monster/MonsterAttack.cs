using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public float atackRadius;
    public float atackInterval;
    public int atackDamage;

    private GameObject player;
    private Transform playerTransform;

    private IEnumerator coroutine;

    void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.transform;

        coroutine = atackPlayerIfPossible(atackInterval);

        StartCoroutine(coroutine);
    }

    private IEnumerator atackPlayerIfPossible(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            float dist = Vector3.Distance(playerTransform.position, transform.position);

            if (dist < atackRadius)
            {
                PlayerLife life = player.GetComponent<PlayerLife>();

                life.damage(atackDamage);
            } 
        }
    }
}
