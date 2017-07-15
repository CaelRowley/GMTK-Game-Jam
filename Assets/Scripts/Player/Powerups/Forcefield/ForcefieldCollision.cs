using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldCollision : MonoBehaviour {

    ForcefieldStats forcefieldStats;

    void Start() {
        forcefieldStats = gameObject.GetComponent<ForcefieldStats>();
    }


    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag.Equals("Obstacle")) {
            Destroy(collider.gameObject);
            forcefieldStats.health -= 1;
        }
    }
}
