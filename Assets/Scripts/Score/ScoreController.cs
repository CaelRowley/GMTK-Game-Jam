using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    private GameObject player;
    private GameObject multiplier;
    private GameObject[] lives;
    private GameObject[] followers;
    public int highestNumOfFollowers = 0;
    private static float score = 1.0f;
    private float scoreMultiplier = 0.0f;


    void Start() {
        lives = GameObject.FindGameObjectsWithTag("Lives");
        player = GameObject.FindGameObjectWithTag("Player");
        multiplier = GameObject.FindGameObjectWithTag("Multiplier");
    }

	// Update is called once per frame
	void FixedUpdate () {
        setMutliplierFromFollowers();
        setMultiplierText();
        displayScore();

        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        removeLives(playerStats);

    }
    void displayScore() {
        float adjustedScore = score / 100;
        gameObject.GetComponent<Text>().text = adjustedScore.ToString("0");
    }
    void setMutliplierFromFollowers() {
        followers = GameObject.FindGameObjectsWithTag("Follower");
        scoreMultiplier = followers.Length;
        if (highestNumOfFollowers < followers.Length) {
            highestNumOfFollowers = followers.Length;
            print(highestNumOfFollowers);
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


}
