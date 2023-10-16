using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimationScript : MonoBehaviour
{
    private const string MAIN_MENU_TRANSITION_ANIMATION = "MainMenuTransitionAnimation";
    [SerializeField] private MainMenuButtonsScript mainMenuButtonsScript;
    private Animator animator;

    private void Awake()
    {
        mainMenuButtonsScript = mainMenuButtonsScript.GetComponent<MainMenuButtonsScript>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        mainMenuButtonsScript.OnSceneTransitionBegin += MainMenuUi_OnSceneTransitionBegin;
    }

    private void MainMenuUi_OnSceneTransitionBegin(object sender, EventArgs e)
    {
        animator.Play(MAIN_MENU_TRANSITION_ANIMATION);
    }
}
