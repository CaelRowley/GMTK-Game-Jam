using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollision : MonoBehaviour {

    public GameObject animation;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerStats playerStats = player.gameObject.GetComponent<PlayerStats>();
        playerStats.health += 2;
        GameObject ActiveAnimation = Instantiate(animation, gameObject.transform.position, gameObject.transform.rotation);
        ActiveAnimation.transform.SetParent(player.gameObject.transform, true);
        Destroy(gameObject);
    }
}
