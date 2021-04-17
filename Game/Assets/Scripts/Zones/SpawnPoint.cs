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

        Debug.Log("Distance " + distance);

        if(distance < minDistance || distance > maxDistance) return false;

        Instantiate(gameObject, this.transform.position, Quaternion.identity, parent);
        return true;
    }
}
