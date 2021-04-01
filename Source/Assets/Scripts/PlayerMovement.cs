using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    private float speed = 12f;
    private float gravity = -20f;
    private float jumpHeigh = 3f;

    private Vector3 velocity;

    private Transform groundCheck;
    private bool isGrounded;
    public LayerMask groundMask;


    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        groundCheck = GameObject.Find("Ground Check").transform;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeigh * -2f * gravity);

        Vector3 movement = transform.right * x + transform.forward * z;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(movement * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
}
