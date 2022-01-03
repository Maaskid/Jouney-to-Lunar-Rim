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

    private float _totalSceneProgress, _totalSpawnProgress, _totalProgress;
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
            _totalSceneProgress += _operation.progress;
            _totalSceneProgress = Mathf.Clamp01(_totalSceneProgress);
            yield return null;
        }

        _totalSceneProgress = 1;
    }

    private IEnumerator GetTotalProgress()
    {
        _totalProgress = 0;

        while (PlaceAsteroids.CurrentPlaceAsteroids == null || !PlaceAsteroids.CurrentPlaceAsteroids.isDone)
        {
            if (PlaceAsteroids.CurrentPlaceAsteroids == null)
            {
                _totalSpawnProgress = 0;
                Debug.Log("if");
            }
            else
            {
                _totalSpawnProgress = Mathf.Clamp01(PlaceAsteroids.CurrentPlaceAsteroids.progress);
                // _totalSpawnProgress = Mathf.Round(asteroidProgress);
                Debug.Log("el");
            }
            
            Debug.Log(_totalSceneProgress + " " + _totalSpawnProgress);
            _totalProgress = Mathf.Clamp01((_totalSceneProgress + _totalSpawnProgress) / 2f);
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
