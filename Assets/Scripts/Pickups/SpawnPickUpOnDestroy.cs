using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickUpOnDestroy : MonoBehaviour {
    public GameObject pickup;
    public GameObject attachedObject;
    public float spawnPosX;
    public float spawnPosY;


    void OnDestroy() {
        Vector3 spawnPoint = new Vector3(attachedObject.transform.position.x + spawnPosX, attachedObject.transform.position.y + spawnPosY, attachedObject.transform.position.z);
        GameObject newPickup = Instantiate(pickup, spawnPoint, attachedObject.transform.rotation);
    }
}
