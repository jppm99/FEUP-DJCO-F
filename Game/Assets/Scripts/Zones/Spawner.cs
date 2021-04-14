using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int zone = 0;
    private GameManager gameHandler;
    private void Start() {
        this.gameHandler = RuntimeStuff.GetSingleton<GameManager>();
    }
}
