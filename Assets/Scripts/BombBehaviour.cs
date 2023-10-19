using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    public event EventHandler OnBombDestroyed;
    private int damage = 2;
    private Rigidbody2D bombRb;
    private float degreesPerSecond = -800;
    public enum State
    {
        ToBottom,
        ToRight,
        ToLeft,
        ToTop,
    }

    [SerializeField] private State state;
    [SerializeField] private float force;


    private void Start()
    {

        bombRb = GetComponent<Rigidbody2D>();

        switch (state)
        {
            case State.ToBottom:
                bombRb.AddForce(Vector2.down * force, ForceMode2D.Impulse);
                break;
            case State.ToRight:
                bombRb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
                break;
            case State.ToLeft:
                bombRb.AddForce(Vector2.left * force, ForceMode2D.Impulse);
                break;
            case State.ToTop:
                bombRb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
                break;
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, degreesPerSecond) * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnBombDestroyed?.Invoke(this, EventArgs.Empty);

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        this.enabled = false;
    }

    public void SetBombState(State state)
    {
        this.state = state;
    }

    public State GetBombState()
    {
        return state;
    }

    public void SetForce(float force)
    {
        this.force = force;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public int GetDamage() 
    {
        return damage;
    }

}
