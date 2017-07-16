using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullBoxController : MonoBehaviour {

    public GameObject player;
    public float offset;

    void LateUpdate() {
        transform.position = new Vector3(0, player.transform.position.y + offset, 0);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        print("youve been culled");
        Destroy(collider.gameObject);
    }
}
