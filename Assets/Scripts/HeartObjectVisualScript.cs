using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartObjectVisualScript : MonoBehaviour
{
    private const string HEART_OBJECT_DEATH = "HeartObjectDissapearAnimation";
    private Animator animator;

    [SerializeField] private HeartObjectScript heartObjectScript;
    private void Awake()
    {
        heartObjectScript = heartObjectScript.GetComponent<HeartObjectScript>();
    }
    private void Start()
    {
        heartObjectScript.OnPlayerTriggerEntered += HeartObjectScript_OnPlayerTriggerEntered;
        animator = GetComponent<Animator>();
    }

    private void HeartObjectScript_OnPlayerTriggerEntered(object sender, EventArgs e)
    {
        animator.Play(HEART_OBJECT_DEATH);
    }

    public void SelfDestruct()
    {
        heartObjectScript.DestroySelf();
    }

}
