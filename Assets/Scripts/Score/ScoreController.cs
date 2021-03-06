﻿using System;
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

        if(adjustedScore < 1500) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 1;
            scoreShipSpawner.spawnTimeMin = 50;
            scoreShipSpawner.spawnTimeMax = 80;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 1;
            powerUpShipSpawner.spawnTimeMin = 120;
            powerUpShipSpawner.spawnTimeMax = 180;

            asteroidSpawner.numToSpawnMin = 0;
            asteroidSpawner.numToSpawnMax = 1;
            asteroidSpawner.spawnTimeMin = 25;
            asteroidSpawner.spawnTimeMax = 40;

            satteliteSpawner.numToSpawnMin = 0;
            satteliteSpawner.numToSpawnMax = 0;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 3.0f;
        } else if(adjustedScore >= 1500 && adjustedScore < 5000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 1;
            scoreShipSpawner.spawnTimeMin = 50;
            scoreShipSpawner.spawnTimeMax = 80;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 1;
            powerUpShipSpawner.spawnTimeMin = 120;
            powerUpShipSpawner.spawnTimeMax = 180;

            asteroidSpawner.numToSpawnMin = 3;
            asteroidSpawner.numToSpawnMax = 4;
            asteroidSpawner.spawnTimeMin = 25;
            asteroidSpawner.spawnTimeMax = 40;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 1;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 3.2f;
        } else if(adjustedScore >= 5000 && adjustedScore < 7000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 1;
            scoreShipSpawner.spawnTimeMin = 50;
            scoreShipSpawner.spawnTimeMax = 80;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 1;
            powerUpShipSpawner.spawnTimeMin = 120;
            powerUpShipSpawner.spawnTimeMax = 180;

            asteroidSpawner.numToSpawnMin = 3;
            asteroidSpawner.numToSpawnMax = 5;
            asteroidSpawner.spawnTimeMin = 25;
            asteroidSpawner.spawnTimeMax = 40;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 1;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 3.4f;
        } else if(adjustedScore >= 7000 && adjustedScore < 10000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 1;
            scoreShipSpawner.spawnTimeMin = 50;
            scoreShipSpawner.spawnTimeMax = 80;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 1;
            powerUpShipSpawner.spawnTimeMin = 110;
            powerUpShipSpawner.spawnTimeMax = 170;

            asteroidSpawner.numToSpawnMin = 4;
            asteroidSpawner.numToSpawnMax = 5;
            asteroidSpawner.spawnTimeMin = 20;
            asteroidSpawner.spawnTimeMax = 35;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 1;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 3.6f;
        } else if(adjustedScore >= 10000 && adjustedScore < 15000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 1;
            scoreShipSpawner.spawnTimeMin = 50;
            scoreShipSpawner.spawnTimeMax = 80;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 1;
            powerUpShipSpawner.spawnTimeMin = 110;
            powerUpShipSpawner.spawnTimeMax = 170;

            asteroidSpawner.numToSpawnMin = 4;
            asteroidSpawner.numToSpawnMax = 6;
            asteroidSpawner.spawnTimeMin = 20;
            asteroidSpawner.spawnTimeMax = 30;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 1;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 3.8f;
        } else if(adjustedScore >= 15000 && adjustedScore < 20000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 1;
            scoreShipSpawner.spawnTimeMin = 50;
            scoreShipSpawner.spawnTimeMax = 80;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 2;
            powerUpShipSpawner.spawnTimeMin = 110;
            powerUpShipSpawner.spawnTimeMax = 170;

            asteroidSpawner.numToSpawnMin = 4;
            asteroidSpawner.numToSpawnMax = 8;
            asteroidSpawner.spawnTimeMin = 15;
            asteroidSpawner.spawnTimeMax = 30;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 1;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 4.0f;
        } else if(adjustedScore >= 20000 && adjustedScore < 25000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 2;
            scoreShipSpawner.spawnTimeMin = 50;
            scoreShipSpawner.spawnTimeMax = 80;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 2;
            powerUpShipSpawner.spawnTimeMin = 110;
            powerUpShipSpawner.spawnTimeMax = 170;

            asteroidSpawner.numToSpawnMin = 4;
            asteroidSpawner.numToSpawnMax = 8;
            asteroidSpawner.spawnTimeMin = 15;
            asteroidSpawner.spawnTimeMax = 30;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 1;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 4.4f;
        } else if(adjustedScore >= 25000 && adjustedScore < 30000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 3;
            scoreShipSpawner.spawnTimeMin = 50;
            scoreShipSpawner.spawnTimeMax = 80;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 2;
            powerUpShipSpawner.spawnTimeMin = 110;
            powerUpShipSpawner.spawnTimeMax = 160;

            asteroidSpawner.numToSpawnMin = 4;
            asteroidSpawner.numToSpawnMax = 6;
            asteroidSpawner.spawnTimeMin = 15;
            asteroidSpawner.spawnTimeMax = 25;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 2;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 100;

            playerMove.speed = 4.8f;
        } else if(adjustedScore >= 30000 && adjustedScore < 50000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 3;
            scoreShipSpawner.spawnTimeMin = 45;
            scoreShipSpawner.spawnTimeMax = 75;

            powerUpShipSpawner.numToSpawnMin = 1;
            powerUpShipSpawner.numToSpawnMax = 2;
            powerUpShipSpawner.spawnTimeMin = 100;
            powerUpShipSpawner.spawnTimeMax = 160;

            asteroidSpawner.numToSpawnMin = 4;
            asteroidSpawner.numToSpawnMax = 8;
            asteroidSpawner.spawnTimeMin = 15;
            asteroidSpawner.spawnTimeMax = 20;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 3;
            satteliteSpawner.spawnTimeMin = 50;
            satteliteSpawner.spawnTimeMax = 80;

            playerMove.speed = 5.2f;
        } else if(adjustedScore >= 50000) {
            scoreShipSpawner.numToSpawnMin = 1;
            scoreShipSpawner.numToSpawnMax = 3;
            scoreShipSpawner.spawnTimeMin = 40;
            scoreShipSpawner.spawnTimeMax = 70;

            powerUpShipSpawner.numToSpawnMin = 2;
            powerUpShipSpawner.numToSpawnMax = 3;
            powerUpShipSpawner.spawnTimeMin = 100;
            powerUpShipSpawner.spawnTimeMax = 160;

            asteroidSpawner.numToSpawnMin = 5;
            asteroidSpawner.numToSpawnMax = 8;
            asteroidSpawner.spawnTimeMin = 10;
            asteroidSpawner.spawnTimeMax = 20;

            satteliteSpawner.numToSpawnMin = 1;
            satteliteSpawner.numToSpawnMax = 3;
            satteliteSpawner.spawnTimeMin = 40;
            satteliteSpawner.spawnTimeMax = 50;

            playerMove.speed = 5.7f;
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
