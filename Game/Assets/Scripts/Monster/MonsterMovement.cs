using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private Rigidbody rb;

    private float angle;
    private int updates;
    private int target_updates;
    private Transform player_transform;

    public float speed;
    public float rotation_speed;
    public float movement_range;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        angle = 0;
        updates = 0;
        target_updates = 0;
        player_transform = GameObject.Find("Player").transform;
    }

    // FixedUpdate is called once every physic update
    private void FixedUpdate()
    {
        float dist = Vector3.Distance(player_transform.position, transform.position);

        Quaternion target = Quaternion.Euler(0, angle, 0);

        if (target != rb.rotation)
            Rotate(target);
        else {
            if (updates == target_updates)
                UpdateAngle();
            else {
                transform.position += transform.forward * Time.deltaTime * speed;
                updates++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Update rotation
    private void Rotate(Quaternion target)
    {
        rb.rotation = Quaternion.Slerp(rb.rotation, target, Time.deltaTime * rotation_speed);
    }

    // Update angle
    private void UpdateAngle()
    {
        float delta = Random.Range(-180.0f, 180.0f);
        
        angle = angle + delta;
        updates = 0;

        target_updates = (int) Random.Range(0.0f, movement_range / speed);
    }
}