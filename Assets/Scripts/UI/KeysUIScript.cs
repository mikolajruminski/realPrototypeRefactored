using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class KeysUIScript : MonoBehaviour
{
    public static KeysUIScript Instance { get; private set; }
    [SerializeField] private Transform redKey;

    private void Awake()
    {
        Instance = this;
        redKey.gameObject.SetActive(false);
    }

    public void AdjustKeys()
    {
        redKey.gameObject.SetActive(true);
    }
}
