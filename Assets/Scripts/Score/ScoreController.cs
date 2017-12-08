using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    private GameObject player;
    private GameObject multiplier;
    private GameObject lifeIcon;
    private List<GameObject> lives = new List<GameObject>();
    private GameObject[] followers;
    private PlayerStats playerStats;
    private GameObject gameCanvas;
    public int numOfFollowers;
    public float score = 0.0f;
    private float scoreMultiplier = 0.0f;
    public float adjustedScore;


    public string highScoreGameKey;
    public string currentScoreGameKey;
    public bool bestScoreHigh;

    public SpawnObstacle scoreShipSpawner;
    public SpawnObstacle powerUpShipSpawner;
    public SpawnObstacle asteroidSpawner;
    public SpawnObstacle satteliteSpawner;

    private Transform firstLifeTransform;
    //private float firstLifeZ;

    private int currentScore;
    private float[] bestScores = new float[5];
    private string highScoreKey;

    PlayerMoveScript playerMove;

    void Start() {
        for(int i = 0; i < bestScores.Length; i++) {
            highScoreKey = highScoreGameKey + (i + 1).ToString();
            bestScores[i] = PlayerPrefs.GetFloat(highScoreKey, 0.0f);
        }
        gameCanvas = GameObject.FindGameObjectWithTag("Canvas");
        lifeIcon = GameObject.FindGameObjectWithTag("LifeIcon");
        player = GameObject.FindGameObjectWithTag("Player");
        multiplier = GameObject.FindGameObjectWithTag("Multiplier");
        playerStats = player.GetComponent<PlayerStats>();
        StartCoroutine("AddScore");
        playerMove = player.GetComponent<PlayerMoveScript>();
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
        //PlayerMoveScript playerMove = player.GetComponent<PlayerMoveScript>();

        if(adjustedScore < 500) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 2;
            scoreShipSpawner.spawnTimeMin = 30;
            scoreShipSpawner.spawnTimeMax = 50;

            powerUpShipSpawner.numToSpawnMin = 0;
            powerUpShipSpawner.numToSpawnMax = 0;
            powerUpShipSpawner.spawnTimeMin = 100;
            powerUpShipSpawner.spawnTimeMax = 120;

            asteroidSpawner.numToSpawnMin = 0;
            asteroidSpawner.numToSpawnMax = 0;
            asteroidSpawner.spawnTimeMin = 100;
            asteroidSpawner.spawnTimeMax = 100;

            satteliteSpawner.numToSpawnMin = 0;
            satteliteSpawner.numToSpawnMax = 0;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 3.0f;
        } else if(adjustedScore >= 500 && adjustedScore < 1000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 2;
            scoreShipSpawner.spawnTimeMin = 30;
            scoreShipSpawner.spawnTimeMax = 50;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 1;
            powerUpShipSpawner.spawnTimeMin = 100;
            powerUpShipSpawner.spawnTimeMax = 120;

            asteroidSpawner.numToSpawnMin = 4;
            asteroidSpawner.numToSpawnMax = 6;
            asteroidSpawner.spawnTimeMin = 40;
            asteroidSpawner.spawnTimeMax = 50;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 1;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 3.0f;
        } else if(adjustedScore >= 1000 && adjustedScore < 2000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 2;
            scoreShipSpawner.spawnTimeMin = 25;
            scoreShipSpawner.spawnTimeMax = 45;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 1;
            powerUpShipSpawner.spawnTimeMin = 100;
            powerUpShipSpawner.spawnTimeMax = 120;

            asteroidSpawner.numToSpawnMin = 4;
            asteroidSpawner.numToSpawnMax = 6;
            asteroidSpawner.spawnTimeMin = 25;
            asteroidSpawner.spawnTimeMax = 40;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 1;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 3.5f;
        } else if(adjustedScore >= 2000 && adjustedScore < 4000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 2;
            scoreShipSpawner.spawnTimeMin = 25;
            scoreShipSpawner.spawnTimeMax = 45;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 1;
            powerUpShipSpawner.spawnTimeMin = 100;
            powerUpShipSpawner.spawnTimeMax = 120;

            asteroidSpawner.numToSpawnMin = 4;
            asteroidSpawner.numToSpawnMax = 6;
            asteroidSpawner.spawnTimeMin = 20;
            asteroidSpawner.spawnTimeMax = 35;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 1;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 4.0f;
        } else if(adjustedScore >= 4000 && adjustedScore < 6000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 2;
            scoreShipSpawner.spawnTimeMin = 25;
            scoreShipSpawner.spawnTimeMax = 40;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 1;
            powerUpShipSpawner.spawnTimeMin = 100;
            powerUpShipSpawner.spawnTimeMax = 120;

            asteroidSpawner.numToSpawnMin = 4;
            asteroidSpawner.numToSpawnMax = 8;
            asteroidSpawner.spawnTimeMin = 16;
            asteroidSpawner.spawnTimeMax = 28;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 1;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 4.5f;
        } else if(adjustedScore >= 6000 && adjustedScore < 8000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 2;
            scoreShipSpawner.spawnTimeMin = 20;
            scoreShipSpawner.spawnTimeMax = 40;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 1;
            powerUpShipSpawner.spawnTimeMin = 100;
            powerUpShipSpawner.spawnTimeMax = 110;

            asteroidSpawner.numToSpawnMin = 4;
            asteroidSpawner.numToSpawnMax = 8;
            asteroidSpawner.spawnTimeMin = 15;
            asteroidSpawner.spawnTimeMax = 27;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 1;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 5.0f;
        } else if(adjustedScore >= 10000 && adjustedScore < 15000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 2;
            scoreShipSpawner.spawnTimeMin = 20;
            scoreShipSpawner.spawnTimeMax = 40;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 1;
            powerUpShipSpawner.spawnTimeMin = 90;
            powerUpShipSpawner.spawnTimeMax = 100;

            asteroidSpawner.numToSpawnMin = 4;
            asteroidSpawner.numToSpawnMax = 8;
            asteroidSpawner.spawnTimeMin = 14;
            asteroidSpawner.spawnTimeMax = 26;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 1;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 5.5f;
        } else if(adjustedScore >= 20000 && adjustedScore < 25000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 2;
            scoreShipSpawner.spawnTimeMin = 20;
            scoreShipSpawner.spawnTimeMax = 40;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 1;
            powerUpShipSpawner.spawnTimeMin = 70;
            powerUpShipSpawner.spawnTimeMax = 80;

            asteroidSpawner.numToSpawnMin = 4;
            asteroidSpawner.numToSpawnMax = 10;
            asteroidSpawner.spawnTimeMin = 12;
            asteroidSpawner.spawnTimeMax = 25;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 1;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 6.0f;
        } else if(adjustedScore >= 35000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 3;
            scoreShipSpawner.spawnTimeMin = 20;
            scoreShipSpawner.spawnTimeMax = 30;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 1;
            powerUpShipSpawner.spawnTimeMin = 50;
            powerUpShipSpawner.spawnTimeMax = 60;

            asteroidSpawner.numToSpawnMin = 6;
            asteroidSpawner.numToSpawnMax = 10;
            asteroidSpawner.spawnTimeMin = 10;
            asteroidSpawner.spawnTimeMax = 15;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 1;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 80;

            playerMove.speed = 6.5f;
        }
    }

    void displayScore() {
        adjustedScore = score;
        //adjustedScore = 301;
        gameObject.GetComponent<Text>().text = adjustedScore.ToString("0");
    }

    void setMutliplierFromFollowers() {
        followers = GameObject.FindGameObjectsWithTag("Follower");
        scoreMultiplier = followers.Length;
        if(numOfFollowers > followers.Length) {
            numOfFollowers = followers.Length;
            score -= 100;
        } else {
            numOfFollowers = followers.Length;
        }
    }

    void setMultiplierText() {
        if(scoreMultiplier >= 1) {
            //score = score + scoreMultiplier * 10;
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

    // Waits for the delay then shoots projectile every second
    private IEnumerator AddScore() {
        yield return new WaitForSeconds(1);
        while(true) {
            yield return new WaitForSeconds(1);
            score += scoreMultiplier * 50;
        }
    }
}
