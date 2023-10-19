using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler LevelEnding;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    public event EventHandler BackToMenu;

    [SerializeField] private SceneTransition sceneTransition;
    private bool isGamePaused = false;

    [SerializeField] private bool isGameActive;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnPausePerformed += GameInput_Instance_OnPausePerformed;
        sceneTransition = sceneTransition.GetComponent<SceneTransition>();
        isGameActive = true;
    }

    private void GameInput_Instance_OnPausePerformed(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    public bool GetIsGameActive()
    {
        return isGameActive;
    }

    public void SetIsGameActive(bool isGameActive)
    {
        this.isGameActive = isGameActive;
    }

    public IEnumerator EndLevel()
    {
        LevelEnding?.Invoke(this, EventArgs.Empty);
        sceneTransition.StartScreenFadeOut();
        yield return new WaitForSecondsRealtime(2);
        Loader.Load(Loader.Scene.CongratulationsScene);
    }

    public IEnumerator BackToMainMenu()
    {
        BackToMenu?.Invoke(this, EventArgs.Empty);
        sceneTransition.StartScreenFadeOut();
        yield return new WaitForSecondsRealtime(2);
        Loader.Load(Loader.Scene.MenuScene);
    }

    public void TogglePauseGame()
    {
        if (isGameActive)
        {
            isGamePaused = !isGamePaused;
            if (isGamePaused)
            {
                OnGamePaused?.Invoke(this, EventArgs.Empty);
                Time.timeScale = 0f;
            }
            else
            {

                Time.timeScale = 1f;
                OnGameUnPaused?.Invoke(this, EventArgs.Empty);
            }
        }
    }

}
