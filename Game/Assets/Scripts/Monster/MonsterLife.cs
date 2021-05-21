using System;
using System.Collections;
using UnityEngine;

public class MonsterLife : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private int secondsUntilBodyDisapears;
    [SerializeField] private bool dropsGeneratorItem;
    private MonsterItem interactableScript;
    Animator monsterAnimator;

    private void Awake()
    {
        monsterAnimator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        if(this.dropsGeneratorItem) this.interactableScript = this.GetComponentInChildren<MonsterItem>();
    }

    public void damage(int damage)
    {
        // Hitpoints cannot go below 0
        currentHealth = Math.Max(currentHealth - damage, 0);

        if(currentHealth <= 0)
        {
            monsterAnimator.SetTrigger("die");
            GetComponent<MonsterAttack>().enabled = false;
            GetComponent<MonsterMovement>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

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

    public float GetHealth()
    {
        return this.currentHealth;
    }
    
    public void SetHealth(float h)
    {
        this.currentHealth = h;
        this.damage(0);
    }
}
