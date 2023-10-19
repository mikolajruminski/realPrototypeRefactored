using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private const string PROPERTY_NAME = "_Progress";

    public event EventHandler OnTransitionDone;
    [SerializeField] private Material transaitionMaterial;
    [SerializeField] private float transitionTime = 1f;

    private void Awake()
    {
        transaitionMaterial.SetFloat(PROPERTY_NAME, 0);
    }

    public void StartSceneTransition()
    {
        StartCoroutine(TransitionCoroutine());
    }

    public void StartScreenFadeIn()
    {
        StartCoroutine(ScreenFadeInAnimation());
    }
    public void StartScreenFadeOut()
    {
        StartCoroutine(ScreenFadeOutAnimation());
    }

    private IEnumerator TransitionCoroutine()
    {
        float currentTime = transitionTime;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            transaitionMaterial.SetFloat(PROPERTY_NAME, Mathf.Clamp01(currentTime));
            yield return null;
        }
        OnTransitionDone?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator ScreenFadeInAnimation()
    {

        float currentTime = 0;
        while (currentTime < transitionTime)
        {
            currentTime += Time.unscaledDeltaTime;
            transaitionMaterial.SetFloat(PROPERTY_NAME, Mathf.Clamp01(currentTime));
            yield return null;
        }
    }
    private IEnumerator ScreenFadeOutAnimation()
    {
        float currentTime = transitionTime;
        while (currentTime > 0)
        {
            currentTime -= Time.unscaledDeltaTime;
            transaitionMaterial.SetFloat(PROPERTY_NAME, Mathf.Clamp01(currentTime));
            yield return null;
        }
    }
}
