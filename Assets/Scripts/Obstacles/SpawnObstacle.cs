using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour {

    public float spawnTimeMin;
    public float spawnTimeMax;
    public float spawnPosXMin;
    public float spawnPosXMax;

    public float zPosition = 0;

    public int numToSpawnMin;
    public int numToSpawnMax;
    public List<GameObject> obstacles = new List<GameObject>();
    public GameObject playerObject;
    public int minScale;
    public int maxScale;
    public bool useRandomRotation;
    private float spawnTimer;


    void Start() {
        spawnTimer = Random.Range(spawnTimeMin, spawnTimeMax) / 10;
    }


    void Update() {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0) {
            for(int i = Random.Range(numToSpawnMin, numToSpawnMax); i > 0; i--) {
                SpawnWaypoint();
            }
        }
    }


    private void SpawnWaypoint() {
        Quaternion randomRotation;
        if(useRandomRotation) {
            randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        } else {
            randomRotation = transform.rotation;
        }
        
        spawnTimer = Random.Range(spawnTimeMin, spawnTimeMax) / 10;
        float yPos = playerObject.transform.position.y - 50 + Random.Range(-25, 25);
        Vector3 spawnPoint = new Vector3(Random.Range(spawnPosXMin, spawnPosXMax), yPos, zPosition);
        GameObject newObstacle = null;

        int spriteToSpawn = Random.Range(0, obstacles.Count);
        newObstacle = Instantiate(obstacles[spriteToSpawn], spawnPoint, randomRotation);

        float newScale = Random.Range(minScale, maxScale) / 10;
        newObstacle.transform.localScale += new Vector3(newScale, newScale, 0);
    }
}
