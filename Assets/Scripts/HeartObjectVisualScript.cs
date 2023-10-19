using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartObjectVisualScript : MonoBehaviour
{

    [SerializeField] private HeartObjectScript heartObjectScript;
    private void Awake()
    {
        heartObjectScript = heartObjectScript.GetComponent<HeartObjectScript>();
    }

    public void SelfDestruct()
    {
        heartObjectScript.DestroySelf();
    }

}
