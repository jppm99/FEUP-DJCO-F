using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLife : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public void damage(int damage)
    {
        Debug.Log(currentHealth);

        // Hitpoints cannnot go below 0
        currentHealth = Math.Max(currentHealth - damage, 0);

        if(currentHealth <= 0)
        {
            die();
        }
    }

    private void die()
    {
        Destroy(transform.root.gameObject);
    }
}
