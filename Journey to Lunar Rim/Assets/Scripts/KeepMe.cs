using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using ScriptableObjects.Scripts;
using UnityEngine;

public class KeepMe : MonoBehaviour
{
    public static KeepMe Instance;
    public LoadingProgress loadingProgress;
    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (loadingProgress.isDone)
        {
            Destroy(gameObject);
        }
    }
}
