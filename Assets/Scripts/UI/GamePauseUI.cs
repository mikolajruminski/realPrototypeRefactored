using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public static GamePauseUI Instance { get; private set; }

    [SerializeField] private Button resumeButton, optionsButton, menuButton;
    [SerializeField] private GameOptionsScript optionsUI;
    [SerializeField] private GameObject pauseButtons;

    private void Awake()
    {
        Instance = this;

        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
            SoundManager.Instance.PlayMenuOptionsSound();
        });
        optionsButton.onClick.AddListener(() =>
        {
            optionsUI.GetComponent<GameOptionsScript>().Show();
            SoundManager.Instance.PlayMenuOptionsSound();
            HidePauseButtons();
        });
        menuButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayMenuOptionsSound();
            GameManager.Instance.StartCoroutine(GameManager.Instance.BackToMainMenu());
        });
    }
    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_Instance_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_Instance_OnGameUnPaused;

        Hide();
    }

    private void GameManager_Instance_OnGameUnPaused(object sender, EventArgs e)
    {
        SoundManager.Instance.PlayMenuOptionsSound();
        Hide();
    }

    private void GameManager_Instance_OnGamePaused(object sender, EventArgs e)
    {
        Show();
        ShowPauseButtons();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void HidePauseButtons()
    {
        pauseButtons.SetActive(false);
    }
    public void ShowPauseButtons()
    {
        pauseButtons.SetActive(true);
    }
}
