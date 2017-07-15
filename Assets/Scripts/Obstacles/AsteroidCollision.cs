using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour {

    public GameObject explosion;
    public int collisionDamage;

    void OnCollisionEnter2D(Collision2D collison) {
        Destroy(gameObject);
        if(collison.gameObject.tag.Equals("Player")) {
            PlayerStats playerStats = collison.gameObject.GetComponent<PlayerStats>();
            playerStats.health -= collisionDamage;
        }
    }

    private void OnDestroy() {
        GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
    }
}
