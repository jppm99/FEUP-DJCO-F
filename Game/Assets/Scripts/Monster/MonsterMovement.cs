using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private Rigidbody rb;

    private int angle;

    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        angle = 0;
    }

    // FixedUpdate is called once every physic update
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(speed, rb.velocity.y, 0);

        Rotate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Update angle
    private void Rotate()
    {
        Quaternion target = Quaternion.Euler(0, angle, 0);
        rb.rotation = Quaternion.Slerp(rb.rotation, target, Time.deltaTime);
    }
}