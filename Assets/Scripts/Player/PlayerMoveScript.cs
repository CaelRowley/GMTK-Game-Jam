using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour {
    public float speed = 5.0F;
    public Transform target;
    public float boundsLimit;


    void Start () {
        
    }

    void FixedUpdate(){
        keepPlayerInBounds();
        playerCanNeverMoveBackwards();
        playerMoveFoward();
        handleTiltInput();
    }

    void playerMoveFoward() {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    void handleTiltInput() {
        Vector3 rotation = new Vector3(0.0f, 0.0f, (Input.acceleration.x * 400.0f));
        transform.Rotate(rotation * Time.deltaTime);
    }

    void playerCanNeverMoveBackwards() {
        if (transform.eulerAngles.z > 75.0f & transform.eulerAngles.z < 180.0f)
        {
            Quaternion rotationBack90 = Quaternion.Euler(new Vector3(0.0f, 0.0f, 75.0f));
            transform.rotation = rotationBack90;

        }
        else if (transform.eulerAngles.z < 285.0f & transform.eulerAngles.z > 180.0f)
        {
            Quaternion rotationBack = Quaternion.Euler(new Vector3(0.0f, 0.0f, 285.0f));
            transform.rotation = rotationBack;
        }
    }
    void keepPlayerInBounds() {
        if (transform.position.x < -boundsLimit)
        {
            Quaternion rotationBackTo0Left = Quaternion.Euler(new Vector3(0.0f, 0.0f, 10.0f));
            transform.rotation = rotationBackTo0Left;

        }
        else if (transform.position.x > boundsLimit)
        {
            Quaternion rotationBackTo0Right = Quaternion.Euler(new Vector3(0.0f, 0.0f, 350.0f));
            transform.rotation = rotationBackTo0Right;
        }
    }
}
