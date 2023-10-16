using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScreenMenuScript : MonoBehaviour
{
    [SerializeField] private MainMenuButtonsScript mainMenuButtonsScript;
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private MenuSoundManager menuSoundManager;
    [SerializeField] private MenuMusicManagerScript menuMusicManager;
    [SerializeField] private Button musicButton, soundEffectButton, backButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText, musicText;

    private void Awake()
    {
        mainMenuButtonsScript = mainMenuButtonsScript.GetComponent<MainMenuButtonsScript>();
        menuSoundManager = menuSoundManager.GetComponent<MenuSoundManager>();

        musicButton.onClick.AddListener(() =>
        {
            menuMusicManager.ChangeVolume();
            UpdateVisual();

        });
        soundEffectButton.onClick.AddListener(() =>
        {
            menuSoundManager.ChangeVolume();
            UpdateVisual();
        });
        backButton.onClick.AddListener(() =>
        {
            mainMenuUI.ChangeCameraToMenu();
            Hide();
            mainMenuButtonsScript.Show();
        });
    }

    private void Start()
    {
        UpdateVisual();
        gameObject.SetActive(false);
        mainMenuButtonsScript.OnOptionsOpened += MainMenuUI_OnOptionsOpened;
    }

    private void MainMenuUI_OnOptionsOpened(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "" + Mathf.Round(menuSoundManager.ReturnGetVolume() * 10f);
        musicText.text = "" + Mathf.Round(menuMusicManager.ReturnGetVolume() * 10f);
    }
}
