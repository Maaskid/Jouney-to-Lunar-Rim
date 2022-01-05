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


    private float _totalSceneProgress, _totalSpawnProgress, _totalProgress;

    private void Awake()
    {
        loadingProgress.Reset();
    }

    public void LevelLoad(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
        StartCoroutine(GetTotalProgress());
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
        if (loadingProgress.scriptActive)
        {
            GetComponent<Canvas>().worldCamera = GameObject.Find("Player").transform.GetChild(0).GetComponent<Camera>();
            GetComponent<Canvas>().planeDistance = 0.1f;
        }
        _totalSceneProgress = 1;
    }

    private IEnumerator GetTotalProgress()
    {
        _totalProgress = 0;
        while (!loadingProgress.scriptActive || !loadingProgress.isDone)
        {
            if (!loadingProgress.scriptActive)
            {
                _totalSpawnProgress = 0;
            }
            else
            {
                _totalSpawnProgress = Mathf.Clamp01(loadingProgress.spawnProgress);
            }
            
            Debug.Log(_totalSceneProgress + " " + _totalSpawnProgress);
            _totalProgress = Mathf.Clamp01((_totalSceneProgress + _totalSpawnProgress) / 2f);
            UpdateProgressUI();
            yield return null;
        }
        _totalProgress = 1;
        UpdateProgressUI();
    }

    private void UpdateProgressUI()
    {
        GetComponent<LoadingScript>().LoadingBar(_totalProgress, loadingProgress.count);
    }
}
