using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    public float speed = 2.0f;

    void LateUpdate()
    {
        float lerpSpeed = speed * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, player.transform.position.y, lerpSpeed);
        position.x = Mathf.Lerp(this.transform.position.x, player.transform.position.x, lerpSpeed);

        this.transform.position = position;
    }
}
