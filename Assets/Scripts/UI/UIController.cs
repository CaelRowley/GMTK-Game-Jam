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

    private List<GameObject> queuedPowerUps = new List<GameObject>();
    private List<GameObject> powerUpIcons = new List<GameObject>();

    private GameObject player;

    private float height;
    private float width;
    private Vector3 spawnPoint;

    void Start () {
        height = Screen.height;
        width = Screen.width;
        player = GameObject.FindGameObjectWithTag("Player");
        scoreIcon = GameObject.Find("New Piskel (9)");
        canvas = GameObject.Find("Canvas");
    }

    public void createUIIcon() {
        queuedPowerUps = player.GetComponent<PowerUpController>().queuedPowerUps;
        if (queuedPowerUps.Count == 1)
        {
            spawnPoint = new Vector3(scoreIcon.transform.position.x, scoreIcon.transform.position.y - 1);
            powerUpChecker(queuedPowerUps[0], spawnPoint);
        }
        if (queuedPowerUps.Count == 2)
        {
            spawnPoint = new Vector3(scoreIcon.transform.position.x, scoreIcon.transform.position.y - 1.50f);
            powerUpChecker(queuedPowerUps[1], spawnPoint);
        }
    }

    public void removeUIIcon() {
        if (powerUpIcons.Count > 0) {
            GameObject usedPowerUp = powerUpIcons[0];
            powerUpIcons.Remove(usedPowerUp);
            Destroy(usedPowerUp);
        }
    }

    void powerUpChecker(GameObject powerup, Vector3 spawnPoint) {
        switch (powerup.name)
        {
            case "Health":
                GameObject powerUpIconHealth = Instantiate(lifePowerUp, spawnPoint, gameObject.transform.rotation);
                powerUpIconHealth.transform.SetParent(canvas.transform, true);
                powerUpIcons.Add(powerUpIconHealth);
                Debug.Log(powerUpIcons[0]);
                break;
            case "ForcePush":
                GameObject powerUpIconForcepush = Instantiate(forcePushPowerUp, spawnPoint, gameObject.transform.rotation);
                powerUpIconForcepush.transform.SetParent(canvas.transform, true);
                powerUpIcons.Add(powerUpIconForcepush);
                break;
            case "Forcefield":
                GameObject powerUpIconForcefield = Instantiate(shieldPowerUp, spawnPoint, gameObject.transform.rotation);
                powerUpIconForcefield.transform.SetParent(canvas.transform, true);
                powerUpIcons.Add(powerUpIconForcefield);
                break;
        }
    }
}
