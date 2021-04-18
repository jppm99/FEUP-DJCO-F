using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called once every physic update
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}