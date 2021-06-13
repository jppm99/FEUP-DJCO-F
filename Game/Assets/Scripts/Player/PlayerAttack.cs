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

    [FMODUnity.EventRef]
    public string attackSound;
    private FMOD.Studio.EventInstance attackSoundInstance;

    [FMODUnity.EventRef]
    public string weaponAttackSound;
    private FMOD.Studio.EventInstance weaponAttackSoundInstance;


    GameObject attackspotMonster;
    GameObject attackspotAnimal;

    void Start()
    {
        playerAnimator = GameObject.Find("PlayerBody").GetComponent<Animator>();
        attackspotMonster = GameObject.Find("Attack Spot Monster");
        attackspotAnimal = GameObject.Find("Attack Spot Animal");

        attackSoundInstance = FMODUnity.RuntimeManager.CreateInstance(attackSound);
        weaponAttackSoundInstance = FMODUnity.RuntimeManager.CreateInstance(weaponAttackSound);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(attackSoundInstance, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(weaponAttackSoundInstance, GetComponent<Transform>(), GetComponent<Rigidbody>());

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0) return;

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
                    if (attackDamage == 5)
                        attackSoundInstance.start();
                    else
                        weaponAttackSoundInstance.start();

                    MonsterLife enemy = ray.transform.GetComponent<MonsterLife>();
                    enemy.damage(attackDamage);
                }
                else
                    attackSoundInstance.start();
            }
            else if (Physics.Raycast(attackspotAnimal.transform.position, transform.TransformDirection(Vector3.forward), out ray, attackDistance))
            {
                string objectTag = ray.transform.tag;

                if (objectTag.Equals("Animal"))
                {
                    if (attackDamage == 5)
                        attackSoundInstance.start();
                    else
                        weaponAttackSoundInstance.start();

                    AnimalLife animal = ray.transform.GetComponentInParent<AnimalLife>();
                    animal.damage(attackDamage);
                }
                else
                    attackSoundInstance.start();
            }
            else
                attackSoundInstance.start();



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
