using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    private float smoothTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = player.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
