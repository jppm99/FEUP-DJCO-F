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
    public float detect_radius;


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
        float step = Time.deltaTime * speed;

        float dist = Vector3.Distance(player_transform.position, transform.position);

        // If following player
        if (dist < detect_radius) {
            // Face the player
            Vector3 target_direction = player_transform.position - transform.position;
            
            Vector3 new_direction = Vector3.RotateTowards(transform.forward, target_direction, step, 0.0f);
            
            rb.rotation = Quaternion.LookRotation(new_direction);

            // Move towards player
            transform.position = Vector3.MoveTowards(transform.position, player_transform.position, step);
        }
        // If patrolling
        else {
            Quaternion target = Quaternion.Euler(0, angle, 0);

            if (target != rb.rotation)
                rb.rotation = Quaternion.Slerp(rb.rotation, target, Time.deltaTime * rotation_speed);
            else {
                if (updates == target_updates)
                    UpdateAngle();
                else {
                    transform.position += transform.forward * step;
                    updates++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

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