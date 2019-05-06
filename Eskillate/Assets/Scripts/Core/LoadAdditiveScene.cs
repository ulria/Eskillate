using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class LoadAdditiveScene
    {
        public static IEnumerator LoadAsync(string scene)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            Debug.Log("Loading Scene: " + scene + "...");

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            Debug.Log("Finished loading Scene: " + scene);

            var test = GameObject.Find("PauseMenuCanvas");
            test.SetActive(false);
        }
    }
}