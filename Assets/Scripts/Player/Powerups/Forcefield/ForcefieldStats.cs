using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldStats : MonoBehaviour {

    public int health;

    private GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        transform.rotation = player.transform.rotation;

        if(health <= 0) {
            Destroy(gameObject);
        }
    }
}
