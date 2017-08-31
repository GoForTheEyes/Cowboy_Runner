using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtonController : MonoBehaviour {

#pragma warning disable 0649
    [SerializeField]
    private Button musicButton;

    [SerializeField]
    private Image musicImage;

    [SerializeField]
    private Sprite musicOn, musicOff;
#pragma warning restore 0649

    public void Start()
    {
        InitializeMusicButton();
    }

    private void InitializeMusicButton()
    {
        musicButton.onClick.RemoveAllListeners();
        musicButton.onClick.AddListener(() => MusicButton());
        SetMusicImage();
    }

    public void SetMusicImage()
    {
        if (AudioController.instance.IsPlaying())
        {
            musicImage.sprite = musicOff;
        }
        else
        {
            musicImage.sprite = musicOn;
        }
    }

    public void MusicButton()
    {
        if (AudioController.instance.IsPlaying())
        {
            AudioController.instance.StopMusic();
            musicImage.sprite = musicOn;
        }
        else
        {
            AudioController.instance.PlayMusic();
            musicImage.sprite = musicOff;
        }
    }

}
