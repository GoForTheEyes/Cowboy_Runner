using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public static bool firstTime = true;

#pragma warning disable 0649
    [SerializeField]
    private GameObject gameStartPanel, playGameButton, LeaderboardButton;
#pragma warning restore 0649


    private void Awake()
    {
    }


    private void Start()
    {
        if (firstTime)
        {
            gameStartPanel.SetActive(true);
            playGameButton.SetActive(false);
            LeaderboardButton.SetActive(false);
            firstTime = false;
        }
        else
        {
            gameStartPanel.SetActive(false);
            playGameButton.SetActive(true);
            LeaderboardButton.SetActive(true);
        }

    }


    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void ChangeMenus()
    {
        gameStartPanel.SetActive(false);
        playGameButton.SetActive(true);
        LeaderboardButton.SetActive(true);
        firstTime = false;
    }


}
