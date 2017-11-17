using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    private GameObject player;
    private GameObject multiplier;
    private GameObject astroidSpawner;
    private GameObject powerUpShipSpawner;
    private GameObject lifeIcon;
    private List<GameObject> lives = new List<GameObject>();
    private GameObject[] followers;
    private PlayerStats playerStats;
    private GameObject gameCanvas;
    public int numOfFollowers;
    public float score = 1.0f;
    private float scoreMultiplier = 0.0f;
    public float adjustedScore;


    public string highScoreGameKey;
    public string currentScoreGameKey;
    public bool bestScoreHigh;

    private Transform firstLifeTransform;
    //private float firstLifeZ;

    private int currentScore;
    private float[] bestScores = new float[5];
    private string highScoreKey;

    void Start() {
        for(int i = 0; i < bestScores.Length; i++) {
            highScoreKey = highScoreGameKey + (i + 1).ToString();
            bestScores[i] = PlayerPrefs.GetFloat(highScoreKey, 0.0f);
        }
        gameCanvas = GameObject.FindGameObjectWithTag("Canvas");
        lifeIcon = GameObject.FindGameObjectWithTag("LifeIcon");
        player = GameObject.FindGameObjectWithTag("Player");
        multiplier = GameObject.FindGameObjectWithTag("Multiplier");
        astroidSpawner = GameObject.FindGameObjectWithTag("AstroidCreator");
        powerUpShipSpawner = GameObject.FindGameObjectWithTag("PowerUpShipCreator");
        playerStats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update() {
        int currentHealth = playerStats.health;
        removeLives(currentHealth);
        setMutliplierFromFollowers();
        setMultiplierText();
        displayScore();
        increaseDifficulty();
        addLives(currentHealth);
    }

    void createLifeUI(int life) {
        if(lives.Count == 0) {
            Vector3 newPosition = new Vector3(lifeIcon.transform.position.x - 2.0f, lifeIcon.transform.position.y, 0);
            GameObject newLife = Instantiate(lifeIcon, newPosition, lifeIcon.transform.rotation, gameCanvas.transform);
            lives.Add(newLife);
        } else if(lives.Count < 8) {
            Vector3 newPosition = new Vector3(lives[life - 1].transform.position.x - 1.00f, lives[life - 1].transform.position.y, lives[life - 1].transform.position.z);
            GameObject newLife = Instantiate(lifeIcon, newPosition, lifeIcon.transform.rotation, gameCanvas.transform);
            lives.Add(newLife);
        }

    }

    void increaseDifficulty() {
        SpawnObstacle astroidSpawnerScript = astroidSpawner.GetComponent<SpawnObstacle>();
        SpawnObstacle powerUpSpawnerScript = powerUpShipSpawner.GetComponent<SpawnObstacle>();
        PlayerMoveScript playerMove = player.GetComponent<PlayerMoveScript>();

        if (adjustedScore >= 200 && adjustedScore < 500)
        {
            playerMove.speed = 3.5f;
        }
        else if (adjustedScore >= 500 && adjustedScore < 750)
        {
            astroidSpawnerScript.numToSpawnMax = 8;
            powerUpSpawnerScript.numToSpawnMax = 3;
            playerMove.speed = 4.0f;
        }
        else if (adjustedScore >= 750 && adjustedScore < 1000)
        {
            astroidSpawnerScript.numToSpawnMin = 4;
            powerUpSpawnerScript.numToSpawnMin = 2;
            playerMove.speed = 4.2f;
        }
        else if (adjustedScore >= 1000 && adjustedScore < 1500)
        {
            astroidSpawnerScript.numToSpawnMax = 10;
            powerUpSpawnerScript.numToSpawnMax = 5;
            playerMove.speed = 4.6f;
        }
        else if (adjustedScore >= 1500 && adjustedScore < 3000)
        {
            astroidSpawnerScript.numToSpawnMax = 15;
            astroidSpawnerScript.numToSpawnMin = 6;
            playerMove.speed = 5.0f;
        }
        else if (adjustedScore >= 3000)
        {
            astroidSpawnerScript.spawnTimeMin = 10;
            astroidSpawnerScript.spawnTimeMax = 15;
            playerMove.speed = 5.5f;
        }
    }

    void displayScore() {
        adjustedScore = score / 100;
        //adjustedScore = 1501;
        gameObject.GetComponent<Text>().text = adjustedScore.ToString("0");
    }

    void setMutliplierFromFollowers() {
        followers = GameObject.FindGameObjectsWithTag("Follower");
        scoreMultiplier = followers.Length;
        if(numOfFollowers > followers.Length) {
            numOfFollowers = followers.Length;
            score -= 10000;
        } else {
            numOfFollowers = followers.Length;
        }
    }

    void setMultiplierText() {
        if(scoreMultiplier >= 1) {
            score = score + scoreMultiplier * 10;
            multiplier.GetComponent<Text>().text = "x" + scoreMultiplier.ToString("0");
        } else {
            multiplier.GetComponent<Text>().text = "";
        }
    }

    void addLives(int currentHealth) {
        if(currentHealth - 1 > lives.Count && currentHealth <= 8) {
            createLifeUI(lives.Count);
        }
    }

    void removeLives(int currentHealth) {
        if(currentHealth <= lives.Count && currentHealth > 0) {
            Destroy(lives[currentHealth - 1]);
            lives.RemoveAt(currentHealth - 1);
        }
    }

    float removeScore(float dividedBy) {
        float minusedScore = score / 2;
        return minusedScore;
    }


    private void OnDestroy() {
        SaveScore();
    }


    public void SaveScore() {
        float originalScore = adjustedScore;
        PlayerPrefs.SetFloat(currentScoreGameKey, adjustedScore);

        for(int i = 0; i < bestScores.Length; i++) {
            highScoreKey = highScoreGameKey + (i + 1).ToString();
            if(adjustedScore > bestScores[i]) {
                PlayerPrefs.SetFloat(highScoreKey, adjustedScore);
                adjustedScore = bestScores[i];
                print("Saved score at position: " + i);
            }
        }

        adjustedScore = originalScore;
        PlayerPrefs.Save();
    }
}
