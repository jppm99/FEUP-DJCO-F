using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private float mouseSensibility = 100f;
    private Transform player;
    private CameraManager cameraManager;

    void Start()
    {
        this.cameraManager = RuntimeStuff.GetSingleton<CameraManager>();

        player = GameObject.Find("Player").transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensibility * Time.deltaTime;

        player.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(KeyCode.C)) this.cameraManager.SwitchPlayerCameras();
    }
}
