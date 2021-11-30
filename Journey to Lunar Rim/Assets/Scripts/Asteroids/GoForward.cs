using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForward : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, 1) * 5 * Time.fixedDeltaTime);
    }
}
