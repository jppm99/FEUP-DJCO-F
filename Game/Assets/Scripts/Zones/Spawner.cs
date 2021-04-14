using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int zone = 0, maxNumberCreatures = 30;
    public float spawnDelay = 5f;
    public GameObject[] animals, monsters, spawnPoints;

    private GameManager gameHandler;
    private GameObject animalParent, monsterParent;

    private void Start() {
        this.gameHandler = RuntimeStuff.GetSingleton<GameManager>();

        // Creating gameobjects to parent animals and monsters
        this.animalParent = new GameObject("animals_parent_zone_" + this.zone);
        this.animalParent.transform.SetParent(this.transform);
        this.monsterParent = new GameObject("mosters_parent_zone_" + this.zone);
        this.monsterParent.transform.SetParent(this.transform);

        // Start Spawner
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(this.spawnDelay);

            GameObject parent;
            GameObject[] spannableList;

            (parent, spannableList) = this.gameHandler.IsDaytime() ? (this.animalParent, this.animals) : (this.monsterParent, this.monsters);

            if(parent.transform.childCount < this.maxNumberCreatures)
            {
                GameObject creature = spannableList[UnityEngine.Random.Range(0, spannableList.Length)];
                GameObject spawnPoint = this.spawnPoints[UnityEngine.Random.Range(0, this.spawnPoints.Length)];

                Instantiate(creature, spawnPoint.transform.position, Quaternion.identity, parent.transform);
            }
        }
    }
}
