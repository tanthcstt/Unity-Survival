using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterFly : MonoBehaviour
{
    public GameObject player;
    public GameObject helicopterCam;
    private void Awake()
    {
        helicopterCam.SetActive(false);
    }
    public void Fly()
    {
        player.SetActive(false);
        helicopterCam.SetActive(true);
    }
}
