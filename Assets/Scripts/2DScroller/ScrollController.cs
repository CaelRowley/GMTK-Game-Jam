using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollController : MonoBehaviour{
    private GameObject player;
    private bool usedOnce;
    public GameObject nextBackground;

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        player = GameObject.FindGameObjectWithTag("Player");
        usedOnce = true;
    }

    void Update()
    {

        if (usedOnce)
        {
            if (transform.position.y > player.transform.position.y)
            {
                Instantiate(nextBackground, new Vector3(transform.position.x, transform.position.y - 30.0f, transform.position.z), transform.rotation);
                usedOnce = false;
            }
        }
        else {
            if (transform.position.y - player.transform.position.y > 100.0f) {
                Destroy(gameObject);
            }
            
        }
        if (gameObject.name.Contains("Plane")) {
            parallaxScroll();
        }
        
    }
    void parallaxScroll() {
        gameObject.transform.Translate(Vector3.forward * 0.05f); 
    }
}
