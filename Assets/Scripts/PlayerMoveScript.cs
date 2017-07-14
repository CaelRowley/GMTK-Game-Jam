using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour {
    private Rigidbody rb;
    // Use this for initialization
    void Start () {
        //Console.WriteLine("Hello Unity");
        //System.Console.WriteLine("Hello");
        //Debug.Log("JPAsf");
        rb = GetComponent<Rigidbody>();
        Vector3 movement = new Vector3(0.0f, -25.0f, 0.0f);
        rb.AddForce(movement * 1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        //var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        //var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        //transform.Rotate(0, 0, 0);
        //transform.Translate(0, 0, z);

        if (Input.GetButtonDown("Fire1")) {
            Vector3 rotation = new Vector3(0.0f, 0.0f, 50.0f);
            transform.Rotate(rotation * Time.deltaTime);
            transform.Translate(0.1f, 0.0f, 0);

            //Vector3 movement = new Vector3(0.0f, -3.0f, 0.0f);
            //rb.AddForce(movement * 1);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Vector3 rotation = new Vector3(0.0f, 0.0f, -50.0f);
            transform.Rotate(rotation * Time.deltaTime);
            transform.Translate(-0.1f, 0, 0.0f);
            //Vector3 movement = new Vector3(0.0f, -3.0f, 0.0f);
            //rb.AddForce(movement * 1);
        }




    }
}
