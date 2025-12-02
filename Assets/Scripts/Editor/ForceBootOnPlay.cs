using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefence.Editor
{
    [InitializeOnLoad]
    public static class ForceBootOnPlay
    {
        private const string BootSceneName = "Bootstrap";

        static ForceBootOnPlay()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                var current = SceneManager.GetActiveScene();

                if (current.name != BootSceneName)
                {
                    var bootScenePath = FindBootScenePath();
                    if (!string.IsNullOrEmpty(bootScenePath))
                    {
                        EditorSceneManager.OpenScene(bootScenePath);
                    }
                }
            }
        }

        private static string FindBootScenePath()
        {
            var scenes = EditorBuildSettings.scenes;

            foreach (var scene in scenes)
            {
                if (scene.path.Contains(BootSceneName))
                    return scene.path;
            }

            Debug.LogWarning($"Boot scene '{BootSceneName}' is not found! " +
                $"Verify the scene name and make sure it is added to the Build Settings.");

            return string.Empty;
        }
    }
}
