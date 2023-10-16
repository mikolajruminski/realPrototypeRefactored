using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetection : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask groundLayer;
    private bool canDetect = true;
    private const string GROUND = "Ground";

    private void Update()
    {
        DetectLedges();

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void DetectLedges()
    {
        if (canDetect)
        {
            Player.Instance.ledgeDetection = Physics2D.OverlapCircle(transform.position, radius, groundLayer);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(GROUND))
        {
            canDetect = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(GROUND))
        {
            canDetect = true;
        }
    }
}
