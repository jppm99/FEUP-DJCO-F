using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLife : MonoBehaviour
{
    [SerializeField] private int maxHitPoints;
    [SerializeField] private int currentHitPoints;
    [SerializeField] private int regenRate;

    public void damage(int hitPoints)
    {
        // Hitpoints cannnot go below 0
        currentHitPoints = Math.Max(currentHitPoints - hitPoints, 0);

        if(currentHitPoints == 0)
        {
            die();
        }
    }

    private void regen()
    {
        // Hitpoints cannnot go above maximum hitpoints
        currentHitPoints = Math.Max(currentHitPoints + regenRate, maxHitPoints);
    }

    private void die()
    {
        Destroy(transform.root.gameObject);
    }
}
