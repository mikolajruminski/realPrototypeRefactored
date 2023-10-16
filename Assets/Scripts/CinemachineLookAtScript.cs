using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineLookAtScript : MonoBehaviour
{
    public static CinemachineLookAtScript Instance;
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ChangeTarget(Transform target)
    {
        cinemachineVirtualCamera.Follow = target;
    }
}
