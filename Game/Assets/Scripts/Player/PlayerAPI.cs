using UnityEngine;

public class PlayerAPI : MonoBehaviour, ISingleton
{
    private void Awake() {
        this.register();
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
}
