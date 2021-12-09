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

    [SerializeField]
    public Text lives;

    [SerializeField]
    public Text hitti;

    [SerializeField]
    public Text speed;

    [SerializeField]
    public Text tank;

    // Update is called once per frame
    void Update()
    {
        txtX.text = player.GetComponent<PlayerController>().playerCam.transform.localEulerAngles.x.ToString("F");
        txtY.text = player.GetComponent<PlayerController>().playerCam.transform.localEulerAngles.y.ToString("F");
        txtZ.text = player.GetComponent<PlayerController>().playerCam.transform.localEulerAngles.z.ToString("F");
        /*txtX.text = player.transform.localEulerAngles.x.ToString();
        txtY.text = player.transform.localEulerAngles.y.ToString();
        txtZ.text = player.transform.localEulerAngles.z.ToString();*/
        lives.text = player.GetComponent<PlayerController>().lives.ToString();
        hitti.text = player.GetComponent<PlayerController>().collisionState.ToString();
        speed.text = player.GetComponent<PlayerController>().shipSpeed.ToString();
        tank.text = player.GetComponent<PlayerController>().tank.ToString();
    }
}
