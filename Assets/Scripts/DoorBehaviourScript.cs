using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DoorBehaviourScript : MonoBehaviour, IInteractable
{
    public event EventHandler OnInteractableDetected;
    public event EventHandler OnInteractableEnded;
    [SerializeField] InventoryManager.AllItems requiredItem;
    [SerializeField] bool isDoorClosed;
    [SerializeField] private Vector3 offset;
    private Vector2 velocity = Vector2.zero;
    [SerializeField] private float speed;
    private bool _isActive = true;

    private bool _isInteractable = false;

    public bool isInteractable { get => _isInteractable; set => _isInteractable = value; }
    public bool isActive { get => _isActive; set => _isActive = value; }

    public void Interact()
    {
        if (HasRequiredItem())
        {
            Debug.Log("in position to interact with:" + gameObject.name + "And bool of item required is: " + HasRequiredItem());
            StartCoroutine(MoveTowards());

        }

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

    private IEnumerator MoveTowards()
    {
        float time = speed;


        while (time > 0)
        {
            CinemachineLookAtScript.Instance.ChangeTarget(transform);
            time -= Time.deltaTime;
            transform.position = Vector2.SmoothDamp(transform.position, transform.position + offset, ref velocity, 2f);
            yield return null;
        }
        yield return null;

        CinemachineLookAtScript.Instance.ChangeTarget(Player.Instance.transform);

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

    bool IInteractable.GetInteractable()
    {
        return isInteractable;
    }

    public void SetInteractable(bool isInteractable)
    {
        this.isInteractable = isInteractable;
    }
}
