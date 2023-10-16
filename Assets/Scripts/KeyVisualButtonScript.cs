using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyVisualButtonScript : MonoBehaviour
{

    [SerializeField] private KeyBehaviourScript keyBehaviourScript;
    [SerializeField] private Transform interactButton;
    // Start is called before the first frame update

    void Start()
    {
        Hide();
        keyBehaviourScript.OnInteractDetected += keyBehaviourScript_OnInteractDetected;
        keyBehaviourScript.OnInteractableEnded += keyBehaviourScript_OnInteractableEnded;

    }

    private void keyBehaviourScript_OnInteractDetected(object sender, EventArgs e)
    {
        Show();
    }
    private void keyBehaviourScript_OnInteractableEnded(object sender, EventArgs e)
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
