using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour {

    public int collisionDamage;

    void OnCollisionEnter2D(Collision2D collison) {
        ObstacleStats obstacleStats;
        if(collison.gameObject.tag.Equals("Player")) {
            PlayerStats playerStats = collison.gameObject.GetComponent<PlayerStats>();
            playerStats.health -= collisionDamage;
            obstacleStats = gameObject.GetComponent<ObstacleStats>();
            obstacleStats.health -= collisionDamage;
        } else if(collison.gameObject.tag.Equals("Ship")) {
            ShipController shipController = collison.gameObject.GetComponent<ShipController>();
            shipController.health -= collisionDamage;
            obstacleStats = gameObject.GetComponent<ObstacleStats>();
            obstacleStats.health -= collisionDamage;
        } else if (collison.gameObject.tag.Equals("Follower") & !gameObject.tag.Equals("Follower")) {
            ShipController shipController = collison.gameObject.GetComponent<ShipController>();
            shipController.health -= collisionDamage;
        }
    }
}
