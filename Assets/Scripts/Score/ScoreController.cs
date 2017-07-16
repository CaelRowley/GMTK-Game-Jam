using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    public GameObject player;
    public GameObject[] lives;
    private static float score = 0.0f;
    private bool scoreMultiplier;
    private bool scoreMultiplierTwo;


    void Start() {
        lives = GameObject.FindGameObjectsWithTag("Lives");
        player = GameObject.FindGameObjectWithTag("Player");
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (scoreMultiplier) {
            score += 20;
        }
        else if (scoreMultiplierTwo) {
            score += 40;
        }
        else
        {
            score += 10;
        }
        
        float adjustedScore = score / 1000;
        gameObject.GetComponent<Text>().text = adjustedScore.ToString("0");

        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        removeLives(playerStats);

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

    public void addScoreMultiplier() {
        scoreMultiplier = true;
    }
    public void removeScoreMultiplier() {
        scoreMultiplier = false;
    }

    public void addScoreMultiplierTwo()
    {
        scoreMultiplierTwo = true;
    }
    public void removeScoreMultiplierTwo()
    {
        scoreMultiplierTwo = false;
    }
}
