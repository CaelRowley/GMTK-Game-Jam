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
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //if (Input.GetButtonDown("Fire1")) {
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * 1);
        //}
            


        
    }
}
