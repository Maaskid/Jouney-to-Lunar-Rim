using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    public Transform lookTarget;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookTarget);
        transform.Rotate(new Vector3(0, -90, 0));
    }
}
