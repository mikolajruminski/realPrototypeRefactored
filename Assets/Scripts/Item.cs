using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] protected AllItemsSO allItemsSO;
    public bool isInteractable { get => _isInteractable; set => _isInteractable = value; }
    public bool isActive { get => _isActive; set => _isActive = value; }

    private bool _isInteractable = false;
    private bool _isActive = true;

    //floating
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private void Start()
    {
        posOffset = transform.position;
    }

    private void Update()
    {
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

    public virtual void Interact()
    {

    }


    public virtual void ShowInteractableButton()
    {

    }

    public virtual bool GetInteractable()
    {
        return isInteractable;
    }

    public virtual void SetInteractable(bool isInteractable)
    {
        this.isInteractable = isInteractable;
    }

    public virtual void HideInteractableButton()
    {

    }
}
