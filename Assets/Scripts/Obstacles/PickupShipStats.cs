using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupShipStats : MonoBehaviour {

    public int health;
    public GameObject explosion;

    private void Update() {
        if(health <= 0) {
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
        }
    }
}
