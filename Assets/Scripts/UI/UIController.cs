using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public GameObject lifePowerUp;
    public GameObject forcePushPowerUp;
    public GameObject shieldPowerUp;
    public PowerupController powerUpScript;

    private GameObject player;

    private float height;
    private float width;
    // Use this for initialization
    void Start () {
        height = Screen.height;
        width = Screen.width;
        player = GameObject.FindGameObjectWithTag("Player");
       
    }
	
	// Update is called once per frame
	void Update () {
        List<GameObject> queuedPowerUps = player.GetComponent<PowerUpController>().queuedPowerUps;
        //if (queuedPowerUps[0] != null) {
        //    Debug.Log(queuedPowerUps[0]);
        //}
        //if (queuedPowerUps[1] != null)
        //{
        //    Debug.Log(queuedPowerUps[1]);   
        //}
        
        
    }
}
