using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PlayingfieldTest : MonoBehaviour
{
    public float radius = 1;
    public Vector2 regionSize = Vector2.one;
    public int rejectionRadius = 30;
    public float displayRadius = 1;
    public List<GameObject> objs;

    List<Vector2> _points;
    private GameObject[] _gameObjects;

    private void Start()
    {
        _points = PoissonDiscSample.GeneratePoints(radius, regionSize, rejectionRadius);
        if (_points == null) return;
        _gameObjects = objs.ToArray();
        foreach (var point in _points)
        {
            Instantiate(_gameObjects[Random.Range(0, objs.Count)], point, Random.rotation);
        }
    }

    /*void OnValidate()
    {
        _points = PoissonDiscSample.GeneratePoints(radius, regionSize, rejectionRadius);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(regionSize/2, regionSize);
        if (_points == null) return;
        foreach (var point in _points)
        {
            Gizmos.DrawSphere(point, displayRadius);
        }
    }*/
}
