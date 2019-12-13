using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour
{

    public float spawnTime = 5f;
    public float spawnDelay = 3f;
    public Vector3 initialPosition;
    public GameObject enemy;
    public float maxDistance;

    public FloatValue maxSummonedValue;
    private FloatValue enemyCounter;


    void Start()
    {
        //Start calling the Spawn function repeatedly after a delay.
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
        enemyCounter = new FloatValue(maxSummonedValue.value);
    }
    void Spawn()
    {
        //Instantiate a random enemy.
        if (enemyCounter.value > 0)
        {
            Vector3 spawn = new Vector3(Random.Range(transform.position.x - maxDistance, transform.position.x + maxDistance), Random.Range(transform.position.y - maxDistance, transform.position.y + maxDistance), transform.position.z);
            GameObject x = Instantiate(enemy, spawn, transform.rotation);
            x.GetComponent<Enemy>().AttatchCounter(enemyCounter);
            enemyCounter.value--;
            // Debug.Log(enemy.gameObject.name+ " Summoneds: "+(enemyCounter.initialValue-enemyCounter.value));

        }
    }
}
