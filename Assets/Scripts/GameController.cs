using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance;

    private int score;
    public Text scoreText;


    void Awake() {
        instance = this;
    }

    private void Start() {
        ManageCoins();
    }

    public void GetCoin() {
        score++;
        scoreText.text = "x " + score.ToString();

        PlayerPrefs.SetInt("score", score);
    }

    public void NextLevel(string nextLevel) {
        SceneManager.LoadScene(nextLevel);
    }

    private void ManageCoins() {
        if (SceneManager.GetActiveScene().name == "lvl_1") {
            PlayerPrefs.DeleteAll();
        }
        score = PlayerPrefs.GetInt("score");
        scoreText.text = "x " + score.ToString();
    }
}
