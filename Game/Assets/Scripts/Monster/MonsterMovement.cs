using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Transform player_transform;

    private float angle;
    private int updates;
    private int target_updates;
    private bool following;
    private static Vector3 raycast_direction = new Vector3(0, 1, 1);

    [Header("Movement Variables")]
    [SerializeField] public float speed;
    [SerializeField] public float follow_speed;
    [SerializeField]public float rotation_speed;
    [SerializeField] public float movement_range;
    [SerializeField] public float detect_radius;
    
    [Header("Collision Variables")]
    //[SerializeField] public Transform collision_check_transform;
    [SerializeField] public LayerMask ground_mask;
    [SerializeField] public float raycast_distance;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        player_transform = GameObject.Find("Player").transform;
        
        angle = 0;
        updates = 0;
        target_updates = 0;
        following = false;
    }

    // FixedUpdate is called once every physic update
    private void FixedUpdate()
    {
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
            if(dist > GetComponent<MonsterAttack>().getAttackDistance())
                transform.position = Vector3.MoveTowards(transform.position, player_transform.position, Time.deltaTime * follow_speed);
        }
        // If patrolling
        else {
            if (following) {
                following = false;
                UpdateAngle(0);
            }

            Quaternion target = Quaternion.Euler(0, angle, 0);

            // Rotate
            if (Quaternion.Angle(rb.rotation, target) > 1)
                rb.rotation = Quaternion.Slerp(rb.rotation, target, Time.deltaTime * rotation_speed);
            else {
                // Change direction if aproaches collision
                if (Physics.Raycast(transform.position, transform.TransformDirection(raycast_direction), raycast_distance, ground_mask))
                    SetAngle(angle + 30);
                else {
                    // Change direction
                    if (updates == target_updates)
                        UpdateAngle(angle);
                    // Move forward
                    else {
                        transform.position += transform.forward * Time.deltaTime * speed;
                        updates++;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

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