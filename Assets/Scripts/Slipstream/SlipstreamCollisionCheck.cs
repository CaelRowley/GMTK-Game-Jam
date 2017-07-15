using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipstreamCollisionCheck : MonoBehaviour {

    public FollowSlipstream parentScript;


    void OnTriggerEnter2D(Collider2D collider) {
        parentScript = gameObject.GetComponent<FollowSlipstream>();
        if(!parentScript.CheckInSlipstream() || parentScript.waypoints.Count == 0) {
            AddWaypoints();
        }
        print("Truggered!!!!");
    }


    private void AddWaypoints() {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        for(int i = 1; i < waypoints.Length; i++) {
            if(waypoints[i].transform.position.y < transform.position.y)
                parentScript.AddWaypoint(waypoints[i].transform);
        }
    }
}
