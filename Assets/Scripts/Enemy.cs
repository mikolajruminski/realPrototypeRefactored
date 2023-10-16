using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public event EventHandler OnGetDamaged;
    public event EventHandler OnEnemyDeath;
    private EnemyItemDrop enemyItemDrop;

    public int Health
    {
        get => health;
        set => health = value;
    }

    [SerializeField] private int speed;
    [SerializeField] private int health;
    [SerializeField] private int damage;

    private void Start()
    {
        enemyItemDrop = GetComponent<EnemyItemDrop>();
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        OnGetDamaged?.Invoke(this, EventArgs.Empty);
        Debug.Log(health);

        if (health <= 0)
        {
            enemyItemDrop.Drop(transform.position);

            OnEnemyDeath?.Invoke(this, EventArgs.Empty);
            
            this.enabled = false;
            StartCoroutine(DestroySelf());
        }
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
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
