using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public GameObject testItem1;
    public GameObject testItem2;
    public GameObject testItem3;

    Animator playerAnimator;


    bool isWithObject;
    GameObject handItem;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GameObject.Find("PlayerBody").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
