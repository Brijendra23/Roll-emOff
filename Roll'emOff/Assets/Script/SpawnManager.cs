using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    private float spawnRange = 9.0f;
    public int enemyCount = 0;
    public int waveNumber = 1;
    public GameObject powerupPrefab;
    public int numberPowerup = 0;

    // Start is called before the first frame update
    void Start()
    {
        Spawnenemy(waveNumber);
        InvokeRepeating("spawnPowerup", 3, 3);
    }
    private void spawnPowerup()
    {
        if (numberPowerup < 4)
        { 
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
            numberPowerup++;
        
        }
    }
    private void Spawnenemy(int numberofEnemy)
    {
        for(int i = 0; i < numberofEnemy; i++)
        {
            Instantiate(enemy, GenerateSpawnPosition(), enemy.transform.rotation);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
       enemyCount=FindObjectsOfType<Enemy>().Length;
       if(enemyCount == 0)
        {
            waveNumber++;
            Spawnenemy(waveNumber);
        }
    }
    
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomSpawnPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomSpawnPos;
    }
}
