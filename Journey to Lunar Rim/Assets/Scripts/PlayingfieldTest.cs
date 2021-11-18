using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayingfieldTest : MonoBehaviour
{
    public float radius = 1;
    public Vector3 regionSize = Vector3.right;
    public int rejectionRadius = 30;
    public List<GameObject> objs;

    List<Vector3> _points;
    private GameObject[] _gameObjects;

    void Start()
    {
        _points = PoissonDiscSample.GeneratePoints(radius, regionSize, rejectionRadius);
        if (_points == null) return;
        _gameObjects = objs.ToArray();
        foreach (var point in _points)
        {
            Instantiate(_gameObjects[Random.Range(0, objs.Count)], point, Random.rotation);
        }
    }

}