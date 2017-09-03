using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Core;
using GameSparks.Api.Requests;
using Facebook.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LoginToFacebook : MonoBehaviour
{
    //public Text userNameField
    //public Image userPic;
    public Text connectionInfoField;

    #pragma warning disable 0649
    [SerializeField]
    private Button fbConnectButton;
    #pragma warning restore 0649


    private void Awake()
    {
        GS.GameSparksAvailable += OnGameSparksConnected;
    }

    private void Start()
    {
        SetFBButton();
    }

    //checks if there is connection with GameSparks
    private void OnGameSparksConnected(bool _isConnected)
    {
        if (_isConnected)
        {
            //connectionInfoField.text = "GameSparks Connected...";
            //Debug.Log("GameSparks Connected...");
        }
        else
        {
            //connectionInfoField.text = "GameSparks Not Connected...";
        }
    }

    private void SetFBButton()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" && MainMenuController.firstTime)
        {
            connectionInfoField.text = "Continue with Facebook";
            fbConnectButton.onClick.RemoveAllListeners();
            fbConnectButton.onClick.AddListener(() => FacebookConnect_Button());
            fbConnectButton.onClick.AddListener(() => GameObject.Find("MainMenu Controller").GetComponent<MainMenuController>().ChangeMenus());
        }
        else if (SceneManager.GetActiveScene().name == "MainMenu" && !MainMenuController.firstTime) {
            GameObject.Find("MainMenu Controller").GetComponent<MainMenuController>().ChangeMenus();
        }
        // Not connected to facebook
        if (FB.IsLoggedIn)
        {
           connectionInfoField.text = "Logout from Facebook";
           fbConnectButton.onClick.RemoveAllListeners();
           fbConnectButton.onClick.AddListener(() => FacebookLogout_Button());
        }
        else if (!FB.IsInitialized || (FB.IsInitialized && !FB.IsLoggedIn))
        {
            connectionInfoField.text = "Continue with Facebook";
            fbConnectButton.onClick.RemoveAllListeners();
            fbConnectButton.onClick.AddListener(() => FacebookConnect_Button());
        }

    }

    private void FacebookLogout_Button()
    {
        if (FB.IsLoggedIn)
        {
            GS.Reset();
            FB.LogOut();
            SetFBButton();

        }
    }

    private void FacebookConnect_Button()
    {
        //First checks if FB is ready, and then logins //
        //If it is not ready we just initialize FB and use the login method as the callback
        // for the init method //
        if (!FB.IsInitialized)
        {
            FB.Init(ConnectUserToFacebook, null);
        }
        else
        {
            FB.ActivateApp();
            ConnectUserToFacebook();
        }
    }

    /// <summary>
    /// This method is used as the delegate for FB initialization - It logs in FB
    /// </summary>
    private void ConnectUserToFacebook()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            //Asking for permission to public profile, email and friends
            //var perms = new List<string>() { "public_profile", "email", "user_friends" };
            var perms = new List<string>() { "public_profile"};
            FB.LogInWithReadPermissions(perms, (result) =>
            {
                if (FB.IsLoggedIn)
                {
                    new GameSparks.Api.Requests.FacebookConnectRequest()
                    .SetAccessToken(AccessToken.CurrentAccessToken.TokenString)
                    .SetDoNotLinkToCurrentPlayer(false) //false means that logins as current user
                    .SetSwitchIfPossible(true) //this will switch to the player with this FB if they already have an account logged in
                    .Send((fbauth_response) =>
                    {
                        if (!fbauth_response.HasErrors)
                        {
                            connectionInfoField.text = "GS authenticated with FB...";
                            //userNameField.text = fbauth_response.DisplayName;
                            //Get User Pic
                            //GetFacebookPic();
                            SetFBButton();
                        }
                        else
                        {
                            Debug.LogWarning(fbauth_response.Errors.JSON); //print errors
                        }
                    });

                }
                else
                {
                    Debug.LogWarning("Facebook Login Failed:" + result.Error);
                }
            });
        }
        else
        {
            FacebookConnect_Button(); //if it did not connect then try again with a callback
        }
    }


    /// <summary>
    /// This method downloads and displays FB user picture
    /// </summary>
    //public void GetFacebookPic()
    //{
    //    new AccountDetailsRequest().Send((response) =>
    //    {
    //        string fbID;
    //        fbID = response.ExternalIds.GetString("FB");
    //        StartCoroutine(GetPicture(fbID));
    //    });
    //}

    //IEnumerator GetPicture(string fbID)
    //{
    //    UnityWebRequest www = UnityWebRequestTexture.GetTexture("http://graph.facebook.com/" + fbID + "/picture?type=square&width=128&height=128");
    //    yield return www.Send();

    //    Texture2D myTexture = DownloadHandlerTexture.GetContent(www);
    //    Image userAvatar = userPic.GetComponent<Image>();
    //    userAvatar.overrideSprite = Sprite.Create(myTexture, new Rect(0f, 0f, myTexture.width, myTexture.height), new Vector2(0f, 0f));
    //}
}
