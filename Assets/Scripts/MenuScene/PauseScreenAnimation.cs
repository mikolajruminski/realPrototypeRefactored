using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreenAnimation : MonoBehaviour
{
    private const string BACK_TO_MENU_ANIMATION = "BackToMenuPauseScreenAnimation";
    private Animator animator;
    private void Awake()
    {

    }

    private void Start()
    {
        GameManager.Instance.BackToMenu += GameManager_Instance_BackToMainMenu;
        animator = GetComponent<Animator>();
    }

    private void GameManager_Instance_BackToMainMenu(object sender, EventArgs e)
    {
        animator.Play(BACK_TO_MENU_ANIMATION);
    }
}
