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
    private GameManager gameManager;
    private Transform center;

    [SerializeField] public string type;

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
        gameManager = RuntimeStuff.GetSingleton<GameManager>();
        center = GameObject.Find("Center of terrain").transform;
        
        angle = 0;
        updates = 0;
        target_updates = 0;
        following = false;
    }

    // FixedUpdate is called once every physic update
    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity, ForceMode.Acceleration);

        float dist = Vector3.Distance(player_transform.position, transform.position);
        bool runAway = false;
        int zone = 1;

        if (transform.position.x < center.position.x) {
            if (transform.position.z < center.position.z)
                zone = 2;
        }
        else {
            if (transform.position.z < center.position.z)
                zone = 4;
            else
                zone = 3;
        }

        if (gameManager.IsDaytime() || gameManager.GetLightsState(zone))
            runAway = true;

        // If running away
        if (runAway) {
            // Rotate away from player
            Vector3 facing = player_transform.position - transform.position;
            Vector3 facing_x_z = new Vector3(facing.x, 0, facing.z);

            Quaternion awayRotation = Quaternion.LookRotation(facing_x_z);
            Vector3 euler = awayRotation.eulerAngles;
            euler.y -= 180;
            awayRotation = Quaternion.Euler(euler);

            transform.rotation = Quaternion.Slerp(transform.rotation, awayRotation, rotation_speed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            

            // Move away from player
            transform.position += transform.forward * Time.deltaTime * speed;
        }
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
                // Change direction if approaches collision
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

    public void AddToData(int zone)
    {
        RuntimeStuff.GetSingleton<GameState>().AddMonsterToData(
            this.GenerateData(),
            zone
            );
    }

    private MonsterData GenerateData()
    {
        MonsterData data = new MonsterData();

        data.type = this.type;

        data.location = new float[] {
            this.transform.position[0],
            this.transform.position[1],
            this.transform.position[2]
        };
        
        data.rotation = new float[] {
            this.transform.eulerAngles[0],
            this.transform.eulerAngles[1],
            this.transform.eulerAngles[2]
        };

        data.health = this.GetComponent<MonsterLife>().GetHealth();

        return data;
    }
}