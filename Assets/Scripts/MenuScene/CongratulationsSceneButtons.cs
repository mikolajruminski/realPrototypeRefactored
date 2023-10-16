using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CongratulationsSceneButtons : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton, mainMenuButton;
    [SerializeField] private SceneTransition sceneTransition;


    private void Awake()
    {
        sceneTransition = sceneTransition.GetComponent<SceneTransition>();


        nextLevelButton.onClick.AddListener(() =>
        {
            sceneTransition.StartSceneTransition();
            UnInteractButtons();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            sceneTransition.StartSceneTransition();
            UnInteractButtons();
        });
    }

    private void Start()
    {
        sceneTransition.OnTransitionDone += SceneTransition_OnTransitionDone;
    }

    private void SceneTransition_OnTransitionDone(object sender, EventArgs e)
    {
        Loader.Load(Loader.Scene.MenuScene);
    }

    private void UnInteractButtons()
    {
        nextLevelButton.interactable = false;
        mainMenuButton.interactable = false;
    }
}
