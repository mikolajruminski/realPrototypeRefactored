using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public bool isInteractable { get; set; }
    public bool isActive { get; set; }
    public void Interact();
    public void ShowInteractableButton();
    public void HideInteractableButton();

    public bool GetInteractable();
    public void SetInteractable(bool isInteractable);


}
