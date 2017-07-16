using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    private GameObject player;
    private GameObject multiplier;
    private GameObject astroidSpawner;
    private GameObject powerUpShipSpawner;
    private GameObject[] lives;
    private GameObject[] followers;
    public int highestNumOfFollowers = 0;
    private float score = 1.0f;
    private float scoreMultiplier = 0.0f;
    private float adjustedScore;


    public string highScoreGameKey;
    public bool bestScoreHigh;

    private int currentScore;
    private float[] bestScores = new float[5];
    private float bestScore;
    private string highScoreKey;



    void Start() {
        bestScore = 0.0f;
        for(int i = 0; i < bestScores.Length; i++) {
            highScoreKey = highScoreGameKey + (i + 1).ToString();
            bestScores[i] = PlayerPrefs.GetFloat(highScoreKey, 0.0f);
        }

        lives = GameObject.FindGameObjectsWithTag("Lives");
        player = GameObject.FindGameObjectWithTag("Player");
        multiplier = GameObject.FindGameObjectWithTag("Multiplier");
        astroidSpawner = GameObject.FindGameObjectWithTag("AstroidCreator");
        powerUpShipSpawner = GameObject.FindGameObjectWithTag("PowerUpShipCreator");
    }

	// Update is called once per frame
	void FixedUpdate () {
        setMutliplierFromFollowers();
        setMultiplierText();
        displayScore();

        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        removeLives(playerStats);

        increaseDifficulty();
    }

    void increaseDifficulty() {
        SpawnObstacle astroidSpawnerScript = astroidSpawner.GetComponent<SpawnObstacle>();
        SpawnObstacle powerUpSpawnerScript = powerUpShipSpawner.GetComponent<SpawnObstacle>();

        if (adjustedScore >= 500 && adjustedScore < 750)
        {
            astroidSpawnerScript.numToSpawnMax = 10;
            powerUpSpawnerScript.numToSpawnMax = 4;
        }
        else if (adjustedScore >= 750 && adjustedScore < 1000)
        {
            astroidSpawnerScript.numToSpawnMin = 4;
            powerUpSpawnerScript.numToSpawnMin = 2;
        }
        else if (adjustedScore >= 1000 && adjustedScore < 1500)
        {
            astroidSpawnerScript.numToSpawnMax = 10;
            powerUpSpawnerScript.numToSpawnMax = 5;
        }
        else if (adjustedScore >= 1500 && adjustedScore < 3000)
        {
            astroidSpawnerScript.numToSpawnMax = 15;
            powerUpSpawnerScript.numToSpawnMax = 8;
            astroidSpawnerScript.numToSpawnMin = 6;
            powerUpSpawnerScript.numToSpawnMin = 4;
        }
        else if (adjustedScore >= 3000)
        {
            astroidSpawnerScript.spawnTimeMax = 3;
            powerUpSpawnerScript.spawnTimeMax = 3;
        }
    }

    void displayScore() {
        score += 100;
        adjustedScore = score / 100;
        gameObject.GetComponent<Text>().text = adjustedScore.ToString("0");
    }

    void setMutliplierFromFollowers() {
        followers = GameObject.FindGameObjectsWithTag("Follower");
        scoreMultiplier = followers.Length;
        if (highestNumOfFollowers < followers.Length) {
            highestNumOfFollowers = followers.Length;
        }
    }

    void setMultiplierText() {
        if (scoreMultiplier >= 1)
        {
            score = score + scoreMultiplier*10;
            multiplier.GetComponent<Text>().text = "x" + scoreMultiplier.ToString("0");
        }
        else
        {
            multiplier.GetComponent<Text>().text = "";
        }
    }
    void removeLives(PlayerStats pStats) {
        if (pStats.health <= lives.Length)
        {
            Destroy(lives[pStats.health - 1]);
        }
    }

    float removeScore(float dividedBy) {
        float minusedScore = score/2;
        return minusedScore;
    }


    private void OnDestroy() {
        SaveScore();
    }


    public void SaveScore() {
        for(int i = 0; i < bestScores.Length; i++) {
            highScoreKey = highScoreGameKey + (i + 1).ToString();
            bestScore = PlayerPrefs.GetFloat(highScoreKey, 0);

            if(adjustedScore > bestScore) {
                PlayerPrefs.SetFloat(highScoreKey, adjustedScore);
                adjustedScore = bestScore;
                print("Saved score at position: " + i);
            }
        }
        PlayerPrefs.Save();
    }
}
