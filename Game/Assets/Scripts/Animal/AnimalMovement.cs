using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Transform player_transform;

    private float angle;
    private int updates;
    private int target_updates;
    private bool evading;
    private static Vector3 raycast_direction = new Vector3(0, 1, 1);

    [Header("Movement Variables")]
    [SerializeField] public float speed;
    [SerializeField] public float evade_speed;
    [SerializeField]public float rotation_speed;
    [SerializeField] public float movement_range;
    [SerializeField] public float detect_radius;
    
    [Header("Collision Variables")]
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
        evading = false;
    }

    // FixedUpdate is called once every physic update
    private void FixedUpdate()
    {
        float dist = Vector3.Distance(player_transform.position, transform.position);

        // If evading player
        if (dist < detect_radius) {
            evading = true;

            // Rotate away player
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
        // If roaming
        else {
            if (evading) {
                evading = false;
                UpdateAngle(0);
            }

            Quaternion target = Quaternion.Euler(0, angle, 0);

            // Rotate
            if (Quaternion.Angle(rb.rotation, target) > 5)
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
        RuntimeStuff.GetSingleton<GameState>().AddAnimalToData(
            this.GenerateData(),
            zone
            );
    }

    private AnimalData GenerateData()
    {
        AnimalData data = new AnimalData();

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

        data.health = this.GetComponent<AnimalLife>().GetHealth();

        return data;
    }
}