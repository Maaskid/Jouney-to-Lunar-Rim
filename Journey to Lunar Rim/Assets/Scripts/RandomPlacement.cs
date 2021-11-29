using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPlacement : MonoBehaviour
{
    public int x, y, z;
    public int xOffset, yOffset, zOffset;
    public int numberOfObjects;
    public int maxNumberOfTrys;
    public List<GameObject> objects;
     
    private List<GameObject> _gameObjects = new List<GameObject>();
    private List<Vector3> _points;
    
    private void Start()
    {
        
        for (int i = 0; i < numberOfObjects; i++)
        {
            _gameObjects[i] = objects[0];
            Debug.Log("WORKING");
        }
        _points = RandomPoints(x, y, z, numberOfObjects, maxNumberOfTrys, xOffset, yOffset, zOffset);
        if (_points == null) return;
        for (int i = 0; i < _points.Count; i++)
        {
            Instantiate(_gameObjects[i], _points[i], Random.rotation);
        }
    }

    public List<Vector3> RandomPoints(int width, int height, int depth, int numPoints, int numMaxTry, int xOff, int yOff, int zOff)
    {
        if (numMaxTry < 10 || numMaxTry > 1000) return null;
        
        List<Vector3> points = new List<Vector3>();
        
        for (int i = 0; i < numPoints; i++)
        {
            int xPos = Random.Range(0, width) + xOff;
            int yPos = Random.Range(0, height) + yOff;
            int zPos = Random.Range(0, depth) + zOff;
            if (i == 0)
            {
                Debug.Log("WTF");
                Vector3 colliderMax = _gameObjects[i - 1].GetComponent<Collider>().bounds.max;
                while (xPos + colliderMax.magnitude < colliderMax.x || xPos - colliderMax.magnitude > colliderMax.x&& zPos < colliderMax.z)
                {
                    xPos = Random.Range(0, width) + xOff;
                }

                while (yPos + colliderMax.magnitude < colliderMax.y || yPos + colliderMax.magnitude > colliderMax.y)
                {
                    yPos = Random.Range(0, height) + yOff;
                }
                
                while (zPos + colliderMax.magnitude < colliderMax.z || zPos + colliderMax.magnitude > colliderMax.z)
                {
                    zPos = Random.Range(0, depth) + zOff;
                }
            }
            points.Add(new Vector3(xPos, yPos, zPos));
        }
        return points;
    }
}
