using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public GameObject lifePowerUp;
    public GameObject forcePushPowerUp;
    public GameObject shieldPowerUp;
    private PowerupController powerUpScript;

    private GameObject scoreIcon;
    private GameObject canvas;
    private GameObject camera;

    private List<GameObject> queuedPowerUps = new List<GameObject>();
    private List<GameObject> powerUpIcons = new List<GameObject>();

    private GameObject player;

    private float height;
    private float width;
    private Vector3 spawnPoint;

    //private bool isFlipped;

    void Start () {
        height = Screen.height;
        width = Screen.width;
        player = GameObject.FindGameObjectWithTag("Player");
        scoreIcon = GameObject.Find("New Piskel (9)");
        canvas = GameObject.Find("Canvas");
        //isFlipped = GameObject.Find("Main Camera").GetComponent<FlipCamera>().flipCamera;
    }

    public void createUIIcon(GameObject powerUp) {
        queuedPowerUps = player.GetComponent<PowerUpController>().queuedPowerUps;
        spawnPoint = new Vector3(scoreIcon.transform.position.x, scoreIcon.transform.position.y - 1.20f);
        //Debug.Log(queuedPowerUps.Count);

        if (queuedPowerUps.Count >= 1){
            powerUpChecker(powerUp, spawnPoint);
        } else {
            GameObject icon = powerUpChecker(powerUp, spawnPoint);
            Vector3 moveTo = new Vector3(0, -1, 0);
            icon.transform.Translate(moveTo);
        }
    }

    public void removeUIIcon() {
        if (powerUpIcons.Count > 0) {
            if (powerUpIcons.Count > 1){
                Vector3 spawnPoint = new Vector3(0, -1, 0);
                powerUpIcons[1].transform.Translate(spawnPoint);
            }
            GameObject usedPowerUp = powerUpIcons[0];
            powerUpIcons.Remove(usedPowerUp);
            Destroy(usedPowerUp);
            
            //Translate icon to use place

            
            


        }
    }

    GameObject powerUpChecker(GameObject powerup, Vector3 spawnPoint) {
        switch (powerup.name)
        {
            case "Health":
                GameObject powerUpIconHealth = Instantiate(lifePowerUp, spawnPoint, gameObject.transform.rotation);
                powerUpIconHealth.transform.SetParent(canvas.transform, true);
                powerUpIcons.Add(powerUpIconHealth);
                return powerUpIconHealth;
            case "ForcePush":
                GameObject powerUpIconForcepush = Instantiate(forcePushPowerUp, spawnPoint, gameObject.transform.rotation);
                powerUpIconForcepush.transform.SetParent(canvas.transform, true);
                powerUpIcons.Add(powerUpIconForcepush);
                return powerUpIconForcepush;
            case "Shield":
                GameObject powerUpIconForcefield = Instantiate(shieldPowerUp, spawnPoint, gameObject.transform.rotation);
                powerUpIconForcefield.transform.SetParent(canvas.transform, true);
                powerUpIcons.Add(powerUpIconForcefield);
                return powerUpIconForcefield;
        }
        return null;
    }
}
