using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

#pragma warning disable 0649
    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private Button restartGameButton, pauseButton, backToMenuButton;

    [SerializeField]
    private Text scoreText, pauseText;

    [SerializeField]
    private AudioClip gameOver;

    private int score;
#pragma warning restore 0649


    // Use this for initialization
    void Start () {
        scoreText.text = score.ToString() + "M";
        StartCoroutine(CountScore());
        backToMenuButton.onClick.AddListener(() => BackToMenu());
        pauseButton.onClick.AddListener(() => PauseButton());
    }
	
    IEnumerator CountScore()
    {
        yield return new WaitForSeconds(0.6f);
        score++;
        scoreText.text = score.ToString() + "M";
        StartCoroutine(CountScore());
    }

    private void OnEnable()
    {
        PlayerDead.endGame += PlayerDiedEndTheGame;
    }

    private void OnDisable()
    {
        PlayerDead.endGame -= PlayerDiedEndTheGame;
    }

    void PlayerDiedEndTheGame()
    {
        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
        } else
        {
            int highScore = PlayerPrefs.GetInt("Score");
            if (score > highScore)
            {
                PlayerPrefs.SetInt("Score", score);
            }
        }

        pausePanel.SetActive(true);
        pauseText.text = "Game Over";
        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => RestartGame());
        AudioSource.PlayClipAtPoint(gameOver, new Vector3(0, 0, 0));
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }


    public void PauseButton()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        pauseText.text = "Pause";
        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => ResumeGame());


        pauseButton.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }



}
