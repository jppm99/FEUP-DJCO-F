
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    private float speed = 10f;
    private float gravity = -30f;
    private float jumpHeigh = 2f;
    private bool canRun = true;
    private bool isMoving = false;

    private Vector3 velocity;

    private Transform groundCheck;
    private bool isGrounded;
    public LayerMask groundMask;

    Animator playerAnimator;
    private float playerSpeed = 0;
    Vector3 lastPosition = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GameObject.Find("PlayerBody").GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        groundCheck = GameObject.Find("Ground Check").transform;

    }

    // Update is called once per frame
    void Update()
    {
        playerSpeed = (new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(lastPosition.x, 0, lastPosition.z)).magnitude;
        lastPosition = transform.position;

        //Debug.Log(playerSpeed);


        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        playerAnimator.SetFloat("VelocityX", x);
        playerAnimator.SetFloat("VelocityY", z);


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeigh * -3f * this.gravity);
            playerAnimator.SetTrigger("Jump");
        }


        if (Input.GetKey(KeyCode.LeftShift) && canRun && (x != 0 || z != 0))
        {
            speed = 8f;
            isMoving = true;
            playerAnimator.SetBool("isRunning", true);
            playerAnimator.SetBool("isWalking", false);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && (x != 0 || z != 0))
        {
            speed = 2.5f;
            isMoving = true;
            playerAnimator.SetBool("isRunning", false);
            playerAnimator.SetBool("isWalking", true);
        }
        else if ((x != 0 || z != 0))
        {
            speed = 5f;
            isMoving = true;
            playerAnimator.SetBool("isRunning", false);
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            speed = 0f;
            isMoving = false;
            playerAnimator.SetBool("isRunning", false);
            playerAnimator.SetBool("isWalking", false);
        }

        if (z < 0)
            playerAnimator.SetFloat("Direction", -1);
        else
            playerAnimator.SetFloat("Direction", 1);


        Vector3 movement = transform.right * x + transform.forward * z;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += this.gravity * Time.deltaTime;

        if (controller.enabled)
        {
            controller.Move(movement * speed * Time.deltaTime);
            controller.Move(velocity * Time.deltaTime);
        }

        //playerAnimator.SetFloat("Velocity", playerSpeed / 1f);
    }

    public void MovePlayer(Vector3 pos, Vector3 rot)
    {
        controller.enabled = false;
        controller.transform.position = pos;
        controller.transform.eulerAngles = rot;
        controller.enabled = true;
    }

    public void setNotBeingAbleToRun(bool canRun)
    {
        this.canRun = !canRun;
    }

}
