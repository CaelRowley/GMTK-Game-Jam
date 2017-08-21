using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

    public int health;
    public int maxHealth = 8;
    public GameObject explosion;
    AsyncOperation loadingScene;

    private void Start() {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        loadingScene = SceneManager.LoadSceneAsync("Leaderboard");
        loadingScene.allowSceneActivation = false;
    }

    private void Update() {
        if(health > maxHealth) {
            health = maxHealth;
        }

        if(health <= 0) {
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
            StartCoroutine("LoadLeaderboard");
        } 
    }

    IEnumerator LoadLeaderboard() {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
        loadingScene.allowSceneActivation = true;
    }
}
