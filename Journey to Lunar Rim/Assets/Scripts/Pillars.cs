using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillars : MonoBehaviour
{
    public GameObject pillar;

    [HideInInspector]
    public List<GameObject> pillars;

    private int _numPillarsPerRow = 20, _numPillarsPerCol = 20;
    // Start is called before the first frame update
    void Start()
    {
        float x = 0f;
        float y = 0f;
        for (int i = 0; i < _numPillarsPerRow; i++)
        {
            for (int j = 0; j < _numPillarsPerCol; j++)
            {
                Instantiate(pillar, new Vector3(x, y, 10f), Quaternion.identity, this.transform);
                x += 7.5f;
            }
            y += 7.5f;
            x = 0f;
        }
    }
}
