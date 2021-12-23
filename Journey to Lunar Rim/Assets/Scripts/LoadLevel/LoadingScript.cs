using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScript : MonoBehaviour
{
    public List<Texture> images;

    private int _pictureRange;
    private float _loadingComplementary = .0f;
    private int _index = 0;

    private void Start()
    {
        _pictureRange = 1/images.Count;
    }

    public void LoadingBar(float loading)
    {
        gameObject.GetComponent<Material>().mainTexture = images[_index];

        if (!(loading > _loadingComplementary + _pictureRange)) return;
        _loadingComplementary += loading;
        _index++;
    }
}
