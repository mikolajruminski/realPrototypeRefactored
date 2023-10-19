using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionsScript : MonoBehaviour
{
    [SerializeField] private Button soundsEffectsButton, musicButton, backButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText, musicText;


    private void Awake()
    {

        soundsEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            SoundManager.Instance.PlayMenuOptionsRefSound();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() =>
        {
            GameMusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        backButton.onClick.AddListener(() =>
        {
            Hide();
            GamePauseUI.Instance.ShowPauseButtons();
            SoundManager.Instance.PlayMenuOptionsSound();
        });
    }

    private void Start() 
    {
        GameManager.Instance.OnGameUnPaused += GameManager_Instance_OnGameUnPaused;
        Hide();
        UpdateVisual();
    }

    private void GameManager_Instance_OnGameUnPaused(object sender, EventArgs e)
    {
        SoundManager.Instance.PlayMenuOptionsSound();
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "" + Mathf.Round(SoundManager.Instance.ReturnGetVolume() * 10f);
        musicText.text = "" + Mathf.Round(GameMusicManager.Instance.ReturnGetVolume() * 10f);
    }
}
