using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is applied for a pad(maybe), the transform should be the pad by default
/// </summary>
public class HoomanSpawner : MonoBehaviour
{
    public GameObject hoomanPrefab;
    public Transform spawnPoint;
    public int maxHoomanCount = 10;
    public float preparationTime = 5f; // Spawn time interval
    public float spawnDelay = 1f; // Delay between each spawned hooman

    private float spawnTimer = 0f;
    private bool isSpawning = false; // Prevent multiple coroutines

    int currentHoomanCount, spawnCount, count;
    Vector3 tempPos;
    GameObject HoomanClone;

    [SerializeField]
    private bool countlessSpawning = false;

    void start()
    {
        // reduce workload
        tempPos = new Vector3(spawnPoint.position.x,
                              spawnPoint.position.y + 1f,
                              spawnPoint.position.z);

        spawnPoint = this.gameObject.transform;
        count = 0;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= preparationTime)
        {
            spawnTimer = 0f;
            if (count < maxHoomanCount)
            {
                if (!isSpawning)
                {
                    StartCoroutine(SpawnHoomanRoutine());
                }
            }

            if (countlessSpawning)
            {
                StartCoroutine(SpawnHoomanRoutine());
            }
        }
    }

    /* this one refill when hooman is not enough
    IEnumerator SpawnHoomanRoutine()
    {
        isSpawning = true;

        // Checking amount of object(hooman) in current scene
        currentHoomanCount = GameObject.FindGameObjectsWithTag("Hooman").Length;
        // but seemingly won't work when there are multiple spawn point

        if (currentHoomanCount < maxHoomanCount)
        {
            // Spawn enough hooman
            spawnCount = maxHoomanCount - currentHoomanCount;
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnHooman();   // every copy of the prefab instantiate, they receive a position for to spawn
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        isSpawning = false;
    }
    rewrite */

    IEnumerator SpawnHoomanRoutine()
    {
        isSpawning = true;

        SpawnHooman();   // every copy of the prefab instantiate, they receive a position for to spawn
        yield return new WaitForSeconds(spawnDelay);

        isSpawning = false;
    }

    void SpawnHooman()
    {
        // Instantiate(hoomanPrefab, spawnPoint.position, Quaternion.identity);
        HoomanClone = Instantiate(hoomanPrefab, tempPos, Quaternion.identity);
        Physics.IgnoreCollision(HoomanClone.GetComponentInChildren<Collider>(), GetComponent<Collider>());
        count++;
    }
}