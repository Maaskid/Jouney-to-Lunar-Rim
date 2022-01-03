using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScript : MonoBehaviour
{
    public List<Material> images;

    private CameraPointer _cameraPointer;
    
    private float _pictureRange;
    private int _index;
    private LoadLevel _levelLoader;

    private void Start()
    {
        _levelLoader = GetComponent<LoadLevel>();
        _pictureRange = 1f/images.Count;
        _levelLoader.LevelLoad(CameraPointer.GetSceneIndex());
    }

    public void LoadingBar(float progress)
    {
        gameObject.GetComponent<MeshRenderer>().material = images[_index];

        Debug.LogError(progress + " <?> " + _pictureRange + " * " + (_index + 1));
        if (progress > _pictureRange * (_index + 1))
        {
            _index++;
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
}