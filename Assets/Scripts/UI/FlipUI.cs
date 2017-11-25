using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipUI : MonoBehaviour {
    private bool isFlipped;

    void Start () {
        isFlipped = GameObject.Find("Main Camera").GetComponent<FlipCamera>().flipCamera;
        flip(isFlipped);
    }

    void flip(bool isFlipped)
    {
        if (isFlipped) {
            transform.Rotate(new Vector3(0, 180, 180));
            transform.Translate(new Vector3(0, 1, 0));
        }
        
    }
	
}
