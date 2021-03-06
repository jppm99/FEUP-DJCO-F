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
            this.RemoveMarkerFromMinimap();
            //if (transform.name == "tallMonster")
            //    transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
    }

    void Die()
    {
        StartCoroutine(DieCollectable());
    }

    private IEnumerator DieCollectable()
    {
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        GetComponents<FMODUnity.StudioEventEmitter>()[1].Stop();
        if (this.dropsGeneratorItem)
        {
            this.interactableScript.EnableInteraction();
        }
        else
        {
            yield return new WaitForSeconds(this.secondsUntilBodyDisapears);
            Destroy(this.gameObject);
        }
    }

    private void RemoveMarkerFromMinimap()
    {
        GameObject marker = gameObject.GetComponentInChildren<KeepSameRotationAsPlayer>()?.gameObject;
        if(marker != null && marker.activeInHierarchy) marker.SetActive(false);
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

    void goToGround()
    {

    }
}
