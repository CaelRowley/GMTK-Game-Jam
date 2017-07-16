using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipstreamCollisionCheck : MonoBehaviour {

    public FollowSlipstream parentScript;

    private bool hasEnteredStream = false;


    void OnTriggerEnter2D(Collider2D collider) {
        parentScript = gameObject.GetComponent<FollowSlipstream>();

        if(collider.tag.Equals("Waypoint") & !hasEnteredStream) {
            AddWaypoints();
            hasEnteredStream = true;
            gameObject.tag = "Follower";
        }

    }


    private void AddWaypoints() {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        for(int i = 1; i < waypoints.Length; i++) {
            if(waypoints[i].transform.position.y < transform.position.y)
                parentScript.AddWaypoint(waypoints[i].transform);
        }
    }
}
