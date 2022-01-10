using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public string url;
    public GameObject endGamePanel;
    public GameObject pauseMenu;


    private void Update() {
        if (Input.GetButtonDown("Cancel")) {
            OnPauseGame();
        }

    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void ExitGame() {
        Application.Quit();
    }
    public void OpenGitHub() {
        Application.OpenURL(url);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            endGamePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void OnPauseGame() {
        if (endGamePanel == null || !endGamePanel.activeSelf) {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            int timeScale = ((int)Time.timeScale);
            Time.timeScale = timeScale == 0 ? 1 : 0;
        }
    }
}
