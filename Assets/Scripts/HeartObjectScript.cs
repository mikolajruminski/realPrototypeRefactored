using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartObjectScript : MonoBehaviour
{
    public event EventHandler OnPlayerTriggerEntered;
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (PlayerDamage.Instance.Health < PlayerDamage.Instance.GetMaxPlayerHealth())
            {
                PlayerDamage.Instance.Health++;
                OnPlayerTriggerEntered?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
