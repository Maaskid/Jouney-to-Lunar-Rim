using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    private AsyncOperation _operation;

    public static LoadLevel CurrentLoadLevel;

    private float _totalSceneProgess, _totalSpawnProgress, _totalProgress;
    public float asteroidProgress;

    private void Awake()
    {
        CurrentLoadLevel = this;
    }

    public void LevelLoad(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
        StartCoroutine(GetTotalProgress());
    }

    private IEnumerator LoadAsync(int sceneIndex)
    {
        _operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!_operation.isDone)
        {
            _totalSceneProgess += _operation.progress;
            _totalSceneProgess = Mathf.Clamp01(_totalSceneProgess);
            yield return null;
        }

        _totalSceneProgess = 1;
    }

    private IEnumerator GetTotalProgress()
    {
        _totalProgress = 0;

        while (PlaceAsteroids.CurrentPlaceAsteroids == null || !PlaceAsteroids.CurrentPlaceAsteroids.isDone)
        {
            if (PlaceAsteroids.CurrentPlaceAsteroids == null)
            {
                _totalSpawnProgress = 0;
            }
            else
            {
                _totalSpawnProgress = Mathf.Clamp01(asteroidProgress);
                // _totalSpawnProgress = Mathf.Round(PlaceAsteroids.currentPlaceAsteroids.progress * 100f);
            }
            
            Debug.Log(PlaceAsteroids.CurrentPlaceAsteroids);
            _totalProgress = Mathf.Clamp01((_totalSceneProgess + _totalSpawnProgress) / 2f);
            // _totalProgress = Mathf.Round((_totalSceneProgess + _totalSpawnProgress) / 2f);
            UpdateProgressUI();
            yield return null;
        }
    }

    private void UpdateProgressUI()
    {
        GetComponent<LoadingScript>().LoadingBar(_totalProgress);
    }
}
