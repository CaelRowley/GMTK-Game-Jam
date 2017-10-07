using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollision : MonoBehaviour {

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerStats playerStats = player.gameObject.GetComponent<PlayerStats>();
        playerStats.health += 2;
        Destroy(gameObject);
    }

    //void activatehealthpower() {
    //    gameobject player = gameobject.findgameobjectwithtag("player");
    //    playerstats playerstats = player.gameobject.getcomponent<playerstats>();
    //    playerstats.health += 2;
    //}
}
