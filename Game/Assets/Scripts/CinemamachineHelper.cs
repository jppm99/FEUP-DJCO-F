using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemamachineHelper : MonoBehaviour
{
    [SerializeField] bool isPlayerCam, isFirstPerson;
    private CinemachineVirtualCamera virtualCamera;
    private void Awake()
    {
        this.virtualCamera = this.GetComponent<CinemachineVirtualCamera>();
        
        if(this.isPlayerCam) RuntimeStuff.GetSingleton<CameraManager>().RegisterPlayerCam(this.gameObject, this.isFirstPerson);
    }

    public int GetPriority()
    {
        return this.virtualCamera.Priority;
    }
    
    public void GetPriority(int priority)
    {
        this.virtualCamera.Priority = priority;
    }
}
