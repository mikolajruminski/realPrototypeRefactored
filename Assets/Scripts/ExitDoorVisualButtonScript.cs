using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExitDoorVisualButtonScript : MonoBehaviour
{
    [SerializeField] private ExitDoorScript exitDoorScript;
    [SerializeField] private Transform interactButton;


    private void Start()
    {
        Hide();
        exitDoorScript.OnInteractableDetected += exitDoorScript_onInteractableDetected;
        exitDoorScript.OnInteractableEnded += exitDoorScript_OnInteractableEnded;
    }

    private void exitDoorScript_onInteractableDetected(object sender, EventArgs e)
    {
        Show();
    }
    private void exitDoorScript_OnInteractableEnded(object sender, EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        interactButton.gameObject.SetActive(true);
    }

    private void Hide()
    {
        interactButton.gameObject.SetActive(false);
    }

}
