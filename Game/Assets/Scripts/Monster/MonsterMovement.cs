using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Transform player_transform;

    private float angle;
    private int updates;
    private int target_updates;
    private bool following;
    private bool obstacle_collision;

    public Transform collision_check_transform;
    public LayerMask ground_mask;

    public float speed;
    public float rotation_speed;
    public float follow_speed;
    public float movement_range;
    public float detect_radius;
    public float collision_radius;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player_transform = GameObject.Find("Player").transform;
        
        angle = 0;
        updates = 0;
        target_updates = 0;
        following = false;
        obstacle_collision = false;
    }

    // FixedUpdate is called once every physic update
    private void FixedUpdate()
    {
        if (obstacle_collision)
            print("collision my g");

        float dist = Vector3.Distance(player_transform.position, transform.position);

        // If following player
        if (dist < detect_radius) {
            following = true;

            // Face the player
            Vector3 target_direction = player_transform.position - transform.position;
            Vector3 target_direction_x_z = new Vector3(target_direction.x, 0, target_direction.z);

            Vector3 new_direction = Vector3.RotateTowards(transform.forward, target_direction_x_z, Time.deltaTime * follow_speed, 0.0f);
            
            rb.rotation = Quaternion.LookRotation(new_direction);

            // Move towards player
            transform.position = Vector3.MoveTowards(transform.position, player_transform.position, Time.deltaTime * follow_speed);
        }
        // If patrolling
        else {
            if (following) {
                following = false;
                UpdateAngle(0);
            }

            if (obstacle_collision) {
                float r = Random.Range(-1.0f, 1.0f);

                if (r > 0)
                    SetAngle(angle + 90);
                else
                    SetAngle(angle - 90);
            }

            Quaternion target = Quaternion.Euler(0, angle, 0);

            // Rotate
            if (target != rb.rotation)
                rb.rotation = Quaternion.Slerp(rb.rotation, target, Time.deltaTime * rotation_speed);
            // Move forward
            else {
                // Change direction
                if (updates == target_updates)
                    UpdateAngle(angle);
                else {
                    transform.position += transform.forward * Time.deltaTime * speed;
                    updates++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hit_colliders = Physics.OverlapSphere(collision_check_transform.position, collision_radius, ground_mask);

        if (hit_colliders.Length > 0)
            obstacle_collision = true;
        else
            obstacle_collision = false;
    }

    // Update angle
    private void UpdateAngle(float angle)
    {
        float delta = Random.Range(-180.0f, 180.0f);
        
        SetAngle(angle + delta);
    }

    // Set angle
    private void SetAngle(float angle) {
        this.angle = angle;

        // Reset updates and set new target updates
        updates = 0;
        target_updates = (int) Random.Range(0.0f, movement_range / speed);
    }
}