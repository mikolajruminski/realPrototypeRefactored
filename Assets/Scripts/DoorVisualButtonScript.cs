using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVisualButtonScript : MonoBehaviour
{
    [SerializeField] private DoorBehaviourScript doorBehaviourScript;
    [SerializeField] private Transform interactButton;
    // Start is called before the first frame update

    void Start()
    {
        Hide();
        doorBehaviourScript.OnInteractableDetected += doorBehaviourScript_onInteractableDetected;
        doorBehaviourScript.OnInteractableEnded += doorBehaviourScript_OnInteractableEnded;

    }

    private void doorBehaviourScript_onInteractableDetected(object sender, EventArgs e)
    {
        Show();
    }
    private void doorBehaviourScript_OnInteractableEnded(object sender, EventArgs e)
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
