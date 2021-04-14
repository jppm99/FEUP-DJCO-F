using UnityEngine;

public class GameManager : MonoBehaviour, ISingleton
{
    public float timeOfDay = 10;
    public GameManager()
    {
        this.register();


    }

    public bool IsItDaytime()
    {
        //TODO
        return timeOfDay > 5;
    }





    /**
     * DON'T USE
     * This shouldn't be public but it must be so that the interface enforces it's existence
     */
    public void register()
    {
        RuntimeStuff.AddSingleton<GameManager>(this);
    }
}
