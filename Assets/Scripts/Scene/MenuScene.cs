using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour {

    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 1f;
    AsyncOperation loadingScene;

    private void Start() {
        fadeGroup = FindObjectOfType<CanvasGroup>();
        loadingScene = SceneManager.LoadSceneAsync("GameScene");
        loadingScene.allowSceneActivation = false;
        fadeGroup.alpha = 1;
    }


    private void Update() {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
        //print(loadingScene.progress);
        
    }

    public void OnStartClick() {
        fadeGroup.alpha = 1;
        loadingScene.allowSceneActivation = true;
        print("pressed");
    }
}
