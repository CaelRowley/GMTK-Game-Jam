using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStats : MonoBehaviour {
    public int health;
    public GameObject explosion;
    public bool invincible = true;
    public GameObject player;
    public float invincibilityOffset;

    public bool spawnPickup;
    public GameObject pickup;
    public GameObject attachedObject;
    public float spawnPosX;
    public float spawnPosY;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update() {
        if(health <= 0) {
            if(invincible)
                health = 1;
            else {
                if(spawnPickup) {
                    Vector3 spawnPoint = new Vector3(attachedObject.transform.position.x + spawnPosX, attachedObject.transform.position.y + spawnPosY, attachedObject.transform.position.z);
                    GameObject newPickup = Instantiate(pickup, spawnPoint, attachedObject.transform.rotation);
                }
                GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
                Destroy(gameObject);
            }
        }

        if(transform.position.y > player.transform.position.y + invincibilityOffset) {
            invincible = false;
        }
    }
}
