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
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= preparationTime && !isSpawning)
        {
            spawnTimer = 0f;
            StartCoroutine(SpawnHoomanRoutine());
        }
    }

    IEnumerator SpawnHoomanRoutine()
    {
        isSpawning = true;

        // Checking amount of object(hooman)
        int currentHoomanCount = GameObject.FindGameObjectsWithTag("Hooman").Length;

        if (currentHoomanCount < maxHoomanCount)
        {
            // Spawn enough hooman
            int spawnCount = maxHoomanCount - currentHoomanCount;
            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 tempPos = new Vector3(spawnPoint.position.x,
                                              spawnPoint.position.y + 1f,
                                              spawnPoint.position.z);
                SpawnHooman(tempPos);   // every copy of the prefab instantiate, they receive a position for to spawn
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        isSpawning = false;
    }

    void SpawnHooman(Vector3 tempPos)
    {
        // Instantiate(hoomanPrefab, spawnPoint.position, Quaternion.identity);
        GameObject HoomanClone = Instantiate(hoomanPrefab, tempPos, Quaternion.identity);
        Physics.IgnoreCollision(HoomanClone.GetComponentInChildren<Collider>(), GetComponent<Collider>());
    }
}