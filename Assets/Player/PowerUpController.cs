using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {

    public GameObject activePowerUp;
    public GameObject nextPowerUp;
    public List<GameObject> queuedPowerUps = new List<GameObject>();

    void FixedUpdate() {
        if(queuedPowerUps.Count > 0)
            activePowerUp = queuedPowerUps[0];
    }


    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.tag.Equals("Follower") && queuedPowerUps.Count < 2) {
            ShipController shipController = collider.gameObject.GetComponent<ShipController>();
            if(shipController.CheckPowerUp()) {
                queuedPowerUps.Add(shipController.GetPowerUp());
                SendPowerUpToPlayer();
            }
        }
    }


    void SendPowerUpToPlayer() {
        if (queuedPowerUps.Count < 2) {
            nextPowerUp = queuedPowerUps[0];
        }
    }


    public void ActivatePowerUp() {
        if (activePowerUp != null) {
            //Vector3 spawnPoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1f, gameObject.transform.position.z);
            Vector3 spawnPoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

            // add rotation to this instantiate

            GameObject newPowerUp = Instantiate(activePowerUp, spawnPoint, gameObject.transform.rotation);
            
            //Debug.Log(activePowerUp.name);
            if (activePowerUp.name == "Forcefield") {
                newPowerUp.transform.SetParent(gameObject.transform, true);
            }
            queuedPowerUps.Remove(activePowerUp);
            activePowerUp = null;
            //newPowerUp.transform.SetParent(gameObject.transform, true);
        }
        if (nextPowerUp != null) {
            activePowerUp = nextPowerUp;
            nextPowerUp = null;
        }
        // remove powerup from list
    }

    public List<GameObject> ReturnPowerUps()
    {
        return queuedPowerUps;
    }
}
