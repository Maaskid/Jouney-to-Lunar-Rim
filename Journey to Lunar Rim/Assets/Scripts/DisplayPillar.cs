using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPillar : MonoBehaviour
{
    public GameObject displayedItem;

    private bool _goUp = false;
    private float _timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displayedItem.transform.Rotate(Vector3.up * 7 * Time.deltaTime);

        if (_goUp)
        {
            _timer += .1f * Time.deltaTime;
            if (_timer > .5f) _goUp = false;
            displayedItem.transform.position += new Vector3(0f,  .1f * Time.deltaTime, 0f);
        }
        else
        {
            _timer -= .1f * Time.deltaTime;
            if (_timer < 0) _goUp = true;
            displayedItem.transform.position -= new Vector3(0f, .1f * Time.deltaTime, 0f);
        }
        
        // Debug.Log(_goUp);
    }
}
