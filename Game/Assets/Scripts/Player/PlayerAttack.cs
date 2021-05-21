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


    GameObject attackspotMonster;
    GameObject attackspotAnimal;

    void Start()
    {
        playerAnimator = GameObject.Find("PlayerBody").GetComponent<Animator>();
        attackspotMonster = GameObject.Find("Attack Spot Monster");
        attackspotAnimal = GameObject.Find("Attack Spot Animal");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            playerAnimator.SetTrigger("Attack");

            //Debug.DrawRay(attackspot.transform.position, transform.TransformDirection(Vector3.forward));

            RaycastHit ray;

            if (Physics.Raycast(attackspotMonster.transform.position, transform.TransformDirection(Vector3.forward), out ray, attackDistance))
            {
                string objectTag = ray.transform.tag;

                if (objectTag.Equals("Monster"))
                {
                    MonsterLife enemy = ray.transform.GetComponent<MonsterLife>();
                    enemy.damage(attackDamage);
                }
            }

            if (Physics.Raycast(attackspotAnimal.transform.position, transform.TransformDirection(Vector3.forward), out ray, attackDistance))
            {
                string objectTag = ray.transform.tag;

                if (objectTag.Equals("Animal"))
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

    public void setDamage(int damage)
    {
        this.attackDamage = damage;
    }
}
