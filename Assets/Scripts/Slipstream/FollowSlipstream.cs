using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSlipstream : MonoBehaviour {

    public List<Transform> waypoints = new List<Transform>();
    public float waypointRadius = 1.5f;
    public float damping = 0.1f;
    public bool loop = false;
    public float speed = 2.0f;
    public bool faceHeading = true;

    private Vector3 currentHeading, targetHeading;
    private int targetwaypoint;
    private Transform xform;
    private bool useRigidbody;
    private Rigidbody rigidmember;

    public bool followWaypoints = false;
    private Vector3 defaultHeading = Vector3.down;


    protected void Start() {
        xform = transform;
        currentHeading = xform.forward;
        targetwaypoint = 0;
        if(GetComponent<Rigidbody>() != null) {
            useRigidbody = true;
            rigidmember = GetComponent<Rigidbody>();
        } else {
            useRigidbody = false;
        }
    }


    protected void FixedUpdate() {
        if(followWaypoints & waypoints.Count > 0) {
            targetHeading = waypoints[targetwaypoint].position - xform.position;
        } else {
            followWaypoints = false;
            targetHeading = defaultHeading;
        }
        currentHeading = Vector3.Lerp(currentHeading, targetHeading, damping * Time.deltaTime);
    }


    protected void Update() {
        if(transform.position.z > 0) {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
        }
        if(useRigidbody)
            rigidmember.velocity = currentHeading * speed;
        else
            xform.position += currentHeading * Time.deltaTime * speed;
        if(faceHeading)
            xform.LookAt(xform.position + currentHeading);

        if(waypoints.Count > 0) {
            if(Vector3.Distance(xform.position, waypoints[targetwaypoint].position) <= waypointRadius) {
                waypoints.RemoveAt(0);
            }
        }
    }


    public bool CheckInSlipstream() {
        return followWaypoints;
    }


    public void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if(waypoints == null)
            return;
        for(int i = 0; i < waypoints.Count; i++) {
            Vector3 pos = waypoints[i].position;
            if(i > 0) {
                Vector3 prev = waypoints[i - 1].position;
                Gizmos.DrawLine(prev, pos);
            }
        }
    }


    public void AddWaypoint(Transform newWaypoint) {
        waypoints.Add(newWaypoint);
        followWaypoints = true;
    }
}
