using UnityEngine;

public class KeepSameRotationAsPlayer : MonoBehaviour
{
    private PlayerAPI player;
    private enum axis: int
    {
        X = 0,
        Y = 1,
        Z = 2
    }

    [SerializeField] private Vector3 baseRotation;
    [SerializeField] private axis axisToRotate;

    void Start()
    {
        player = RuntimeStuff.GetSingleton<PlayerAPI>();
    }

    void LateUpdate()
    {
        baseRotation[(int) axisToRotate] = player.transform.eulerAngles.y;
        transform.eulerAngles = baseRotation;
    }
}
