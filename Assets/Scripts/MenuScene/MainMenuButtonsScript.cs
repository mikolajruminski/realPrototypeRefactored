using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;
using Cinemachine;

public class MainMenuButtonsScript : MonoBehaviour
{
    [SerializeField] private SceneTransition sceneTransition;
    public event EventHandler OnSceneTransitionBegin;
    public event EventHandler OnOptionsOpened;
    [SerializeField] private MainMenuUI mainMenuUI;

    [SerializeField] private Button playButton, exitButton, optionsButton;

    private void Awake()
    {
        sceneTransition.OnTransitionDone += SceneTransition_OnTransitionDone;

        playButton.onClick.AddListener(() =>
        {
            sceneTransition.StartSceneTransition();
            OnSceneTransitionBegin?.Invoke(this, EventArgs.Empty);
            UnInteractButtons();
        });

        optionsButton.onClick.AddListener(() =>
        {
            OnOptionsOpened?.Invoke(this, EventArgs.Empty);
            mainMenuUI.ChangeCameraToOptions();
            Hide();

        });

        exitButton.onClick.AddListener(() =>
        {
            UnInteractButtons();
            StartCoroutine(QuitTheGame());

        });
    }

    private void Start()
    {
        sceneTransition = sceneTransition.GetComponent<SceneTransition>();
    }

    private void SceneTransition_OnTransitionDone(object sender, EventArgs e)
    {
        Loader.Load(Loader.Scene.GameScene);
    }

    private void UnInteractButtons()
    {
        playButton.interactable = false;
        exitButton.interactable = false;
        optionsButton.interactable = false;
    }
    private void InteractButtons()
    {
        playButton.interactable = true;
        exitButton.interactable = true;
        optionsButton.interactable = true;
    }

    private IEnumerator QuitTheGame()
    {
        sceneTransition.StartScreenFadeOut();
        OnSceneTransitionBegin?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSecondsRealtime(3);
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
