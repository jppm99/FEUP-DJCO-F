using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected char actionKey = 'F';
    protected string actionString = "interact";
    protected float maxDistanceToPlayer = 7f;
    protected PlayerAPI player;
    protected TextMesh text;
    protected abstract void Action();
    
    public void Interact()
    {
        if(isCloseEnough()) this.Action();
    }

    protected virtual void Start()
    {
        this.player = RuntimeStuff.GetSingleton<PlayerAPI>();
        this.text = this.transform.GetComponentInChildren<TextMesh>();

        this.text.text = "Press " + this.actionKey + " to " + this.actionString;
    }

    protected void FixedUpdate()
    {
        bool isClose = this.isCloseEnough();

        text.gameObject.SetActive(isClose);

        if(isClose)
        {
            text.transform.LookAt(Camera.main.transform);
            text.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }

    protected bool isCloseEnough()
    {
        // Rough distance to player
        Vector3 playerPosition = this.player.GetPosition();
        float distance = Mathf.Abs(this.transform.position.x - playerPosition.x) + Mathf.Abs(this.transform.position.z - playerPosition.z);

        return distance < this.maxDistanceToPlayer;
    }
}
