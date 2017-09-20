using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
    public int health;
    public GameObject explosion;
    public bool invincible = true;
    public GameObject player;
    public float invincibilityOffset;
    public int collisionDamage;
    public GameObject score;
    public GameObject powerUp;
    public bool HasPowerUp;
    public int scoreLost = 10000;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        if(health <= 0) {
            if(invincible)
                health = 1;
            else {
                GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
                Destroy(gameObject);
            }
        }

        if(transform.position.y > player.transform.position.y + invincibilityOffset) {
            invincible = false;
        }
    }

    public bool CheckPowerUp() {
        if(HasPowerUp) {
            HasPowerUp = false;
            return true;
        } else
            return false;
    }

    public GameObject GetPowerUp() {
        return powerUp;
    }

    void OnCollisionEnter2D(Collision2D collison) {
        ObstacleStats obstacleStats;
        if(collison.gameObject.tag.Equals("Player")) {
            PlayerStats playerStats = collison.gameObject.GetComponent<PlayerStats>();
            playerStats.health -= collisionDamage;
            health -= collisionDamage;
            score = GameObject.FindGameObjectWithTag("Score");
            ScoreController scoreControl = score.GetComponent<ScoreController>();
            scoreControl.score -= scoreLost;
        } else if(collison.gameObject.tag.Equals("Obstacle")) {
            obstacleStats = collison.gameObject.GetComponent<ObstacleStats>();
            obstacleStats.health -= collisionDamage;
            health -= collisionDamage;
        } else if(collison.gameObject.tag.Equals("Follower") & !gameObject.tag.Equals("Follower")) {
            ShipController shipController = collison.gameObject.GetComponent<ShipController>();
            shipController.health -= collisionDamage;
            health -= collisionDamage;
        }
    }
}
