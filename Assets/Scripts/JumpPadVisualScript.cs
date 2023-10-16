using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadVisualScript : MonoBehaviour
{
    [SerializeField] private JumpPadScript jumpPadScript;
    private const string LAUNCH_PAD_LAUNCHING = "LaunchPadLaunching";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        jumpPadScript = jumpPadScript.gameObject.GetComponent<JumpPadScript>();
    }

    private void Start()
    {
        jumpPadScript.OnLaunchPerformed += jumpPadScript_OnLaunchPerformed;
    }

    private void jumpPadScript_OnLaunchPerformed(object sender, EventArgs e)
    {
        animator.Play(LAUNCH_PAD_LAUNCHING);
    }
}
