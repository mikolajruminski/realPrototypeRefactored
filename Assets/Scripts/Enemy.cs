using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public event EventHandler OnGetDamaged;

    public int Health
    {
        get => health;
        set => health = value;
    }

    [SerializeField] private int speed;
    [SerializeField] private int health;
    [SerializeField] private int damage;

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        OnGetDamaged?.Invoke(this, EventArgs.Empty);
        Debug.Log(health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int GetSpeed()
    {
        return speed;
    }
    public int GetDamege()
    {
        return damage;
    }

}
