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

using System.Collections;
using System.Collections.Generic;
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
    
    private const float _maxDistance = 30;
    private const float _capSizeGrowing = 1;
    private const float _growRate = .5f;
    private float _grownBy;
    private GameObject _gazedAtObject = null;
    private Renderer _myRenderer;
    private bool _options = false;
    private bool _archiv = false;
    private int _volume = -1;
    
    private AudioManager _audioManager;
    
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
                _gazedAtObject?.SendMessage("OnPointerExit");
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
                    _gazedAtObject = null;
                    return;
                }
            }
            SetMaterial(true);
            ChangeSize(_growRate * Time.deltaTime, _capSizeGrowing);
            _grownBy += _growRate * Time.deltaTime;
            if (_grownBy >= _capSizeGrowing)
            {
                MenuItemPressed(_gazedAtObject);
                _gazedAtObject = null;
                Reset();
            }
            
           if (_grownBy >= _capSizeGrowing)
           { 
              _gazedAtObject = null;
              Reset();   
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
                /* open level scene */
                foreach (GameObject g in items)
                {
                    if (g != gazedAtObject) g.SetActive(false);
                }
                break;
            case "archiv":
                /* load "archiv" scene */
                _archiv = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case "pillar": /*zoom to item on pillar*/ break;
            case "exit":
                /* quit game */
                Application.Quit();
                break;
            case "options":
                /* load "options" scene */
                _options = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
                break;
            case "volume+":
                /* make next bar visible */
                if (_volume <= 5)
                {
                    _volume++;
                    volumeBars[_volume].SetActive(true);
                }
                _audioManager.VolumeUp("soundtrack");
                break;
            case "volume-":
                /* make next bar invisible */
                if (_volume  >= 0)
                {
                    _volume--;
                    volumeBars[_volume].SetActive(false);
                }
                _audioManager.VolumeUp("soundtrack");
                break;
            case "back":
                SceneManager.LoadScene(0);
                if (_options)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
                if (_archiv)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                _options = false;
                _archiv = false;
                break;
        }
    }

    private void Reset()
    {
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
