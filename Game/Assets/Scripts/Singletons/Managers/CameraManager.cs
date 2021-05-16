using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class CameraManager : ISingleton
{
    CinemachineVirtualCamera firstPersonCam, thirdPersonCam;
    GameObject[] zonesCutscenes = new GameObject[4];
    bool isFirstPerson;

    public CameraManager()
    {
        this.register();
    }

    public void RegisterPlayerCam(GameObject virtualCamera, bool isFirstPerson)
    {
        CinemachineVirtualCamera vcam = virtualCamera.GetComponentInChildren<CinemachineVirtualCamera>();
        
        if(isFirstPerson) this.firstPersonCam = vcam;
        else this.thirdPersonCam = vcam;

        if(this.firstPersonCam != null && this.thirdPersonCam != null) this.Init();
    }

    public void RegisterCutsceneCam(GameObject virtualCamera, int zone)
    {
        this.zonesCutscenes[zone - 1] = virtualCamera;
    }

    private void Init()
    {
        if(this.firstPersonCam.Priority > this.thirdPersonCam.Priority) this.isFirstPerson = true;
        else if(this.firstPersonCam.Priority < this.thirdPersonCam.Priority) this.isFirstPerson = false;
        else
        {
            this.isFirstPerson = true;
            this.firstPersonCam.Priority += 10;
        }
    }

    public void SwitchPlayerCameras()
    {
        this.isFirstPerson = !this.isFirstPerson;
        
        if(this.isFirstPerson)
        {
            this.firstPersonCam.Priority = 20;
            this.thirdPersonCam.Priority = 15;
        } else
        {
            this.firstPersonCam.Priority = 15;
            this.thirdPersonCam.Priority = 20;
        }
    }

    public void PlayCutscene(int zone)
    {
        Debug.Log("playing cutscene for zone " + zone);
        Time.timeScale = 0;
        this.zonesCutscenes[zone - 1].GetComponent<CinemachineVirtualCamera>().Priority = 100;
        this.zonesCutscenes[zone - 1].GetComponent<PlayableDirector>().Play();
    }

    public void CutsceneEnded(int zone)
    {
        Time.timeScale = 1;
        this.zonesCutscenes[zone - 1].GetComponent<CinemachineVirtualCamera>().Priority = 0;
    }

    /**
     * DON'T USE
     * This shouldn't be public but it must be so that the interface enforces it's existence
     */
    public void register()
    {
        RuntimeStuff.AddSingleton<CameraManager>(this);
    }
}
