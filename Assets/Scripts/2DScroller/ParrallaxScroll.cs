using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrallaxScroll : MonoBehaviour {
    public float speed;
    void Update()
    {
        parallaxScroll();
    }
    void parallaxScroll()
    {
        gameObject.transform.Translate(new Vector3(0,1,0) * speed);
    }
}
