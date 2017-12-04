using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour {

    public GameObject highlightButton;
    public GameObject normalButton;
    public GameObject tiltButton;
    public GameObject flipScreenCheckMark;
    public GameObject tiltButtonCheckMark;

    public bool showOptions = false;
    public bool showCredits = false;
    public GameObject creditObject;
    public GameObject optionsObject;
    public float creditSpeed = 0.05f;
    public float optionsSpeed = 0.2f;
    Vector3 startPositionCredit;
    Vector3 startPositionOptions;
    public float endPointCredit;
    public float endPointOption;

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
        startPositionOptions = optionsObject.transform.position;

        if(PlayerPrefs.GetInt("FlipScreen", 0) == 1){
            GameObject button = GameObject.Find("Flip Screen");
            GameObject checkMark = Instantiate(flipScreenCheckMark, button.transform.position, button.transform.rotation);
            checkMark.transform.SetParent(button.transform, true);
            Vector3 moveTo = new Vector3(30.0f, 0, 0);
            checkMark.transform.Translate(moveTo);
        }

        if(PlayerPrefs.GetInt("TiltToPlay", 0) == 1) {
            GameObject button = GameObject.Find("Tilt Button");
            GameObject checkMark = Instantiate(tiltButtonCheckMark, button.transform.position, button.transform.rotation);
            checkMark.transform.SetParent(button.transform, true);
            Vector3 moveTo = new Vector3(30.0f, 0, 0);
            checkMark.transform.Translate(moveTo);
        }
    }


    private void Update() {
        if(fadeIn) {
            //fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
            fadeGroup.alpha = fadeGroup.alpha - 0.04f;
        } else {
            if(showCredits || showOptions) {
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
        } else if(showOptions) {
            float newY = optionsObject.transform.position.y + optionsSpeed;
            if(newY > endPointOption)
                newY = endPointOption;
            optionsObject.transform.position = new Vector3(optionsObject.transform.position.x, newY, optionsObject.transform.position.z);
        }
    }

    public void OnStartClick() {
        fadeIn = false;
        loadingScene.allowSceneActivation = true;
        print("pressed");
    }

    public void OnCreditsClick() {
        fadeIn = false;
        showCredits = true;
    }

    public void OnSkipClick() {
        fadeIn = true;
        creditObject.transform.position = startPositionCredit;
        endCreditButton.transform.position = startPositionButton;
        showCredits = false;
    }

    public void OnOptionsClick() {
        fadeIn = false;
        showOptions = true;
    }

    public void OnFlipScreenClick() {
        Debug.Log("Flip Screen");
        GameObject button = GameObject.Find("Flip Screen");

        if(GameObject.Find("FlipScreenTick(Clone)")) {
            Destroy(GameObject.Find("FlipScreenTick(Clone)"));
            PlayerPrefs.SetInt("FlipScreen", 0);
        } else {
            GameObject checkMark = Instantiate(flipScreenCheckMark, button.transform.position, button.transform.rotation);
            checkMark.transform.SetParent(button.transform, true);
            Vector3 moveTo = new Vector3(30.0f, 0, 0);
            checkMark.transform.Translate(moveTo);
            PlayerPrefs.SetInt("FlipScreen", 1);
        }
    }

    public void OnUseTiltClick() {
        Debug.Log("Tilt Button");
        GameObject button = GameObject.Find("Tilt Button");

        if(GameObject.Find("TiltToPlayTick(Clone)")) {
            Destroy(GameObject.Find("TiltToPlayTick(Clone)"));
            PlayerPrefs.SetInt("TiltToPlay", 0);
        } else {
            GameObject checkMark = Instantiate(tiltButtonCheckMark, button.transform.position, button.transform.rotation);
            checkMark.transform.SetParent(button.transform, true);
            Vector3 moveTo = new Vector3(30.0f, 0, 0);
            checkMark.transform.Translate(moveTo);
            PlayerPrefs.SetInt("TiltToPlay", 1);
        }
    }

    public void OnConfirmClick() {
        fadeIn = true;
        optionsObject.transform.position = startPositionOptions;
        showOptions = false;
    }

    public void OnLeaderboardClick() {
        fadeIn = false;
        SceneManager.LoadScene("Leaderboard");
        print("pressed");
    }
}
