using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldCollision : MonoBehaviour {

    ForcefieldStats forcefieldStats;

    void Start() {
        forcefieldStats = gameObject.GetComponent<ForcefieldStats>();
    }


    void OnTriggerEnter2D(Collider2D collider) {
        ObstacleStats obstacleStats;
        if(collider.gameObject.tag.Equals("Obstacle")) {
            obstacleStats = collider.gameObject.GetComponent<ObstacleStats>();
            obstacleStats.health -= 1;
            forcefieldStats.health -= 1;
        }
    }
}
