using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public GameObject knifeObject;
    public GameObject catanaObject;
    public GameObject axeObject;
    public GameObject hammerObject;

    Animator playerAnimator;
    bool differentAnimation;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GameObject.Find("PlayerBody").GetComponent<Animator>();

    }

    // Update is called once per frame
    // void Update()
    // {
    //     //TODO: Remove this lines after integrated with inventory
    //     if (Input.GetKeyDown(KeyCode.Alpha1))
    //         UpdateHandItem("knife");

    //     if (Input.GetKeyDown(KeyCode.Alpha2))
    //         UpdateHandItem("catana");

    //     if (Input.GetKeyDown(KeyCode.Alpha3))
    //         UpdateHandItem("axe");

    //     if (Input.GetKeyDown(KeyCode.Alpha4))
    //         UpdateHandItem("hammer");

    // }

    public void UpdateHandItem(string item)
    {
        knifeObject.SetActive(false);
        catanaObject.SetActive(false);
        axeObject.SetActive(false);
        hammerObject.SetActive(false);
        differentAnimation = false;

        int damage = 4;
        float delay = 0.5f, mult = 1;

        if (item == "knife")
        {
            // 12 damage per second
            damage = 6;
            delay = 0.5f;
            knifeObject.SetActive(true);
        }
        else if (item == "catana")
        {
            // 15 damage per second
            damage = 12;
            delay = 0.8f;
            catanaObject.SetActive(true);
            differentAnimation = true;
        }
        else if (item == "axe")
        {
            // 20 damage per second
            damage = 30;
            delay = 1.5f;
            mult = 0.9f;
            axeObject.SetActive(true);
            differentAnimation = true;
        }
        else if (item == "hammer")
        {
            // 14 damage per second
            damage = 14;
            delay = 1f;
            mult = 0.95f;
            hammerObject.SetActive(true);
            differentAnimation = true;
        }

        GetComponent<PlayerAttack>().setDamage(damage);
        GetComponent<PlayerAttack>().setDelay(delay);

        playerAnimator.SetFloat("attackSpeedMult", mult);
        playerAnimator.SetBool("withItem", differentAnimation);
    }
}

