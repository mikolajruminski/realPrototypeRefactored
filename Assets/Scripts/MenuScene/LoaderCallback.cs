using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    [SerializeField] private int loadSceneDelay = 2;

    private void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneDelay());
    }


    private IEnumerator LoadSceneDelay()
    {
        yield return new WaitForSecondsRealtime(loadSceneDelay);
        Loader.LoaderCallback();
    }
}
