using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    private GameObject player;
    private GameObject multiplier;
    private GameObject[] lives;
    private static float score = 1.0f;
    private float scoreMultiplier = 1.0f;


    void Start() {
        lives = GameObject.FindGameObjectsWithTag("Lives");
        player = GameObject.FindGameObjectWithTag("Player");
        multiplier = GameObject.FindGameObjectWithTag("Multiplier");
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (scoreMultiplier >= 1)
        {
            score = score * scoreMultiplier;
            multiplier.GetComponent<Text>().text = "x" + scoreMultiplier.ToString("0");
        }
        else {
            multiplier.GetComponent<Text>().text = "";
        }

        score += 10;
        
        float adjustedScore = score / 1000;
        gameObject.GetComponent<Text>().text = adjustedScore.ToString("0");

        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        removeLives(playerStats);

    }
    void removeLives(PlayerStats pStats) {
        if (pStats.health <= lives.Length)
        {
            Destroy(lives[pStats.health - 1]);
            scoreMultiplier -= 1;
        }
    }

    float removeScore(float dividedBy) {
        float minusedScore = score/2;
        return minusedScore;
    }

    public void addScoreMultiplier() {
        scoreMultiplier += 1;
    }
    public void removeScoreMultiplier() {
        scoreMultiplier -= 1;
    }

}
