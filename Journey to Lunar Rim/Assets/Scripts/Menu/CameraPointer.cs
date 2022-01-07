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
        _audioManager = FindObjectOfType<AudioManager>();
        if (volumeBars.Count != 0 && sfxBars.Count != 0)
        {
            DisplayVolume(volumeBars, loadingProgress.musicVolume);
            DisplayVolume(sfxBars, loadingProgress.sfxVolume);
        }
        
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
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                /* open level scene 1 */
                loadingProgress.sceneToLoad = SceneIndexes.Level1;
                _audioManager.Stop(SoundNames.MenuTheme.ToString());
                _audioManager.Play(SoundNames.InGameTheme.ToString());
                SceneManager.LoadSceneAsync((int)SceneIndexes.LevelLoading);
                Reset();
                break;
            case "archiv":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                /* load "archiv" scene */
                _archiv = true;
                SceneManager.LoadScene((int)SceneIndexes.Archiv);
                Reset();
                break;
            case "pillar": /*zoom to item on pillar*/ break;
            case "exit":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                /* quit game */
                Application.Quit();
                Reset();
                break;
            case "options":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                /* load "options" scene */
                _options = true;
                SceneManager.LoadScene((int)SceneIndexes.Options);
                Reset();
                break;
            case "volume+":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                if (loadingProgress.musicVolume < 5)
                {
                    loadingProgress.musicVolume += 1;
                    _audioManager.VolumeUp(SoundType.Music);
                    DisplayVolume(volumeBars, loadingProgress.musicVolume);
                }
                Reset();
                break;
            case "volume-":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                if (loadingProgress.musicVolume > 0)
                {
                    loadingProgress.musicVolume -= 1;
                    _audioManager.VolumeDown(SoundType.Music);
                    DisplayVolume(volumeBars, loadingProgress.musicVolume);
                }
                Reset();
                break;
            case "sfx+": 
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                if (loadingProgress.sfxVolume < 5)
                {
                    loadingProgress.sfxVolume += 1;
                    _audioManager.VolumeUp(SoundType.Sfx);
                    DisplayVolume(sfxBars, loadingProgress.sfxVolume);
                }
                Reset();
                break;
            case "sfx-":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                if (loadingProgress.sfxVolume > 0)
                {
                    loadingProgress.sfxVolume -= 1;
                    _audioManager.VolumeDown(SoundType.Sfx);
                    DisplayVolume(sfxBars, loadingProgress.sfxVolume);
                }
                Reset();
                break;
            case "back":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                SceneManager.LoadScene((int)SceneIndexes.Menu);
                _options = false;
                _archiv = false;
                Reset();
                break;
            case "level1":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                loadingProgress.ResetRetry();
                ActivateInformation(1, SceneIndexes.Level1);
                Reset();
                break;
            case "level2":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                loadingProgress.ResetRetry();
                ActivateInformation(2, SceneIndexes.Level2);
                Reset();
                break;
            case "level3":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                loadingProgress.ResetRetry();
                ActivateInformation(3, SceneIndexes.Level3);
                Reset();
                break;
            case "level4":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                loadingProgress.ResetRetry();
                ActivateInformation(4, SceneIndexes.Level4);
                Reset();
                break;
            case "level5":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                loadingProgress.ResetRetry();
                ActivateInformation(5, SceneIndexes.Level5);
                Reset();
                break;
            case "level6":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                loadingProgress.ResetRetry();
                ActivateInformation(6, SceneIndexes.Level6);
                Reset();
                break;
            case "startLevel":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                loadingProgress.ResetRetry();
                _audioManager.Stop(SoundNames.MenuTheme.ToString());
                _audioManager.Play(SoundNames.InGameTheme.ToString());
                SceneManager.LoadSceneAsync((int)SceneIndexes.LevelLoading);
                Reset();
                break;
            case "retry":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                loadingProgress.retry = true;
                SceneManager.LoadSceneAsync((int) SceneIndexes.LevelLoading);
                Reset();
                break;
            case "home":
                _audioManager.PlayOneShot(SoundNames.Button1.ToString());
                loadingProgress.ResetRetry();
                SceneManager.LoadScene((int)SceneIndexes.Menu);
                Reset();
                break;
        }
    }

    private void DisplayVolume(List<GameObject> bar, float volume)
    {
        switch (volume)
        {
            case 0:
                foreach (var tick in bar)
                {
                    tick.SetActive(false);
                }
                break;
            case 1:
                for (var i = 4; i > 0; i--)
                {
                    bar[i].SetActive(false);
                }
                bar[0].SetActive(true);
                break;
            case 2:
                for (var i = 4; i > 1; i--)
                {
                    bar[i].SetActive(false);
                }
                for (var i = 1; i >= 0; i--)
                {
                    bar[i].SetActive(true);
                }
                break;
            case 3:
                for (var i = 4; i > 2; i--)
                {
                    bar[i].SetActive(false);
                }
                for (var i = 2; i >= 0; i--)
                {
                    bar[i].SetActive(true);
                }
                break;
            case 4:
                bar[4].SetActive(false);
                for (var i = 3; i >= 0; i--)
                {
                    bar[i].SetActive(true);
                }
                break;
            case 5:
                foreach (var tick in bar)
                {
                    tick.SetActive(true);
                }
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
