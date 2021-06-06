using UnityEngine;

public class KeepSameRotationAsPlayer : MonoBehaviour
{
    private PlayerAPI player;

    void Start()
    {
        player = RuntimeStuff.GetSingleton<PlayerAPI>();
    }

    void Update()
    {
        float playerYRot = player.transform.eulerAngles.y;
        Vector3 newRot = new Vector3(90, playerYRot, 0);
        transform.eulerAngles = newRot;
    }
}
