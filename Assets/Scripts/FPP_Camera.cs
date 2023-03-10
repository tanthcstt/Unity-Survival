using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPP_Camera : MonoBehaviour
{
    public Transform cam;
    public float MouseSensitive = 100f;
    private float xAxis;
    private float yAxis;
    private float xRotation;

    // Start is called before the first frame update
    void Start()
    {
     /*   Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;*/
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }
    public void CameraRotation()
    {
        xAxis = Input.GetAxis("Mouse X") * MouseSensitive * Time.deltaTime;
        yAxis = Input.GetAxis("Mouse Y") * MouseSensitive * Time.deltaTime;
        xRotation -= yAxis;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // rotate around x Axis
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // rotate around y Axis
        cam.Rotate(Vector3.up * xAxis);
    }
}
