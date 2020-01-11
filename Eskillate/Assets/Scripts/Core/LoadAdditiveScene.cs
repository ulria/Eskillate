using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class LoadHelper
    {
        public static void LoadGenericMenus(MonoBehaviour mono)
        {
            Debug.Log("Loading Scene: PauseMenu...");
            LoadPauseMenu(mono);
            Debug.Log("Finished loading Scene: PauseMenu");
            Debug.Log("Loading Scene: LevelCompletionMenu...");
            LoadLevelCompletionMenu(mono);
            Debug.Log("Finished loading Scene: LevelCompletionMenu");
        }

        private static void LoadLevelCompletionMenu(MonoBehaviour mono)
        {
            // Add level completion menu
            mono.StartCoroutine(LoadAdditiveScene.LoadAsync("LevelCompletionMenu", OnLevelCompletionMenuLoaded));
        }

        private static void OnLevelCompletionMenuLoaded()
        {
            var levelCompletionMenuCanvas = GameObject.Find("LevelCompletionMenuCanvas");
            levelCompletionMenuCanvas.SetActive(false);
        }

        private static void LoadPauseMenu(MonoBehaviour mono)
        {
            // Add pause menu
            mono.StartCoroutine(LoadAdditiveScene.LoadAsync("PauseMenu", OnPauseMenuLoaded));
        }

        private static void OnPauseMenuLoaded()
        {
            var pauseMenuCanvas = GameObject.Find("PauseMenuCanvas");
            pauseMenuCanvas.SetActive(false);
        }

        private class LoadAdditiveScene
        {
            internal static IEnumerator LoadAsync(string scene, Action callback)
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

                while (!asyncLoad.isDone)
                {
                    yield return null;
                }

                callback();
            }
        }
    }
}