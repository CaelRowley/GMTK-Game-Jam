using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour {

    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 1f;

    private void Start() {
        fadeGroup = FindObjectOfType<CanvasGroup>();

        fadeGroup.alpha = 1;

    }


    private void Update() {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
    }


    public void OnStartClick() {
        fadeGroup.alpha = 1;
        print("pressed");
        SceneManager.LoadScene("GameScene");
    }
}
