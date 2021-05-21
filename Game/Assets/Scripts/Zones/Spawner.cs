using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int maxNumberCreatures;
    public float spawnDelay, minimumDistaceToPlayer, maximumDistaceToPlayer;
    public Transform spawnPointParent;
    public GameObject[] animals, monsters;

    private SpawnPoint[] spawnPoints;
    private GameManager gameHandler;
    private GameObject animalParent, monsterParent;
    private int spawn_count = 0, zone;
    private ZoneLightSystem zoneLightSystem;

    private void Start() {
        this.gameHandler = RuntimeStuff.GetSingleton<GameManager>();

        this.zone = this.GetComponent<Zone>().GetZone();
        this.zoneLightSystem = this.GetComponent<ZoneLightSystem>();

        // Creating gameobjects to parent animals and monsters
        this.animalParent = new GameObject("animals_parent_zone_" + this.zone);
        this.animalParent.transform.SetParent(this.transform);
        this.monsterParent = new GameObject("mosters_parent_zone_" + this.zone);
        this.monsterParent.transform.SetParent(this.transform);

        // Get all Spawn Points
        this.spawnPoints = this.spawnPointParent.GetComponentsInChildren<SpawnPoint>();

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

            (parent, spannableList) = (this.gameHandler.IsDaytime() || this.zoneLightSystem.GetState()) ? (this.animalParent, this.animals) : (this.monsterParent, this.monsters);

            if(parent.transform.childCount < this.maxNumberCreatures)
            {
                if(spannableList.Length > 0 && this.spawnPoints.Length > 0)
                {
                    GameObject creature = spannableList[UnityEngine.Random.Range(0, spannableList.Length)];

                    // Try to spawn within distance starting from spawnPoint
                    int oldCount = this.spawn_count;
                    while(
                        (this.spawn_count - oldCount) < this.spawnPoints.Length &&
                        !this.GetNextSpawnPoint().Spawn(creature, parent.transform, this.minimumDistaceToPlayer, this.maximumDistaceToPlayer)
                    );
                }
            }
        }
    }

    private SpawnPoint GetNextSpawnPoint()
    {
        // // Random spawn point
        // SpawnPoint spawnPoint = this.spawnPoints[UnityEngine.Random.Range(0, this.spawnPoints.Length)];

        // Sequential spawn point
        SpawnPoint spawnPoint = this.spawnPoints[this.spawn_count % this.spawnPoints.Length];

        this.spawn_count++;
        return spawnPoint;
    }

    public void SaveMonsters()
    {
        foreach(Transform monster in this.monsterParent.GetComponentsInChildren<Transform>())
        {
            if(monster.CompareTag("Monster"))
            {
                monster.gameObject.GetComponent<MonsterMovement>()?.AddToData(this.zone);
            }
        }
    }
    
    public void SaveAnimals()
    {
        foreach(Transform animal in this.animalParent.GetComponentsInChildren<Transform>())
        {
            if(animal.CompareTag("Animal"))
            {
                animal.gameObject.GetComponent<AnimalMovement>()?.AddToData(this.zone);
            }
        }
    }

    public void SpawnMonsters(List<MonsterData> monsters)
    {
        foreach(MonsterData monster in monsters)
        {
            this.spawnPoints?[0]?.Spawn(
                this.monsters?[0],
                this.monsterParent.transform,
                monster.location,
                monster.rotation,
                monster.health
                );
        }
    }
    
    public void SpawnAnimals(List<AnimalData> animals)
    {
        foreach(AnimalData animal in animals)
        {
            this.spawnPoints?[0]?.Spawn(
                this.animals?[0],
                this.animalParent.transform,
                animal.location,
                animal.rotation,
                animal.health
                );
        }
    }
}
