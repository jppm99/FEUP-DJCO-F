using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int attackDistance;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackDelay = 0.5f;
    private bool canAttack = true;

    Animator playerAnimator;


    GameObject attackspot;

    void Start()
    {
        playerAnimator = GameObject.Find("PlayerBody").GetComponent<Animator>();
        attackspot = GameObject.Find("Attack Spot");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            playerAnimator.SetTrigger("Attack");

            Debug.DrawRay(attackspot.transform.position, transform.TransformDirection(Vector3.forward));

            RaycastHit ray;

            /* In the future a layermask must be added as a parameter to this function */
            if (Physics.Raycast(attackspot.transform.position, transform.TransformDirection(Vector3.forward), out ray, attackDistance))
            {
                string objectTag = ray.transform.tag;

                if (objectTag.Equals("Monster"))
                {
                    MonsterLife enemy = ray.transform.GetComponent<MonsterLife>();
                    enemy.damage(attackDamage);
                }
                else if (objectTag.Equals("Animal"))
                {
                    AnimalLife animal = ray.transform.GetComponentInParent<AnimalLife>();
                    animal.damage(attackDamage);
                }
            }
            StartCoroutine(WaitToAttack(attackDelay));
            
        }
    }

    private IEnumerator WaitToAttack(float waitTime)
    {
        canAttack = false;
        yield return new WaitForSeconds(waitTime);
        canAttack = true;

    }
}
