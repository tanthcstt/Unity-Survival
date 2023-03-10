using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpaceShipInteractions : MonoBehaviour
{

    public GameObject spaceShipCamera;
    public GameObject player;
    public GameObject spaceShip;

    public Transform ship;
    public Transform shipModel;
    public Transform getOffPosition;
    public float MouseSensitive = 100f;
    public float horizontal;
    public float targetAngle;
    public float vertical;
    private float xAxis;
    private float yAxis;
  
    public Vector3 direction;

    public bool isOnSpaceShip;
    public bool isBroken;

    private KeyCode takeOff = KeyCode.Space;
    private KeyCode landing = KeyCode.LeftShift;

    public CharacterController shipController;
    public void Start()
    {
        spaceShipCamera.SetActive(false);
        isBroken = true;
    }
    public void Update()
    {
        if (isOnSpaceShip)
        {
            SpaceShipMovements();
        }
    }
    public void GetIn()
    {
        spaceShipCamera.SetActive(true );
        player.SetActive(false);
        isOnSpaceShip = true;
       
    }

    public void GetOff()
    {
        spaceShipCamera.SetActive(false);
        player.transform.position = getOffPosition.position;
        player.SetActive(true);
        isOnSpaceShip = false;
    }

    public void SpaceShipMovements()
    {
      
        // rotate around y Axis
        xAxis = Input.GetAxis("Mouse X") * MouseSensitive * Time.deltaTime;
        yAxis = 0f;
        if (Input.GetKey(takeOff))
        {
            yAxis = 0.5f;          
        } else if (Input.GetKey(landing))
        {
            yAxis = -0.5f;
        }
        ship.Rotate(Vector3.up * xAxis);

        //ship
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");      
        direction = transform.right * horizontal + transform.forward * vertical + transform.up * yAxis;        
        shipController.Move(50 * Time.deltaTime * direction);

        //get off
        if (Input.GetKeyDown(KeyCode.F) && isOnSpaceShip)
        {
            GetOff();   
        }

       
    }

   
}
