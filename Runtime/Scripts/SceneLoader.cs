using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace SceneTransitions
{
    public class SceneLoader : MonoBehaviour
    {
        public static Dictionary<string, SceneInstance> loadedScenes = new Dictionary<string, SceneInstance>();

        public AssetReference AScene;
        public AssetReference BScene;
        
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
            if (AScene.Asset != null)
                return;
            if (loadedScenes.ContainsKey(AScene.AssetGUID))
                return;
            
            // load
            loadedScenes.Add(AScene.AssetGUID, default);
            Addressables.LoadSceneAsync(AScene, LoadSceneMode.Additive).Completed += (op) =>
            {
                loadedScenes[AScene.AssetGUID] = op.Result;
                SceneManager.SetActiveScene(op.Result.Scene);
            };
        }

        public void LoadBScene()
        {
            // don't load if already loaded
            if (BScene.Asset != null)
                return;
            if (loadedScenes.ContainsKey(BScene.AssetGUID))
                return;

            // load
            loadedScenes.Add(BScene.AssetGUID, default);
            Addressables.LoadSceneAsync(BScene, LoadSceneMode.Additive).Completed += (op) =>
            {
                loadedScenes[BScene.AssetGUID] = op.Result;
                SceneManager.SetActiveScene(op.Result.Scene);
            };
        }

        public void RemoveAScene()
        {
            // don't remove if not loaded
            if (!loadedScenes.ContainsKey(AScene.AssetGUID))
                return;
            else if (loadedScenes[AScene.AssetGUID].Equals(default))
                return;

            // unload and release resources
            Addressables.UnloadSceneAsync(loadedScenes[AScene.AssetGUID], true).Completed += (op) =>
            {
                //Debug.Log($"Unloaded {AScene.AssetGUID} {op.Status.ToString()}");
            };
            loadedScenes.Remove(AScene.AssetGUID);
        }

        public void RemoveBScene()
        {
            // don't remove if not loaded
            if (!loadedScenes.ContainsKey(BScene.AssetGUID))
                return;
            else if (loadedScenes[BScene.AssetGUID].Equals(default))
                return;

            // unload and release resources
            Addressables.UnloadSceneAsync(loadedScenes[BScene.AssetGUID], true).Completed += (op) =>
            {
                //Debug.Log($"Unloaded {BScene.AssetGUID} {op.Status.ToString()}");
            };
            loadedScenes.Remove(BScene.AssetGUID);
        }
    }
}
