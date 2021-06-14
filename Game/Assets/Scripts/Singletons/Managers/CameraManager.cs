using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class CameraManager : ISingleton
{
    private CinemachineVirtualCamera firstPersonCam, thirdPersonCam;
    private GameObject[] zonesCutscenes = new GameObject[4];
    private GameObject[] finalCutscene = new GameObject[2];
    private Vector3 finalCutscenePlayerPostion = new Vector3(
        555.65f,
        44.87f,
        528.6f
    );
    private Vector3 finalCutscenePlayerRotation = new Vector3(0, -175.62f, 0);
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

    public void RegisterFinalCutsceneCam(GameObject finalCutscene, int part)
    {
        this.finalCutscene[part-1] = finalCutscene;
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

    public void PlayFinalCutscene()
    {
        Time.timeScale = 0;
        this.finalCutscene[0].GetComponent<CinemachineVirtualCamera>().Priority = 200;
        this.finalCutscene[0].GetComponent<PlayableDirector>().Play();
    }

    public void SwitchFinalCutsceneCamera(int part)
    {
        this.finalCutscene[part].GetComponent<CinemachineVirtualCamera>().Priority = 200 - part;
        this.finalCutscene[part-1].GetComponent<CinemachineVirtualCamera>().Priority = 0;
        this.finalCutscene[part].GetComponent<PlayableDirector>().Play();

        RuntimeStuff.GetSingleton<PlayerAPI>().gameObject.GetComponentInChildren<PlayerMovement>()
            .MovePlayer(finalCutscenePlayerPostion, finalCutscenePlayerRotation);
    }

    public void FinalCutsceneEnded(int part)
    {
        Time.timeScale = 1;
        
        // this.finalCutscene[part-1].GetComponent<CinemachineVirtualCamera>().Priority = 0;
    }
    
    public void PlayCutscene(int zone)
    {
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
