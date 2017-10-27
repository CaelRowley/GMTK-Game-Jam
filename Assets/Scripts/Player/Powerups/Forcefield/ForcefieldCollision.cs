using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldCollision : MonoBehaviour {

    ForcefieldStats forcefieldStats;

    void Start() {
        forcefieldStats = gameObject.GetComponent<ForcefieldStats>();
    }


    void OnCollisionEnter2D(Collision2D collider) {
        ObstacleStats obstacleStats;
        if(collider.gameObject.tag.Equals("Obstacle")) {
            obstacleStats = collider.gameObject.GetComponent<ObstacleStats>();
            obstacleStats.health -= 1;
            forcefieldStats.health -= 1;
        } else if(collider.gameObject.tag.Equals("Ship")) {
            ShipController shipController = collider.gameObject.GetComponent<ShipController>();
            shipController.health -= 1;
            forcefieldStats.health -= 1;
        }
    }
}
