using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePushPowerUp : MonoBehaviour {
    // Use this for initialization
    private GameObject player;

    public float distance = 5.0f;
    public GameObject animationEnd;
    public float radius = 5.0F;
    public float power = 50.0F;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float speed = 20.0f;
        float step = speed * Time.deltaTime;
        transform.Translate(Vector3.down * step);

        if (player.transform.position.y - transform.position.y > distance) {
            Vector2 explosionPos = transform.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
            foreach (Collider2D hit in colliders)
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                if (rb != null) {
                    Vector3 direction = hit.transform.position - transform.position;
                    direction = direction.normalized;
                    rb.AddForce((direction * 8.0f), ForceMode2D.Impulse);
                }
            }
            GameObject ActiveAnimation = Instantiate(animationEnd, gameObject.transform.position, animationEnd.transform.rotation);
            Destroy(gameObject);
        }

    }
}
