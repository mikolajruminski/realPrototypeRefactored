using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadStomperAttack : MonoBehaviour
{
    public static HeadStomperAttack Instance { get; private set; }

    public event EventHandler OnHeadStomperAttack;
    [SerializeField] private float knockupAmount = 4f;
    [SerializeField] private int damageAmount;
    private float damageCooldown = 0.5f;
    private bool canBeDamaged = true;

    private void Awake()
    {
        Instance = this;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable component))
        {
            if (canBeDamaged)
            {
                component.Damage(damageAmount);
                StartCoroutine(DamageCooldown());
                shotPlayer();
                OnHeadStomperAttack?.Invoke(this, EventArgs.Empty);
            }

        }
    }

    private void shotPlayer()
    {
        Player.Instance.GetPlayerRB().velocity = Vector2.up * knockupAmount;
    }

    private IEnumerator DamageCooldown()
    {
        canBeDamaged = false;

        yield return new WaitForSeconds(damageCooldown);

        canBeDamaged = true;

    }
}
