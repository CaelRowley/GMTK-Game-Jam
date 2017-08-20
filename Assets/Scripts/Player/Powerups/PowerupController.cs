using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour {

    public GameObject attachedObject;
    public float spawnPosX;
    public float spawnPosY;
    public bool spawnAsChild = false;
    private GameObject[] activatedPowerup;


    public void activatePowerUp(GameObject powerup) {
        activatedPowerup = GameObject.FindGameObjectsWithTag("Powerup");
        if(activatedPowerup.Length < 100) {
            Vector3 spawnPoint = new Vector3(attachedObject.transform.position.x + spawnPosX, attachedObject.transform.position.y + spawnPosY, attachedObject.transform.position.z);
            GameObject newPickup = Instantiate(powerup, spawnPoint, attachedObject.transform.rotation);
            if(spawnAsChild)
                newPickup.transform.parent = attachedObject.transform;
        } 
    }
}
