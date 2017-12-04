using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStats : MonoBehaviour {
    public int health;
    public GameObject explosion;
    public bool invincible = true;
    public GameObject player;
    public float invincibilityOffset;
    public float explosionStartSize = 5f; 

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update() {
        if(health <= 0) {
            if(invincible)
                health = 1;
            else {
                GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
                newExplosion.GetComponent<ParticleSystem>().startSize = explosionStartSize;
                Destroy(gameObject);
            }
        }

        if(transform.position.y > player.transform.position.y + invincibilityOffset) {
            invincible = false;
        }
    }
}
