using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField]
    private int zone;

    private void Start() {
        RuntimeStuff.GetSingleton<GameManager>().RegisterZone(this.gameObject, this.zone);
    }

    public int GetZone()
    {
        return this.zone;
    }
}
