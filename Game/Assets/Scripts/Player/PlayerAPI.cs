using UnityEngine;

public class PlayerAPI : MonoBehaviour, ISingleton
{
    private PlayerLife life;

    private void Awake() {
        this.register();

        this.life = this.GetComponent<PlayerLife>();
    }
    
    /**
     * DON'T USE
     * This shouldn't be public but it must be so that the interface enforces it's existence
     */
    public void register()
    {
        RuntimeStuff.AddSingleton<PlayerAPI>(this);
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }
    
    public Vector3 GetRotation()
    {
        return this.transform.eulerAngles;
    }
    
    public void SetPosition(Vector3 p)
    {
        this.transform.position = p;
    }
    
    public void SetRotation(Vector3 r)
    {
        this.transform.eulerAngles = r;
    }

    public float GetHealth()
    {
        return this.life.GetHealth();
    }
    public void SetHealth(float h)
    {
        this.life.SetHealth(h);
    }
    public float GetSanity()
    {
        return this.life.GetSanity();
    }
    public void SetSanity(float s)
    {
        this.life.SetSanity(s);
    }
}
