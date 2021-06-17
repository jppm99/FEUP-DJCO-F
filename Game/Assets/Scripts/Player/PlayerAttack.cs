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
    // GameObject attackspotAnimal;

    void Start()
    {
        playerAnimator = GameObject.Find("PlayerBody").GetComponent<Animator>();
        attackspotMonster = GameObject.Find("Attack Spot Monster");
        // attackspotAnimal = GameObject.Find("Attack Spot Animal");
        playerAnimator.SetFloat("attackSpeedMult", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0) return;

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            if(Time.timeScale == 0) return;

            playerAnimator.SetTrigger("Attack");

            //Debug.DrawRay(attackspot.transform.position, transform.TransformDirection(Vector3.forward));

            RaycastHit ray;

            if (Physics.Raycast(attackspotMonster.transform.position, transform.TransformDirection(Vector3.forward), out ray, attackDistance))
            {
                string objectTag = ray.transform.tag;

                if (objectTag.Equals("Monster"))
                {
                    GetComponents<FMODUnity.StudioEventEmitter>()[1].Play();

                    MonsterLife enemy = ray.transform.GetComponent<MonsterLife>();
                    enemy.damage(attackDamage);
                }
                else if (objectTag.Equals("Animal"))
                {
                    GetComponents<FMODUnity.StudioEventEmitter>()[1].Play();

                    AnimalLife animal = ray.transform.GetComponentInParent<AnimalLife>();
                    animal.damage(attackDamage);
                }
                else
                    GetComponents<FMODUnity.StudioEventEmitter>()[0].Play();
            }
            // else if (Physics.Raycast(attackspotAnimal.transform.position, transform.TransformDirection(Vector3.forward), out ray, attackDistance))
            // {
            //     string objectTag = ray.transform.tag;

            //     if (objectTag.Equals("Animal"))
            //     {
            //         if (attackDamage == 4)
            //             GetComponents<FMODUnity.StudioEventEmitter>()[0].Play();
            //         else
            //             GetComponents<FMODUnity.StudioEventEmitter>()[1].Play();

            //         AnimalLife animal = ray.transform.GetComponentInParent<AnimalLife>();
            //         animal.damage(attackDamage);
            //     }
            //     else
            //         GetComponents<FMODUnity.StudioEventEmitter>()[0].Play();
            // }
            else
                GetComponents<FMODUnity.StudioEventEmitter>()[0].Play();



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
    
    public void setDelay(float delay)
    {
        this.attackDelay = delay;
    }
}
