using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationTracker : MonoBehaviour
{

    public GameObject player;

    [SerializeField]    
    public Text txtX;

    [SerializeField]
    public Text txtY;

    [SerializeField]
    public Text txtZ;

    // Update is called once per frame
    void Update()
    {
        txtX.text = player.transform.localEulerAngles.x.ToString();
        txtY.text = player.transform.localEulerAngles.y.ToString();
        txtZ.text = player.transform.localEulerAngles.z.ToString();
    }
}
