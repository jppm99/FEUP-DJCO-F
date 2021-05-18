using System;
using System.Collections;
using UnityEngine;

public class AnimalLife : MonoBehaviour
{
    [SerializeField] private int maxHealth, secondsUntilBodyDisapears;
    private int currentHealth;
    private AnimalFood interactableScript;
    Animator animalAnimator;

    private void Start() {
        animalAnimator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        this.interactableScript = this.GetComponentInChildren<AnimalFood>();
    }

    public void damage(int damage)
    {
        // Hitpoints cannot go below 0
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            animalAnimator.SetTrigger("die");
            GetComponent<AnimalMovement>().enabled = false;
            GetComponentInChildren<Rigidbody>().useGravity = false;
            GetComponentInChildren<BoxCollider>().enabled = false;
            this.Die();
        }
    }

    public void Die()
    {
        StartCoroutine(DieCollectable());
    }


    private IEnumerator DieCollectable()
    {
        this.interactableScript.EnableInteraction();

        yield return new WaitForSeconds(this.secondsUntilBodyDisapears);

        Destroy(this.gameObject);
    }
}
