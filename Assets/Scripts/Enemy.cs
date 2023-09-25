using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    public int Health
    {
        get => health;
        set => health = value;
    }

    [SerializeField] private int speed;
    [SerializeField] private int health;

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
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
}
