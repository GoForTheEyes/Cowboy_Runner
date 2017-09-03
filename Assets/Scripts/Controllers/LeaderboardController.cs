using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Core;
using GameSparks.Api.Requests;
using Facebook.Unity;
using UnityEngine.SceneManagement;


public class LeaderboardController : MonoBehaviour {

#pragma warning disable 0649
    [SerializeField]
    private Text rankText, playerText, scoreText;

    [SerializeField]
    private GameObject dataPanel, notConnectedPanel;
#pragma warning disable 0649

    // Use this for initialization
    void Start () {
		if (FB.IsLoggedIn)
        {
            RefreshLeaderboard();
        }
        else
        {
            dataPanel.SetActive(false);
            notConnectedPanel.SetActive(true);
        }

	}
	
    public void GetLeaderboardData()
    {
        new GameSparks.Api.Requests.LeaderboardDataRequest().SetLeaderboardShortCode("SCORE_LEADERBOARD").
            SetEntryCount(20).Send((response) =>
           {
               if (!response.HasErrors)
               {
                   ClearLeaderBoardData();
                   Debug.Log("Found Leaderboard Data...");
                   foreach(GameSparks.Api.Responses.LeaderboardDataResponse._LeaderboardData entry in response.Data)
                   {
                       int rank = (int) entry.Rank;
                       rankText.text += rank.ToString();
                       string playerName = entry.UserName;
                       playerText.text += playerName;
                       playerText.text = playerText.text.Replace(' ', '\u00A0');
                       string score = entry.JSONData["SCORE"].ToString();
                       scoreText.text += score; 


                   }
               } else
               {
                   Debug.Log("Error Retrieving Leaderboard Data...");
               }
           });
    }

    private void ClearLeaderBoardData()
    {
        rankText.text = "";
        playerText.text = "";
        scoreText.text = "";
    }


    public void RefreshLeaderboard()
    {
        dataPanel.SetActive(true);
        notConnectedPanel.SetActive(false);
        GetLeaderboardData();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
