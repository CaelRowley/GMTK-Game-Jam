using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    public Transform player;
    private static float score = 0.0f;
    private bool scoreMultiplier;
	
	// Update is called once per frame
	void FixedUpdate () {
        //if (addScoreMultiplier) {
            //sc
        //}
        score += 10;

        float adjustedScore = score / 1000;
        gameObject.GetComponent<Text>().text = adjustedScore.ToString("0"); 
	}

    float removeScore(float dividedBy) {
        float minusedScore = score/2;
        return minusedScore;
    }

    void addScoreMultiplier() {
        scoreMultiplier = true;
    }
}
