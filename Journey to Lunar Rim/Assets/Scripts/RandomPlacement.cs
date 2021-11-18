using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPlacement : MonoBehaviour
{
    public int x, y, z;
    public int xOffset, yOffset, zOffset;
    public int radius;
    public int delta;
    public int numberOfObjects;
    public int maxNumberOfTrys;
    public List<GameObject> objects;
    
    private GameObject[] _gameObjects;
    List<Vector3> _points;
    
    private void Start()
    {
        _points = RandomPoints(x, y, z, radius, delta, numberOfObjects, maxNumberOfTrys, xOffset, yOffset, zOffset);
        if (_points == null) return;
        _gameObjects = objects.ToArray();
        foreach (var point in _points)
        {
            Instantiate(_gameObjects[Random.Range(0, objects.Count)], point, Random.rotation);
        }
    }

    public List<Vector3> RandomPoints(int width, int height, int depth, int radius, int delta, int numPoints, int numMaxTry, int xOff, int yOff, int zOff)
    {
        if (numMaxTry < 10 || numMaxTry > 1000) return null;
        
        List<Vector3> points = new List<Vector3>();
        
        for (int i = 0; i < numPoints; i++)
        {
            bool trying = true;
            int counter = 0;
            int xPos = 0;
            int yPos = 0;
            int zPos = 0;
            while (trying && counter <= numMaxTry)
            {
                
                xPos = Random.Range(0, width) + xOff;
                yPos = Random.Range(0, height) + yOff;
                zPos = Random.Range(0, depth) + zOff;

                for (int j = 0; j < points.Count; j++)
                {
                    Vector3 point = points[j];
                    if (point.x + 2*radius + delta < xPos ||
                        point.y + 2*radius + delta < yPos ||
                        point.z + 2*radius + delta < zPos)
                    {
                        trying = false;
                    }
                }
                counter++;
            }
            points.Add(new Vector3(xPos, yPos, zPos));
        }

        return points;
    }
}
