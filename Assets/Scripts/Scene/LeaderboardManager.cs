using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour {

    public string highScoreGameKey;
    public string currentScoreGameKey;
    public float[] highScores = new float[5];

    [SerializeField]
    private Text highScore1 = null;
    [SerializeField]
    private Text highScore2 = null;
    [SerializeField]
    private Text highScore3 = null;
    [SerializeField]
    private Text highScore4 = null;
    [SerializeField]
    private Text highScore5 = null;
    [SerializeField]
    private Text currentScore = null;

    private string highScoreKey;

    // Reads highscores from the PlayerPrefs and assigns them to the Text fields
    private void Start() {

        for(int i = 0; i < highScores.Length; i++) {
            highScoreKey = highScoreGameKey + (i + 1).ToString();
            highScores[i] = PlayerPrefs.GetFloat(highScoreKey, 0.0f);
        }

        highScore1.text = highScores[0].ToString("0");
        highScore2.text = highScores[1].ToString("0");
        highScore3.text = highScores[2].ToString("0");
        highScore4.text = highScores[3].ToString("0");
        highScore5.text = highScores[4].ToString("0");
        currentScore.text = PlayerPrefs.GetFloat(currentScoreGameKey, 0.0f).ToString("0");
    }


    public void OnClickRestart() {
        SceneManager.LoadScene("GameScene");
    }


    public void OnClickMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
