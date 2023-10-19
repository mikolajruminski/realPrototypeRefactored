using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class JumpPadScript : MonoBehaviour
{
    public event EventHandler OnLaunchPerformed;
    public static event EventHandler OnAnyLaunchPerformed;

    public static void ResetStaticData()
    {
        OnAnyLaunchPerformed = null;
    }
    [SerializeField] private float jumpMultiplier;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpMultiplier);
            OnLaunchPerformed?.Invoke(this, EventArgs.Empty);
            OnAnyLaunchPerformed?.Invoke(this, EventArgs.Empty);
        }
    }
}
