using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour {

    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 1f;
    AsyncOperation loadingScene;
    bool fadeIn;

    private void Start() {
        fadeGroup = FindObjectOfType<CanvasGroup>();
        loadingScene = SceneManager.LoadSceneAsync("GameScene");
        loadingScene.allowSceneActivation = false;
        fadeIn = true;
        fadeGroup.alpha = 1;
    }


    private void Update() {
        if (fadeIn){
            fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
        }
        else {
            fadeGroup.alpha = fadeGroup.alpha + 0.01f;
            
        }
    }

    public void OnStartClick() {
        //fadeGroup.alpha = 1;
        fadeIn = false;
        loadingScene.allowSceneActivation = true;
        print("pressed");
    }
}
