using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLife : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int secondsUntilBodyDisapears;
    [SerializeField] private bool dropsGeneratorItem;
    private MonsterItem interactableScript;
    Animator monsterAnimator;

    private void Start()
    {
        monsterAnimator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        if(this.dropsGeneratorItem) this.interactableScript = this.GetComponentInChildren<MonsterItem>();
    }

    public void damage(int damage)
    {
        // Hitpoints cannnot go below 0
        currentHealth = Math.Max(currentHealth - damage, 0);
        Debug.Log(currentHealth);

        if(currentHealth <= 0)
        {
            monsterAnimator.SetTrigger("die");
            GetComponent<MonsterAttack>().enabled = false;
            GetComponent<MonsterMovement>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    void Die()
    {
        StartCoroutine(DieCollectable());
    }

    private IEnumerator DieCollectable()
    {
        if(this.dropsGeneratorItem) this.interactableScript.EnableInteraction();

        yield return new WaitForSeconds(this.secondsUntilBodyDisapears);

        
        Destroy(this.gameObject);
    }
}
