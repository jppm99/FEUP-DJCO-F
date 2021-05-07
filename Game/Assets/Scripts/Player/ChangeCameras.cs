using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameras : MonoBehaviour
{

    private Camera firstPersonCamera;
    private Camera thirdPersonCamera;

    // Start is called before the first frame update
    void Start()
    {
        firstPersonCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        thirdPersonCamera = GameObject.Find("Second Camera").GetComponent<Camera>();

        thirdPersonCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            firstPersonCamera.enabled = !firstPersonCamera.enabled;
            thirdPersonCamera.enabled = !thirdPersonCamera.enabled;
        }
    }
}
