using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private float mouseSensibility = 100f;
    private float verticalRotation;
    public float cameraDistance = 5;

    private Transform player;
    private Transform cameraTarget;

    private Transform firstPersonCameraHolder; //camera 1
    private Transform thirdPersonCameraHolder; //camera 2
    private int currentCamera;
    private CameraManager cameraManager;

    // Start is called before the first frame update
    void Start()
    {
        this.cameraManager = RuntimeStuff.GetSingleton<CameraManager>();

        player = GameObject.Find("Player").transform;
        cameraTarget = GameObject.Find("Camera Target").transform;


        firstPersonCameraHolder = GameObject.Find("First Person Camera Holder").transform;
        // thirdPersonCameraHolder = GameObject.Find("Third Person Camera Holder").transform;
        currentCamera = 2;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensibility * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensibility * Time.deltaTime;

        verticalRotation -= mouseY;

        Transform rotationTransform = transform;

        // if (currentCamera == 1)
        // {
        //     verticalRotation = Mathf.Clamp(verticalRotation, -35f, 95f);
        //     rotationTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        // }
        // else if (currentCamera == 2)
        // {
        //     verticalRotation = Mathf.Clamp(verticalRotation, -10f, 60f);
        //     rotationTransform.eulerAngles = new Vector3(verticalRotation, rotationTransform.eulerAngles.y);

        //     rotationTransform.position = cameraTarget.position - rotationTransform.forward * cameraDistance;
        // }

        player.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(KeyCode.C)) this.cameraManager.SwitchPlayerCameras();

        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     if(currentCamera == 1)
        //     {
        //         transform.SetParent(thirdPersonCameraHolder);
        //         currentCamera = 2;
        //     }
        //     else if (currentCamera == 2)
        //     {
        //         transform.SetParent(firstPersonCameraHolder);
        //         currentCamera = 1;
        //     }

        //     transform.localPosition = new Vector3(0, 0, 0);
        //     transform.localScale = new Vector3(1, 1, 1);
        //     transform.localRotation = Quaternion.Euler(0, 0, 0);
        // }
    }
}
