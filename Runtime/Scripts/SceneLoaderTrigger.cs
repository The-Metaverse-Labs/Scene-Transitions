using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderTrigger: MonoBehaviour
{
    public enum TriggerType
    {
        ASide, BSide, Center
    }
    public TriggerType Type;
    public SceneLoader Loader { get { return GetComponentInParent<SceneLoader>(); } }
    
    public void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.Equals(Camera.main.gameObject)) return;

        //Debug.Log($"[SceneLoaderTrigger] Triggered {Type}");
        if (Type == TriggerType.ASide)
        {
            Loader.RemoveBScene();
        }
        else if (Type == TriggerType.BSide)
        {
            Loader.RemoveAScene();
        }
        else
        {
            Loader.LoadAScene();
            Loader.LoadBScene();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        
    }
}