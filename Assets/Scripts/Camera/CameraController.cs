using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    public float speed = 2.0f;
    private bool introActive;

    private void Start() {
        introActive = true;
    }

    void LateUpdate() {
        if(!introActive) {
            float lerpSpeed = speed * Time.deltaTime;

            Vector3 position = this.transform.position;
            position.y = Mathf.Lerp(this.transform.position.y, player.transform.position.y, lerpSpeed);
            position.x = Mathf.Lerp(this.transform.position.x, player.transform.position.x, lerpSpeed);

            this.transform.position = position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag.Equals("Player")) {
            introActive = false;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerMoveScript playerStats = collider.gameObject.GetComponent<PlayerMoveScript>();
            playerStats.canMove = true;
        }
    }
}
