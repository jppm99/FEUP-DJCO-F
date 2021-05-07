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

    private void Start()
    {
        if(this.dropsGeneratorItem) this.interactableScript = this.GetComponentInChildren<MonsterItem>();
    }

    public void damage(int damage)
    {
        Debug.Log(currentHealth);

        // Hitpoints cannnot go below 0
        currentHealth = Math.Max(currentHealth - damage, 0);

        if(currentHealth <= 0)
        {
            StartCoroutine(die());
        }
    }

    private IEnumerator die()
    {
        if(this.dropsGeneratorItem) this.interactableScript.EnableInteraction();

        yield return new WaitForSeconds(this.secondsUntilBodyDisapears);

        Destroy(this.gameObject);
    }
}
