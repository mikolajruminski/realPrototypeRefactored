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
    [SerializeField] private SceneTransition sceneTransition;

    [SerializeField] private bool isGameActive;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        sceneTransition = sceneTransition.GetComponent<SceneTransition>();
        isGameActive = true;
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

}
