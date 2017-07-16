using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour {

    public GameObject explosion;
    public int collisionDamage;

    void OnCollisionEnter2D(Collision2D collison) {
        ObstacleStats obstacleStats;
        if(collison.gameObject.tag.Equals("Player")) {
            PlayerStats playerStats = collison.gameObject.GetComponent<PlayerStats>();
            playerStats.health -= collisionDamage;
        } else if(collison.gameObject.tag.Equals("Obstacle")) {
            obstacleStats = collison.gameObject.GetComponent<ObstacleStats>();
            obstacleStats.health -= collisionDamage;
        }

        obstacleStats = gameObject.GetComponent<ObstacleStats>();
        obstacleStats.health -= collisionDamage;
    }
}
