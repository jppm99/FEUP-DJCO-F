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


    }

    public void updateHandItem(string item, int damage)
    {
        knifeObject.SetActive(false);
        catanaObject.SetActive(false);
        axeObject.SetActive(false);
        hammerObject.SetActive(false);
        differentAnimation = false;

        if (item == "knife")
            knifeObject.SetActive(true);
        else if (item == "catana")
        {
            catanaObject.SetActive(true);
            differentAnimation = true;
        }
        else if (item == "axe")
        {
            axeObject.SetActive(true);
            differentAnimation = true;
        }
        else if (item == "hammer")
        {
            hammerObject.SetActive(true);
            differentAnimation = true;
        }

        GetComponent<PlayerAttack>().setDamage(damage);
    }
}

