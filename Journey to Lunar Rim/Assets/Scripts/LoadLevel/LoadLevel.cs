using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    private AsyncOperation _operation;
    public LoadingProgress loadingProgress;

    public static LoadLevel CurrentLoadLevel;

    private float _totalSceneProgress, _totalSpawnProgress, _totalProgress;
    public float asteroidProgress;

    private void Awake()
    {
        CurrentLoadLevel = this;
        loadingProgress.Reset();
    }

    public void LevelLoad(int sceneIndex)
    {
        // DontDestroyOnLoad(this);
        StartCoroutine(LoadAsync(sceneIndex));
        StartCoroutine(GetTotalProgress(sceneIndex));
    }

    private IEnumerator LoadAsync(int sceneIndex)
    {
        yield return null;
        _operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!_operation.isDone)
        {
            _totalSceneProgress += _operation.progress;
            _totalSceneProgress = Mathf.Clamp01(_totalSceneProgress);
            yield return null;
        }

        _totalSceneProgress = 1;
        
    }

    private IEnumerator GetTotalProgress(int sceneIndex)
    {
        _totalProgress = 0;
        
        while (!loadingProgress.scriptActive || !loadingProgress.isDone)
        {
            
            if (!loadingProgress.scriptActive)
            {
                _totalSpawnProgress = 0;
                Debug.Log("if");
            }
            else
            {
                _totalSpawnProgress = Mathf.Clamp01(loadingProgress.spawnProgress);
                Debug.Log("el");
            }
            
            Debug.Log(_totalSceneProgress + " " + _totalSpawnProgress);
            _totalProgress = Mathf.Clamp01((_totalSceneProgress + _totalSpawnProgress) / 2f);
            UpdateProgressUI();
            yield return null;
        }

        _totalProgress = 1;
        UpdateProgressUI();
        Destroy(GameObject.Find("LoadingCanvas"));
    }

    private void UpdateProgressUI()
    {
        GetComponent<LoadingScript>().LoadingBar(_totalProgress, loadingProgress.count);
    }
}
