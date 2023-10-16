using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    private static int LoadSceneDelayTime = 2;

    public enum Scene
    {
        CongratulationsScene,
        LoadingScene,
        MenuScene,
        GameScene,
    }

    private static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());

    }

    public static void LoaderCallback()
    {
        SceneManager.LoadSceneAsync(targetScene.ToString());
    }

}
