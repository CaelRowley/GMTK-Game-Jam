using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipUI : MonoBehaviour {
    private bool isFlipped;

    void Start () {
        flip();
    }

    void flip()
    {
        if (PlayerPrefs.GetInt("FlipScreen", 0) == 0) {
            transform.Rotate(new Vector3(0, 180, 180));
            transform.Translate(new Vector3(0, 1, 0));
        }    
    }	
}
