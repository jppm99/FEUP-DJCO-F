using UnityEngine;

public class KeepSamePostitionAsGameObject : MonoBehaviour
{
    [SerializeField] private GameObject target;

    void LateUpdate()
    {
        transform.position = target.transform.position;
    }
}