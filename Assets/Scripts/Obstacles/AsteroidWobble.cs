using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidWobble : MonoBehaviour {
    public float speed = 5.0F;

    private Vector3 targetPosition;

    private void Awake() {
        speed = Random.Range(0.0f, 10.0f);
        targetPosition = new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y + Random.Range(-10, 10), transform.position.z);
    }
    private void Update() {
        //float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
