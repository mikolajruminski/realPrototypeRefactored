using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out IDamageable component))
        {
            component.Damage(1);
        }
    }
}
