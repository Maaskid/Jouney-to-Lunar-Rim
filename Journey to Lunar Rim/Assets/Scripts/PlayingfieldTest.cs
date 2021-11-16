using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingfieldTest : MonoBehaviour
{
    public float radius = 1;
    public Vector2 regionSize = Vector2.one;
    public int rejectionRadius = 30;
    public float displayRadius = 1;

    List<Vector2> _points;

    void OnValidate()
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
    }
}
