using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private PlayerAPI player;
    
    private void Start() {
        this.player = RuntimeStuff.GetSingleton<PlayerAPI>();
    }

    public bool Spawn(GameObject gameObject, Transform parent, float minDistance, float maxDistance)
    {
        // Get distance from player
        Vector3 playerPosition = this.player.GetPosition();
        float distance = Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.z), new Vector2(playerPosition.x, playerPosition.z));

        // Debug.Log("Distance " + distance);

        if(distance < minDistance || distance > maxDistance) return false;

        Instantiate(gameObject, this.transform.position, Quaternion.identity, parent);
        return true;
    }

    public void Spawn(GameObject gameObject, Transform parent, float[] position, float[] rotation, float health = -9999)
    {
        Vector3 pos = new Vector3(position[0], position[1], position[2]);
        Vector3 rot = new Vector3(rotation[0], rotation[1], rotation[2]);

        GameObject creature = Instantiate(gameObject, new Vector3(), Quaternion.identity, parent);

        creature.transform.position = pos;
        creature.transform.eulerAngles = rot;

        if(health != -9999)
        {
            if (creature.CompareTag("Animal")) 
            {
                creature.GetComponent<AnimalLife>().SetHealth(health);
            }
            else if (creature.CompareTag("Monster"))
            {
                creature.GetComponent<MonsterLife>().SetHealth(health);
            }
            else
            {
                Debug.LogError("What is this?");
            }
        }
    }
}
