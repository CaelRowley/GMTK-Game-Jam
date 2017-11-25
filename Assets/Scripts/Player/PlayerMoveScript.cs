using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveScript : MonoBehaviour {
    public float speed;
    public Transform target;
    public float boundsLimit;
    Transform spriteRotation;
    Transform spriteRotationOrg;
    bool canRotateBack = true;

    //private GameObject multiplier;

    void Start() {
        //multiplier = GameObject.FindGameObjectWithTag("Multiplier");
        spriteRotation = gameObject.transform.GetChild(2);
    }

    void Update() {
        keepPlayerInBounds();
        playerCanNeverMoveBackwards();
        playerMoveFoward();

        //handleTiltInput();
        handleTouchScreen();
        moveWithKeys();
        usePowerUp();
        playerAlwaysRotatesLevel();
    }

    private void usePowerUp() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            //multiplier.GetComponent<Text>().text = touchPosition.ToString("0");
            if (touchPosition.x > Screen.width*.40 && touchPosition.x < Screen.width * .60)
            {
                gameObject.GetComponent<PowerUpController>().ActivatePowerUp();
            }
        }
    }

    private void handleTouchScreen() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            if (touchPosition.x < (Screen.width * .40)) {
                Vector3 rotation = new Vector3(0.0f, 0.0f, -135.0f);
                transform.Rotate(rotation * Time.deltaTime);
                tiltPlayerSprite("left");
            }
            else if(touchPosition.x > (Screen.width * .60)) {
                Vector3 rotation = new Vector3(0.0f, 0.0f, 135.0f);
                transform.Rotate(rotation * Time.deltaTime);
                tiltPlayerSprite("right");
            }
        }
    }

    private void moveWithKeys()
    {
        if (Input.GetKey("a"))
        {
            Vector3 rotation = new Vector3(0.0f, 0.0f, -135.0f);
            transform.Rotate(rotation * Time.deltaTime);
            tiltPlayerSprite("left");
        }

        else if (Input.GetKey("d")){
            Vector3 rotation = new Vector3(0.0f, 0.0f, 135.0f);
            transform.Rotate(rotation * Time.deltaTime);
            tiltPlayerSprite("right");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<PowerUpController>().ActivatePowerUp();
        }
    }

    public void playerAlwaysRotatesLevel() {
        if (spriteRotation.rotation != gameObject.transform.rotation && canRotateBack)
        {
            spriteRotation.rotation = Quaternion.RotateTowards(spriteRotation.rotation, gameObject.transform.rotation, 60 * Time.deltaTime);
        }
    }

    public void tiltPlayerSprite(String direction){
        
        if (direction == "left")
        {  
            if (spriteRotation.rotation.eulerAngles.y < 5 || spriteRotation.rotation.eulerAngles.y > 355)
            {
                Vector3 rotation = new Vector3(0.0f, 700.0f, 0.0f);
                spriteRotation.Rotate(rotation);
            }
            canRotateBack = true;
        }
        else if (direction == "right")
        {
            if (spriteRotation.rotation.eulerAngles.y > 355 || spriteRotation.rotation.eulerAngles.y < 5)
            {
                Vector3 rotation = new Vector3(0.0f, -700.0f, 0.0f);
                spriteRotation.Rotate(rotation);
                
            }
            canRotateBack = true;
        }
        playerStopsRotating();

    }

    void playerStopsRotating() {

        if (spriteRotation.eulerAngles.y >= 354)
        {
            canRotateBack = false;
        }
        if (spriteRotation.eulerAngles.y <= 6.0)
        {
            
            canRotateBack = false;
        }
    }

    void playerMoveFoward() {
        float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, step);

        //transform.Translate(Vector3.down* step);

        Vector3 position = transform.position;
        position.y = Mathf.Lerp(transform.position.y, target.position.y , step);
        position.x = Mathf.Lerp(transform.position.x, target.position.x, step);

        transform.position = position;
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
            spriteRotation.rotation = transform.rotation;
            transform.rotation = rotationBackTo0Left;

        }
        else if (transform.position.x > boundsLimit)
        {
            Quaternion rotationBackTo0Right = Quaternion.Euler(new Vector3(0.0f, 0.0f, 350.0f));
            spriteRotation.rotation = transform.rotation;
            transform.rotation = rotationBackTo0Right;
        }
    }
}
