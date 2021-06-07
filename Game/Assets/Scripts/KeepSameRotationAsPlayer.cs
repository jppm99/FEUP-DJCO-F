using UnityEngine;

public class KeepSameRotationAsPlayer : MonoBehaviour
{
    private PlayerAPI player;
    private Vector3 optimizationVector = new Vector3(90, 0, 0);

    void Start()
    {
        player = RuntimeStuff.GetSingleton<PlayerAPI>();
    }

    void Update()
    {
        optimizationVector.y = player.transform.eulerAngles.y;
        transform.eulerAngles = optimizationVector;
    }
}
