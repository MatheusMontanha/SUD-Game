using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour
{

    public float spawnTime = 5f;
    public float spawnDelay = 3f;
    public Vector3 initialPosition;
    public GameObject enemy;

    void Start()
    {
        //Start calling the Spawn function repeatedly after a delay.
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }
    void Spawn()
    {
        float maxDistance = 3;
        //Instantiate a random enemy.
        Vector3 spawn = new Vector3(Random.Range(transform.position.x-maxDistance, transform.position.x+maxDistance), Random.Range(transform.position.y-maxDistance, transform.position.y+maxDistance), transform.position.z);
        GameObject x = Instantiate(enemy, spawn, transform.rotation);
    }
}
