using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveScript : MonoBehaviour {
    public float speed;
    public Transform target;
    public float boundsLimit;

    private GameObject multiplier;

    void Start() {
        multiplier = GameObject.FindGameObjectWithTag("Multiplier");
    }

    void FixedUpdate() {
        keepPlayerInBounds();
        playerCanNeverMoveBackwards();
        playerMoveFoward();
        //handleTiltInput();
        handleTouchScreen();
        moveWithKeys();
        usePowerUp();
    }

    private void usePowerUp() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            multiplier.GetComponent<Text>().text = touchPosition.ToString("0");
            if (touchPosition.y > 900.0F)
            {
                gameObject.GetComponent<PowerUpController>().ActivatePowerUp();
            }
        }
    }

    private void handleTouchScreen() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            if (touchPosition.x < 335.0f && touchPosition.y < 900.0F) {
                Vector3 rotation = new Vector3(0.0f, 0.0f, -135.0f);
                transform.Rotate(rotation * Time.deltaTime);
            }
            else if(touchPosition.x > 335.0f && touchPosition.y < 900.0F) {
                Vector3 rotation = new Vector3(0.0f, 0.0f, 135.0f);
                transform.Rotate(rotation * Time.deltaTime); 
            }
        }
    }

    private void moveWithKeys()
    {
        if (Input.GetKey("a")){
            Vector3 rotation = new Vector3(0.0f, 0.0f, -135.0f);
            transform.Rotate(rotation * Time.deltaTime);
        }
        if (Input.GetKey("d")){
            Vector3 rotation = new Vector3(0.0f, 0.0f, 135.0f);
            transform.Rotate(rotation * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            gameObject.GetComponent<PowerUpController>().ActivatePowerUp();
        }

    }

    void playerMoveFoward() {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    void handleTiltInput() {
        //Vector3 rotation = new Vector3(0.0f, 0.0f, (Input.acceleration.x * 350.0f));
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
