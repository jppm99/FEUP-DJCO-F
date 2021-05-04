using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int attackDistance;
    [SerializeField] private int attackDamage;

    Animator playerAnimator;


    GameObject playerCamera;

    void Start()
    {
        playerAnimator = GameObject.Find("PlayerBody").GetComponent<Animator>();
        playerCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimator.SetTrigger("Attack");

            RaycastHit ray;

            /* In the future a layermask must be added as a parameter to this function */
            if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out ray, attackDistance))
            {
                string objectTag = ray.transform.tag;

                if(objectTag.Equals("monster"))
                {
                    MonsterLife enemy = ray.transform.GetComponent<MonsterLife>();

                    enemy.damage(attackDamage);
                }
            }
        }
    }
}
