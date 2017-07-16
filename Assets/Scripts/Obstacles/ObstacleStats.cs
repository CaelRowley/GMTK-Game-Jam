using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStats : MonoBehaviour {
    public int health;
    public GameObject explosion;
    public bool invincible = true;
    public GameObject player;
    public float invincibilityOffset;

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
}
