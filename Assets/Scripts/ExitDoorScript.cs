using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ExitDoorScript : MonoBehaviour, IInteractable
{
    public static ExitDoorScript Instance { get; private set; }
    public event EventHandler OnInteractableDetected;
    public event EventHandler OnInteractableEnded;
    public event EventHandler DoorOpening;
    [SerializeField] private ExitDoorVisualScript exitDoorVisualScript;

    private bool _isInteractable = false;
    private bool _isActive = true;

    [SerializeField] InventoryManager.AllItems requiredItem;

    public bool isInteractable { get => _isInteractable; set => _isInteractable = value; }
    public bool isActive { get => _isActive; set => _isActive = value; }

    private void Awake()
    {
        Instance = this;
        exitDoorVisualScript = exitDoorVisualScript.GetComponent<ExitDoorVisualScript>();
    }

    private void Start()
    {
        exitDoorVisualScript.OnExitAnimationEnd += ExitDoorVisualScript_OnExitAnimationEnd;
    }

    private void ExitDoorVisualScript_OnExitAnimationEnd(object sender, EventArgs e)
    {
        StartEndingLevel();
    }

    private bool HasRequiredItem()
    {
        if (InventoryManager.Instance.inventoryItems.Contains(requiredItem))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Interact()
    {
        if (HasRequiredItem())
        {
            GameManager.Instance.SetIsGameActive(false);
            DoorOpening?.Invoke(this, EventArgs.Empty);
            isActive = false;
            HideInteractableButton();
        }
        else
        {
            Debug.Log("fuck off");
        }
    }

    public void ShowInteractableButton()
    {
        OnInteractableDetected.Invoke(this, EventArgs.Empty);
        isInteractable = true;
    }

    public void HideInteractableButton()
    {
        OnInteractableEnded.Invoke(this, EventArgs.Empty);
        isInteractable = false;
    }

    public bool GetInteractable()
    {
        return isInteractable;
    }

    public void SetInteractable(bool isInteractable)
    {
        this.isInteractable = isInteractable;
    }

    public void StartEndingLevel()
    {
        StartCoroutine(GameManager.Instance.EndLevel());
    }
}
