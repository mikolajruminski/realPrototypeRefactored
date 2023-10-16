using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour, IDamageable
{
    public static PlayerDamage Instance { get; private set; }
    public event EventHandler OnGetDamaged;
    public event EventHandler OnPlayerDeath;
    [SerializeField] private int maxPlayerHealth = 3;

    [Header("Damageable cooldown")]
    [SerializeField] private float playerDamageCooldown = 1f;
    private bool canGetDamaged;
    public int Health
    {
        get => playerHealth;
        set => playerHealth = value;
    }

    [Header("Health points")]
    [SerializeField] private int playerHealth;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerHealth = maxPlayerHealth;
        canGetDamaged = true;
    }

    public void Damage(int damageAmount)
    {
        playerHealth -= damageAmount;
        OnGetDamaged?.Invoke(this, EventArgs.Empty);
        StartCoroutine(GetDamageCountdown());

        if (playerHealth <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        GameManager.Instance.SetIsGameActive(false);
        canGetDamaged = false;
        Time.timeScale = 0f;
        StartCoroutine(GameResetOnDeath());
    }

    private IEnumerator GetDamageCountdown()
    {
        canGetDamaged = false;
        yield return new WaitForSeconds(playerDamageCooldown);
        canGetDamaged = true;
    }

    private IEnumerator GameResetOnDeath()
    {
        yield return new WaitForSecondsRealtime(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool CanGetDamaged()
    {
        return canGetDamaged;
    }
    public int GetMaxPlayerHealth()
    {
        return maxPlayerHealth;
    }


}
