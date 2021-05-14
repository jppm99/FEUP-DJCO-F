using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : ISingleton
{
    CinemachineVirtualCamera firstPersonCam, thirdPersonCam;
    bool isFirstPerson;

    public CameraManager()
    {
        this.register();
    }

    public void RegisterPlayerCam(GameObject cameraHolder, bool isFirstPerson)
    {
        CinemachineVirtualCamera vcam = cameraHolder.GetComponent<CinemachineVirtualCamera>();

        if(isFirstPerson) this.firstPersonCam = vcam;
        else this.thirdPersonCam = vcam;

        if(this.firstPersonCam != null && this.thirdPersonCam != null) this.Init();
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

    /**
     * DON'T USE
     * This shouldn't be public but it must be so that the interface enforces it's existence
     */
    public void register()
    {
        RuntimeStuff.AddSingleton<CameraManager>(this);
    }
}
