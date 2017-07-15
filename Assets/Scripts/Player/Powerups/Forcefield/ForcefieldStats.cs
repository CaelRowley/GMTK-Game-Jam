using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldStats : MonoBehaviour {

    public int health;

    private void Update() {
        if(health <= 0) {
            Destroy(gameObject);
        }
    }
}
