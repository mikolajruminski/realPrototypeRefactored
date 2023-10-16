using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;
using Cinemachine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Transform cameraMainMenuPoint, cameraOptionsPoint;
    [SerializeField] private CinemachineVirtualCamera vmCam;

    public void ChangeCameraToOptions()
    {
        vmCam.Follow = cameraOptionsPoint;
    }


    public void ChangeCameraToMenu()
    {
        vmCam.Follow = cameraMainMenuPoint;
    }
}
