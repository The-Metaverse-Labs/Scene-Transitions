using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SceneTransitions
{
    public class SceneLoader : MonoBehaviour
    {
        public string AScene;
        public string BScene;
        public enum LoaderType
        {
            A, B, Neither
        }
        public LoaderType IsInScene;

        private void Awake()
        {
            // Remove Indicator in game
            transform.Find("Indicator").gameObject.SetActive(false);

            // Remove extra colliders
            if (IsInScene == LoaderType.A)
            {
                DestroyImmediate(transform.Find("B Side").gameObject);
                DestroyImmediate(transform.Find("B Side Right").gameObject);
                DestroyImmediate(transform.Find("B Side Left").gameObject);
            }
            else if (IsInScene == LoaderType.B)
            {
                DestroyImmediate(transform.Find("A Side").gameObject);
                DestroyImmediate(transform.Find("A Side Right").gameObject);
                DestroyImmediate(transform.Find("A Side Left").gameObject);
            }
        }

        public void LoadAScene()
        {
            // don't load if already loaded
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.path == AScene)
                {
                    //Debug.Log($"[SceneLoader] {AScene} already loaded");
                    return;
                }
            }

            // load
            //Debug.Log($"[SceneLoader] Loading Scene {AScene}");
            SceneManager.LoadSceneAsync(AScene, LoadSceneMode.Additive);
        }

        public void LoadBScene()
        {
            // don't load if already loaded
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.path == BScene)
                {
                    //Debug.Log($"[SceneLoader] {BScene} already loaded");
                    return;
                }
            }

            // load
            //Debug.Log($"[SceneLoader] Loading Scene {BScene}");
            SceneManager.LoadSceneAsync(BScene, LoadSceneMode.Additive);
        }

        public void RemoveAScene()
        {
            // Check loaded scenes for target scene to unload
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.path.Equals(AScene))
                {
                    // Unload the scene if it's found
                    //Debug.Log($"[SceneLoader] Removing Scene {AScene}");
                    SceneManager.UnloadSceneAsync(scene);
                    return;
                }
            }
            //Debug.Log($"[SceneLoader] couldn't unload scene {AScene}");
        }

        public void RemoveBScene()
        {
            // Check loaded scenes for target scene to unload
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.path.Equals(BScene))
                {
                    // Unload the scene if it's found
                    //Debug.Log($"[SceneLoader] Removing Scene {BScene}");
                    SceneManager.UnloadSceneAsync(scene);
                    return;
                }
            }
            //Debug.Log($"[SceneLoader] couldn't unload scene {BScene}");
        }
    }
}
