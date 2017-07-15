using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour {
    //private Rigidbody rb;
    //private float startAcceleration;
    public float speed = 5.0F;
    public Transform target;
    //private bool isAbleToTurn;
    //public float speed;
    // Use this for initialization
    void Start () {
        //Console.WriteLine("Hello Unity");
        //System.Console.WriteLine("Hello");
        //Debug.Log("JPAsf");
        //rb = GetComponent<Rigidbody>();
        //Vector3 movement = new Vector3(0.0f, -20.0f, 0.0f);
        //rb.AddForce(movement * 1);
        
}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        //isAbleToTurn = false;
        //var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        //var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        //transform.Rotate(0, 0, 0);
        //transform.Translate(0, 0, z);
        //xTiltPhoneValue = Input.acceleration.x;
        //float calculatedAngle = transform.eulerAngles.z;
        //float numberOfRotations = calculatedAngle / 360.0f;
        //print(numberOfRotations);
        //if (numberOfRotations > 0.1f)
        //{
        //    print("full Rotation");
        //    float adjustedAngleToRotate = calculatedAngle - (360.0f * numberOfRotations);
        //    print(adjustedAngleToRotate);
        //}
        //else {
        //print(calculatedAngle);
        //if (calculatedAngle >= 90.0f)
        //{

        //    var movement = 360.0f - calculatedAngle;
        //    //print(movement);
        //    if (movement <= 90.0f)
        //    {
        //        print("Greater than 90 but less than 360");
        //        isAbleToTurn = true;
        //    }
        //}
        //else
        //{
        //    print("Less than 90");
        //    isAbleToTurn = true;
        //}

        //}
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        
        //if (calculatedAngle < 90.0f)
        //{
        //    print("Hope");
        //    isAbleToTurn = true;
        //}
        //else {

        //}
        //dir *= Time.deltaTime;
        //transform.Translate(dir * speed);


        //Vector3 dir = Vector3.zero;
        //dir.x = Input.acceleration.x;
        //dir.z = -Input.acceleration.x;
        //if (dir.sqrMagnitude > 1)
        //    dir.Normalize();
        //Vector3 playerMovementDirection = new Vector3(direction, -3.0f, 0.0f);
        //transform.Translate(playerMovementDirection * speed);
        //dir *= Time.deltaTime;
        //transform.Translate(dir * speed);
        //transform.Rotate(dir * speed);
        //if (isAbleToTurn) {
            //print("Not able to turn");
        Vector3 rotation = new Vector3(0.0f, 0.0f, (Input.acceleration.x * 270.0f));
        transform.Rotate(rotation * Time.deltaTime);    
            //Vector3 playerMovementDirection = new Vector3(0.0f, -3.0f, 0.0f);
            //transform.Translate(playerMovementDirection * speed);
        //}




        //if (Input.GetButtonDown("Fire1")) {
        //    Vector3 rotation = new Vector3(0.0f, 0.0f, 50.0f);
        //    transform.Rotate(rotation * Time.deltaTime);
        //    transform.Translate(0.1f, 0.0f, 0);

        //    //Vector3 movement = new Vector3(0.0f, -3.0f, 0.0f);
        //    //rb.AddForce(movement * 1);
        //}

        //if (Input.GetButtonDown("Fire2")){
        //    Vector3 rotation = new Vector3(0.0f, 0.0f, -50.0f);
        //    transform.Rotate(rotation * Time.deltaTime);
        //    transform.Translate(-0.1f, 0, 0.0f);
        //    //Vector3 movement = new Vector3(0.0f, -3.0f, 0.0f);
        //    //rb.AddForce(movement * 1);
        //}




    }
}
