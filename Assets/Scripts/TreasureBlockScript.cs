using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBlockScript : MonoBehaviour
{
    public event EventHandler OnTriggerWithPlayer;
    [SerializeField] private Transform itemToSpawn;
    [SerializeField] private Transform newParent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            OnTriggerWithPlayer?.Invoke(this, EventArgs.Empty);
            Instantiate(itemToSpawn, transform.position, Quaternion.identity, newParent);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
