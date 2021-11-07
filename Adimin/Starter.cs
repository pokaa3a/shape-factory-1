using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Starter
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        // Debug.Log("Before first Scene loaded");
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log($"Scene {scene.name} is loaded");
        // Debug.Log("After first Scene loaded");
        Map.Instance.CreateMap();
        GamePlayPanel.Instance.ShowPanel();
        BullpenManager.Instance.Initialize();
        // LevelUtils.MakeLevelAndSave();

        // LevelSelection.Instance.ShowSelections();
    }

    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        // Debug.Log("RuntimeMethodLoad: After first Scene loaded");

    }
}
