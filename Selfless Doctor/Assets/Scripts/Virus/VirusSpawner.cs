using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawner : MonoBehaviour
{
    public GameObject virusPrefab;
    public Terrain groundTerrain;
    private float maxSpawnWidth;
    private float maxSpawnHeight;
  
    public void Start()
    {
        maxSpawnWidth = groundTerrain.terrainData.size.z;
        maxSpawnHeight = groundTerrain.terrainData.size.x;
        StartCoroutine(SpawnVirus());
    }

    IEnumerator SpawnVirus()
    {
        while(true)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(3, maxSpawnWidth - 40),11f, Random.Range(15, maxSpawnHeight - 40));
            GameObject temp = Instantiate(virusPrefab, spawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(GameManager.instance.enemySpawnTime);
         
        }
    }
   
}
