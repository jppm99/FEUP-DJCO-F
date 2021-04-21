using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLife : MonoBehaviour
{
    [SerializeField] private int maxHitPoints;
    [SerializeField] private int currentHitPoints;
    [SerializeField] private int regenRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damage(int hitPoints)
    {
        // Hitpoints cannnot go below 0
        currentHitPoints = Math.Max(currentHitPoints - currentHitPoints, 0);
    }

    private void regen()
    {
        // Hitpoints cannnot go above maximum hitpoints
        currentHitPoints = Math.Max(currentHitPoints + regenRate, maxHitPoints);
    }
}
