using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour {

    public bool showCredits = false;
    public GameObject creditObject;
    public float creditSpeed = 0.05f;
    Vector3 startPositionCredit;
    public float endPointCredit;

    public GameObject endCreditButton;
    public float buttonSpeed = 0.05f;
    Vector3 startPositionButton;
    public float endPointButton;

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

        startPositionCredit = creditObject.transform.position;
        startPositionButton = endCreditButton.transform.position;
    }


    private void Update() {
        if (fadeIn){
            //fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
            fadeGroup.alpha = fadeGroup.alpha - 0.04f;
        }
        else {
            if(showCredits) {
                if(fadeGroup.alpha < 0.8f)
                    fadeGroup.alpha = fadeGroup.alpha + 0.02f;
            } else
                fadeGroup.alpha = fadeGroup.alpha + 0.02f; 
        }

        if(showCredits) {
            float newY = creditObject.transform.position.y + creditSpeed;
            if(newY > endPointCredit)
                newY = endPointCredit;
            creditObject.transform.position = new Vector3(creditObject.transform.position.x, newY, creditObject.transform.position.z);

            newY = endCreditButton.transform.position.y + buttonSpeed;
            if(newY > endPointButton)
                newY = endPointButton;
            endCreditButton.transform.position = new Vector3(endCreditButton.transform.position.x, newY, endCreditButton.transform.position.z);
        }
    }

    public void OnStartClick() {
        //fadeGroup.alpha = 1;
        fadeIn = false;
        loadingScene.allowSceneActivation = true;
        print("pressed");
    }

    public void OnCreditsClick() {
        //fadeGroup.alpha = 0.1f;
        fadeIn = false;
        showCredits = true;
    }

    public void OnSkipClick() {
        //fadeGroup.alpha = 0.5f;
        fadeIn = true;
        creditObject.transform.position = startPositionCredit;
        endCreditButton.transform.position = startPositionButton;
        showCredits = false;
    }
}
