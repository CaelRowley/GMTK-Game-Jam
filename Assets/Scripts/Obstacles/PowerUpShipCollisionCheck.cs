using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShipCollisionCheck : MonoBehaviour {

    public int collisionDamage;
    public GameObject score;

    void OnCollisionEnter2D(Collision2D collison)
    {
        ObstacleStats obstacleStats;
        if (collison.gameObject.tag.Equals("Player"))
        {
            PlayerStats playerStats = collison.gameObject.GetComponent<PlayerStats>();
            playerStats.health -= collisionDamage;
            obstacleStats = gameObject.GetComponent<ObstacleStats>();
            obstacleStats.health -= collisionDamage;
            score = GameObject.FindGameObjectWithTag("Score");
           
            ScoreController scoreControl = score.GetComponent<ScoreController>();
            scoreControl.score-=1000;
        }
        else if (collison.gameObject.tag.Equals("Obstacle"))
        {
            obstacleStats = collison.gameObject.GetComponent<ObstacleStats>();
            obstacleStats.health -= collisionDamage;
            obstacleStats = gameObject.GetComponent<ObstacleStats>();
            obstacleStats.health -= collisionDamage;
        }
        else if (collison.gameObject.tag.Equals("Follower") & !gameObject.tag.Equals("Follower"))
        {
            obstacleStats = collison.gameObject.GetComponent<ObstacleStats>();
            obstacleStats.health -= collisionDamage;
        }
    }
}
