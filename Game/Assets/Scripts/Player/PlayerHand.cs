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
    void Update()
    {
        //TODO: Remove this lines after integrated with inventory
        if (Input.GetKeyDown(KeyCode.Alpha1))
            UpdateHandItem("knife");

        if (Input.GetKeyDown(KeyCode.Alpha2))
            UpdateHandItem("catana");

        if (Input.GetKeyDown(KeyCode.Alpha3))
            UpdateHandItem("axe");

        if (Input.GetKeyDown(KeyCode.Alpha4))
            UpdateHandItem("hammer");

    }

    public void UpdateHandItem(string item)
    {
        knifeObject.SetActive(false);
        catanaObject.SetActive(false);
        axeObject.SetActive(false);
        hammerObject.SetActive(false);
        differentAnimation = false;

        int damage = 0;

        if (item == "knife")
        {
            damage = 30;
            knifeObject.SetActive(true);
        }
        else if (item == "catana")
        {
            damage = 50;
            catanaObject.SetActive(true);
            differentAnimation = true;
        }
        else if (item == "axe")
        {
            damage = 40;
            axeObject.SetActive(true);
            differentAnimation = true;
        }
        else if (item == "hammer")
        {
            damage = 20;
            hammerObject.SetActive(true);
            differentAnimation = true;
        }

        GetComponent<PlayerAttack>().setDamage(damage);
        playerAnimator.SetBool("withItem", differentAnimation);
    }
}

