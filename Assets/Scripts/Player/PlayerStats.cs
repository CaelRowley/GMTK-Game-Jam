using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

    public int health;
    public GameObject explosion;

    private void Update() {
        if(health <= 0) {
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
            StartCoroutine("LoadLeaderboard");
        } 
    }

    private void OnDestroy() {
        SceneManager.LoadScene("Leaderboard");
    }

    IEnumerator LoadLeaderboard() {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
