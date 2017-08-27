using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour {

    public float spawnTimeMin;
    public float spawnTimeMax;
    public float spawnPosXMin;
    public float spawnPosXMax;
    public int numToSpawnMin;
    public int numToSpawnMax;
    public GameObject obstacle;
    //public GameObject[] obsticles;
    public List<GameObject> obsticles = new List<GameObject>();
    public GameObject playerObject;
    public int minScale;
    public int maxScale;
    public bool useRandomRotation;
    public bool isAstroid = false;

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
        Vector3 spawnPoint = new Vector3(Random.Range(spawnPosXMin, spawnPosXMax), yPos, 0);
        GameObject newObstacle = null;

        if (isAstroid)
        {
            int spriteToSpawn = Random.Range(0, 2);
            print(spriteToSpawn);
            newObstacle = Instantiate(obsticles[spriteToSpawn], spawnPoint, randomRotation);
        }
        else {
            newObstacle = Instantiate(obstacle, spawnPoint, randomRotation);
        }

        float newScale = Random.Range(minScale, maxScale) / 10;
        newObstacle.transform.localScale += new Vector3(newScale, newScale, 0);
    }
}
