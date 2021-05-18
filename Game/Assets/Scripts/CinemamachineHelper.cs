using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemamachineHelper : MonoBehaviour
{
    [SerializeField] bool isPlayerCam, isFirstPerson;
    private CinemachineVirtualCamera virtualCamera;
    private int zone;
    CameraManager cameraManager;
    private void Awake()
    {
        this.cameraManager = RuntimeStuff.GetSingleton<CameraManager>();
        this.virtualCamera = this.GetComponent<CinemachineVirtualCamera>();
        
        // Register to camera manager
        if(this.isPlayerCam) this.cameraManager.RegisterPlayerCam(this.gameObject, this.isFirstPerson);
        else
        {   
            this.zone = this.GetComponentInParent<Zone>().GetZone();
            this.cameraManager.RegisterCutsceneCam(this.gameObject, this.zone);
        }
    }

    public int GetPriority()
    {
        return this.virtualCamera.Priority;
    }
    
    public void SetPriority(int priority)
    {
        this.virtualCamera.Priority = priority;
    }

    public void EndCutscene()
    {
        this.cameraManager.CutsceneEnded(this.zone);
    }
}
