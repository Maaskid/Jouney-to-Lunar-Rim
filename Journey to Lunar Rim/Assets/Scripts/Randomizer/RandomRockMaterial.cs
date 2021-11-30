using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomRockMaterial : MonoBehaviour
{
    public Material[] materials;
    // Start is called before the first frame update
    void Start()
    {
        ApplyMaterial();
        ApplyRandomRotation();
    }

    private void ApplyRandomRotation()
    {
        transform.rotation = Random.rotation;
    }

    void ApplyMaterial()
    {
        GetComponent<Renderer>().material = materials[Random.Range(0,3)];
    }
}
