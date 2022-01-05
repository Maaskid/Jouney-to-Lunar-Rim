using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class KeepMe : MonoBehaviour
{
    public LoadingProgress loadingProgress;
    private void Start()
    {
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
