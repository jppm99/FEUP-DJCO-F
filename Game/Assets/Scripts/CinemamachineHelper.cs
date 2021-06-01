using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemamachineHelper : MonoBehaviour
{
    [SerializeField] bool isPlayerCam, isFirstPerson, isFinalCutscene;
    bool isStopped, hasBeenCheckedForStopping;
    private float maxSpeed;
    private CinemachineVirtualCamera virtualCamera;
    private int zone;
    CameraManager cameraManager;
    private void Awake()
    {
        this.cameraManager = RuntimeStuff.GetSingleton<CameraManager>();
        this.virtualCamera = this.GetComponent<CinemachineVirtualCamera>();
        
        // Register to camera manager
        if(this.isPlayerCam)
        {
            CinemachinePOV pov = this.virtualCamera.GetCinemachineComponent<CinemachinePOV>();
            this.maxSpeed = pov.m_VerticalAxis.m_MaxSpeed;
            this.cameraManager.RegisterPlayerCam(this.gameObject, this.isFirstPerson);
        }
        else
        {
            if(this.isFinalCutscene)
            {
                this.cameraManager.RegisterFinalCutsceneCam(this.gameObject);
            }
            else
            {
                this.zone = this.GetComponentInParent<Zone>().GetZone();
                this.cameraManager.RegisterCutsceneCam(this.gameObject, this.zone);
            }
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
    
    public void EndFinalCutscene()
    {
        this.cameraManager.FinalCutsceneEnded();
    }

    public void ShowWinningScreen()
    {
        GameObject.Find("Canvas").GetComponent<MenuUI>().playerHasWon();
    }

    private void Update() {
        if(!this.isPlayerCam) return;

        if(!this.hasBeenCheckedForStopping)
        {
            this.hasBeenCheckedForStopping = true;
            isStopped = Time.timeScale == 0;
            if(!isStopped)
            {
                CinemachinePOV pov = this.virtualCamera.GetCinemachineComponent<CinemachinePOV>();
                this.maxSpeed = pov.m_VerticalAxis.m_MaxSpeed;
                pov.m_VerticalAxis.m_MaxSpeed = 0;
            }
            // Just resumed
            else
            {
                CinemachinePOV pov = this.virtualCamera.GetCinemachineComponent<CinemachinePOV>();
                pov.m_VerticalAxis.m_MaxSpeed = this.maxSpeed;
            }
        }
        else
        {
            // Paused just now
            if(!isStopped && Time.timeScale == 0)
            {
                isStopped = true;
                CinemachinePOV pov = this.virtualCamera.GetCinemachineComponent<CinemachinePOV>();
                this.maxSpeed = pov.m_VerticalAxis.m_MaxSpeed;
                pov.m_VerticalAxis.m_MaxSpeed = 0;
            }
            // Just resumed
            else if(isStopped == Time.timeScale > 0)
            {
                isStopped = false;
                CinemachinePOV pov = this.virtualCamera.GetCinemachineComponent<CinemachinePOV>();
                pov.m_VerticalAxis.m_MaxSpeed = this.maxSpeed;
            }
        }
    }
}
