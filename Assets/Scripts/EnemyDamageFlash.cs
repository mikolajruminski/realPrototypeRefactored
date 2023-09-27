using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageFlash : MonoBehaviour
{
    private const string HIT = "_Hit";
    [SerializeField] private Enemy enemy;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        enemy.OnGetDamaged += Enemy_OnGetDamaged;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Enemy_OnGetDamaged(object sender, EventArgs e)
    {
        InvokeDamageFlashCoroutine();
    }
    private IEnumerator DamageFlash()
    {
        spriteRenderer.material.SetInt(HIT, 1);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material.SetInt(HIT, 0);
    }

    private void InvokeDamageFlashCoroutine()
    {
        StartCoroutine(DamageFlash());
    }

}
