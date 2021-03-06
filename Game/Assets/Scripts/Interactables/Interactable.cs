using System.Collections;
using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected string floatingText = "Press F to interact";
    protected float maxDistanceToPlayer = 7f;
    protected PlayerAPI player;
    protected TextMeshPro text;
    protected Camera mainCamera;
    protected bool isClose = false, isActive = true;
    protected MeshRenderer[] meshRenderers;
    protected abstract void Action();
    

    public void Interact()
    {
        if(this.isActive && isCloseEnough()) this.Action();
    }

    protected virtual void Start()
    {
        this.player = RuntimeStuff.GetSingleton<PlayerAPI>();
        
        this.text = this.transform.GetComponentInChildren<TextMeshPro>();

        this.text.text = this.floatingText;

        this.mainCamera = Camera.main;

        this.meshRenderers = this.GetComponentsInChildren<MeshRenderer>();
    }

    protected void UpdateFloatingText(string newText)
    {
        this.floatingText = newText;

        if(this.text == null)
        {
            this.text = this.transform.GetComponentInChildren<TextMeshPro>();
        }
        this.text.text = newText;
    }

    protected virtual void FixedUpdate()
    {
        this.isClose = this.isCloseEnough();
        text.gameObject.SetActive(this.isActive && this.isClose);
    }

    protected void LateUpdate() {
        if(this.isActive && this.isClose && this.text.text != "")
        {
            text.transform.LookAt(this.mainCamera.transform);
            text.transform.rotation = Quaternion.LookRotation(this.mainCamera.transform.forward);
        }
    }

    protected bool isCloseEnough()
    {
        // Rough distance to player
        Vector3 playerPosition = this.player.GetPosition();
        float distance = Mathf.Abs(this.transform.position.x - playerPosition.x) + Mathf.Abs(this.transform.position.z - playerPosition.z);

        return distance < this.maxDistanceToPlayer;
    }

    protected void Disable()
    {
        this.isActive = false;
        foreach(MeshRenderer mr in this.meshRenderers)
        {
            mr.enabled = false;
        }
    }

    protected void Enable()
    {
        foreach(MeshRenderer mr in this.meshRenderers)
        {
            mr.enabled = true;
        }
        this.isActive = true;
    }

    protected IEnumerator DisableForDuration(float duration)
    {
        this.Disable();
        yield return new WaitForSeconds(duration);
        this.Enable();
    }
}
