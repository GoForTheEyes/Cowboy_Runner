using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameSparks.Core;
using GameSparks.Api.Requests;

public class GameplayController : MonoBehaviour {

#pragma warning disable 0649
    [SerializeField]
    private GameObject pausePanel, prefabPlayer;

    [SerializeField]
    private Button restartGameButton, pauseButton, backToMenuButton;

    [SerializeField]
    private Text scoreText, pauseText;

    [SerializeField]
    private AudioClip gameOver;

    private int score;
#pragma warning restore 0649

    //Listener in case the player scores a HighScore!
    void Awake()
    {
        GameSparks.Api.Messages.NewHighScoreMessage.Listener += HighScoreMessageHandler;
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            Instantiate(prefabPlayer);
        }
    }

    //Console message if player scored highscore
    void HighScoreMessageHandler(GameSparks.Api.Messages.NewHighScoreMessage _message)
    {
        Debug.Log("NEW HIGH SCORE \n " + _message.LeaderboardName);
    }

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
        //if (!PlayerPrefs.HasKey("Score"))
        //{
        //    PlayerPrefs.SetInt("Score", 0);
        //} else
        //{
        //    int highScore = PlayerPrefs.GetInt("Score");
        //    if (score > highScore)
        //    {
        //        PlayerPrefs.SetInt("Score", score);
        //    }
        //}
        SaveScore(score);
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

    //Save score in gamesparks
    private void SaveScore(int score)
    {
        new GameSparks.Api.Requests.LogEventRequest().SetEventKey("SUBMIT_SCORE").SetEventAttribute("SCORE", score)
            .Send((response) =>
           {
               if (!response.HasErrors)
               {
                   Debug.Log("Score Posted Successfully...");
               }
               else
               {
                   Debug.Log("Error Posting Score...");
               }
           });
    }

}
