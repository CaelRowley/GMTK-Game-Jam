using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour {

    private CanvasGroup fadeGroup;
    private float loadTime;
    public float logoTimeMin = 3.0f;

    private void Start() {
        fadeGroup = FindObjectOfType<CanvasGroup>();

        fadeGroup.alpha = 1;

        if(Time.time < logoTimeMin)
            loadTime = logoTimeMin;
        else
            loadTime = Time.time;
    }


    private void Update() {
        if(Time.time < logoTimeMin) {
            fadeGroup.alpha = 1 - Time.time;
        }

        if(Time.time > logoTimeMin && loadTime != 0) {
            fadeGroup.alpha = Time.time - logoTimeMin;
            if(fadeGroup.alpha >= 1) {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
