//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    public Material inactiveMaterial;
    public Material activeMaterial;
    public List<GameObject> items;
    public List<GameObject> ignore;
    public List<GameObject> volumeBars;
    public List<GameObject> sfxBars;
    public LoadingProgress loadingProgress;
    public List<GameObject> levelDescriptions;

    private const float _maxDistance = 50;
    private const float _capSizeGrowing = 1;
    private const float _growRate = .5f;
    private float _grownBy;
    private GameObject _gazedAtObject = null;
    private Renderer _myRenderer;
    private bool _options;
    private bool _archiv;
    private bool _level1;
    private bool _level2;
    private bool _level3;
    private bool _level4;
    private bool _level5;
    private bool _level6;
    private int _volume = -1;
    private int _sfx = -1;
    
    private int _levelToLoad;
    
    private AudioManager _audioManager;

    private void Awake()
    {
        if (loadingProgress!=null)
        {
            loadingProgress.ResetProgress();
        }
    }

    public void Start()
    {
        _myRenderer = GetComponent<Renderer>();
        _audioManager = GetComponent<AudioManager>();
        SetMaterial(false);
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                // New GameObject.
                if (_gazedAtObject!=null)
                {
                    _gazedAtObject?.SendMessage("OnPointerExit");
                }
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnter");
            }

            foreach (GameObject g in ignore)
            {
                if (g == _gazedAtObject)
                {
                    _gazedAtObject.SendMessage("OnPointerExit");
                    Reset();
                    SetMaterial(false);
                    return;
                }
            }
            SetMaterial(true);
            ChangeSize(_growRate * Time.deltaTime, _capSizeGrowing);
            _grownBy += _growRate * Time.deltaTime;
            if (_grownBy >= _capSizeGrowing)
            {
                MenuItemPressed(_gazedAtObject);
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            SetMaterial(false);
            _gazedAtObject?.SendMessage("OnPointerExit");
        }


        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClick");
        }
    }

    private void MenuItemPressed(GameObject gazedAtObject)
    {
        switch (gazedAtObject.tag)
        {
            case "play":
                /* open level scene 1 */
                loadingProgress.sceneToLoad = SceneIndexes.Level1;
                SceneManager.LoadSceneAsync((int)SceneIndexes.LevelLoading);
                break;
            case "archiv":
                /* load "archiv" scene */
                _archiv = true;
                SceneManager.LoadScene((int)SceneIndexes.Archiv);
                break;
            case "pillar": /*zoom to item on pillar*/ break;
            case "exit":
                /* quit game */
                Application.Quit();
                break;
            case "options":
                /* load "options" scene */
                _options = true;
                SceneManager.LoadScene((int)SceneIndexes.Options);
                break;
            case "volume+":
                /* make next bar visible */
                if (_volume < 5)
                {
                    _volume++;
                    volumeBars[_volume].SetActive(true);
                }
                Reset();
                _audioManager.VolumeUp("soundtrack");
                break;
            case "volume-":
                /* make next bar invisible */
                if (_volume >= 0)
                {
                    volumeBars[_volume].SetActive(false);
                    _volume--;
                }
                Reset();
                _audioManager.VolumeUp("soundtrack");
                break;
            case "sfx+": 
                if (_sfx < 5)
                {
                    _sfx++;
                    sfxBars[_sfx].SetActive(true);
                }
                Reset();
                _audioManager.VolumeUp("soundtrack");
                break;
            case "sfx-": 
                if (_sfx >= 0)
                {
                    sfxBars[_sfx].SetActive(false);
                    _sfx--;
                }
                Reset();
                _audioManager.VolumeUp("soundtrack");
                break;
            case "back":
                SceneManager.LoadScene((int)SceneIndexes.Menu);
                _options = false;
                _archiv = false;
                break;
            case "level1":
                loadingProgress.ResetRetry();
                ActivateInformation(1, SceneIndexes.Level1);
                break;
            case "level2":
                loadingProgress.ResetRetry();
                ActivateInformation(2, SceneIndexes.Level2);
                break;
            case "level3":
                loadingProgress.ResetRetry();
                ActivateInformation(3, SceneIndexes.Level3);
                break;
            case "level4":
                loadingProgress.ResetRetry();
                ActivateInformation(4, SceneIndexes.Level4);
                break;
            case "level5":
                loadingProgress.ResetRetry();
                ActivateInformation(5, SceneIndexes.Level5);
                break;
            case "level6":
                loadingProgress.ResetRetry();
                ActivateInformation(6, SceneIndexes.Level6);
                break;
            case "startLevel":
                loadingProgress.ResetRetry();
                SceneManager.LoadSceneAsync((int)SceneIndexes.LevelLoading);
                break;
            case "retry":
                loadingProgress.retry = true;
                SceneManager.LoadSceneAsync((int) SceneIndexes.LevelLoading);
                break;
            case "home":
                loadingProgress.ResetRetry();
                SceneManager.LoadScene((int)SceneIndexes.Menu);
                break;
        }
    }

    private void ActivateInformation(int containerIndex, SceneIndexes sceneIndex)
    {
        foreach (var levelInfo in levelDescriptions)
        {
            if (levelInfo.name.Contains(containerIndex.ToString()))
            {
                levelInfo.SetActive(true);
                continue;
            }
            levelInfo.SetActive(false);
        }
        loadingProgress.sceneToLoad = sceneIndex;
        Debug.Log((int)sceneIndex);
        ignore.Remove(GameObject.Find("Start"));
    }

    private void Reset()
    {
        _gazedAtObject = null;
        _grownBy = 0;
        ResetSize(.5f);
    }

    private void ResetSize(float resizeBy)
    {
        transform.localScale = new Vector3(resizeBy, resizeBy, resizeBy);
    }

    private void ChangeSize(float changeBy, float capAt)
    {
        if (transform.localScale.x > capAt) return;
        transform.localScale += new Vector3(changeBy, changeBy, changeBy);
    }

    private void SetMaterial(bool isEntered)
    {
        if (inactiveMaterial != null && activeMaterial != null)
        {
            _myRenderer.material = isEntered ? activeMaterial : inactiveMaterial;
        } 
    }
}
