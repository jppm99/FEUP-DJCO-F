using System;
using System.Collections;
using UnityEngine;

public class AnimalLife : MonoBehaviour
{
    [SerializeField] private int maxHealth, currentHealth, secondsUntilBodyDisapears;
    private AnimalFood interactableScript;

    private void Start() {
        this.interactableScript = this.GetComponent<AnimalFood>();
    }

    public void damage(int damage)
    {
        Debug.Log(currentHealth);

        // Hitpoints cannot go below 0
        currentHealth = Math.Max(currentHealth - damage, 0);

        if(currentHealth <= 0)
        {
            StartCoroutine(die());
        }
    }

    private IEnumerator die()
    {
        this.interactableScript.EnableInteraction();

        yield return new WaitForSeconds(this.secondsUntilBodyDisapears);

        Destroy(this.gameObject);
    }
}
