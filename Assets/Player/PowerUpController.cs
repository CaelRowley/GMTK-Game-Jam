using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {

    public GameObject activePowerUp;
    public GameObject nextPowerUp;
    public List<GameObject> queuedPowerUps = new List<GameObject>();

    private UIController UIController;

    private void Start(){
        UIController = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.tag.Equals("Follower") && queuedPowerUps.Count < 1) {
            ShipController shipController = collider.gameObject.GetComponent<ShipController>();
            
            if (shipController.CheckPowerUp()) {
                UIController.createUIIcon(shipController.GetPowerUp());
                queuedPowerUps.Add(shipController.GetPowerUp());
                //UIController.createUIIcon(queuedPowerUps);
                GameObject collectionAnimation = shipController.getCollectionAnimation();
                GameObject ActiveCollectionAnimation = Instantiate(collectionAnimation, gameObject.transform.position, collectionAnimation.transform.rotation);
                ActiveCollectionAnimation.transform.SetParent(gameObject.transform, true);
                SendPowerUpToPlayer();
                
            }
        }
    }

    void SendPowerUpToPlayer() {

        if (queuedPowerUps.Count == 1)
        {
            activePowerUp = queuedPowerUps[0];
        }

        else if (queuedPowerUps.Count == 2)
        {
            nextPowerUp = queuedPowerUps[1];
        }
    }


    public void ActivatePowerUp() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerStats playerStats = player.gameObject.GetComponent<PlayerStats>();
        if(playerStats.health > 0) {
            if(activePowerUp != null) {
                Vector3 spawnPoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                GameObject newPowerUp = Instantiate(activePowerUp, spawnPoint, gameObject.transform.rotation);
                queuedPowerUps.Remove(activePowerUp);
                UIController.removeUIIcon();
                activePowerUp = null;
            }

            if(nextPowerUp != null) {
                activePowerUp = nextPowerUp;
                nextPowerUp = null;
            }
        }
    }
}
