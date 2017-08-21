using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollection : MonoBehaviour {
    public GameObject powerup;

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag.Equals("Player")) {
            PowerupController powerupController = collider.gameObject.GetComponent<PowerupController>();
            powerupController.activatePowerUp(powerup);
            Destroy(gameObject);
        }
    }
}
