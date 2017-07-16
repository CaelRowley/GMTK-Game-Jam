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
            if (startPosition.y > player.transform.position.y)
            {
                Instantiate(nextBackground, new Vector3(startPosition.x, startPosition.y - 40.0f, startPosition.z), startRotation);
                usedOnce = false;
            }
        }
        else {
            if (startPosition.y - player.transform.position.y > 30.0f) {
                Destroy(gameObject);
            }
        }
        
    }
}
