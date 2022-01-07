using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    public List<Material> images;

    private CameraPointer _cameraPointer;
    
    private float _pictureRange;
    private int _index;
    private LoadLevel _levelLoader;
    public Text currentStatus;

    private void Awake()
    {
        _pictureRange = 1f/images.Count;
    }

    public void LoadingBar(float progress, int count)
    {
        transform.GetChild(1).GetComponent<Image>().material = images[_index];
        if (progress > _pictureRange * (_index + 1))
        {
            _index++;
        }

        if (progress > .5f)
        {
            currentStatus.text = "Spawning asteroids: " + count + "/2500";
        }

        if (progress > .95f)
        {
            currentStatus.text = "Have fun!";
        }
        
    }
}