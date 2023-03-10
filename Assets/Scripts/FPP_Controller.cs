using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FPP_Controller : MonoBehaviour
{
    public CharacterController controller;
    public Transform GroundCheck;
    public LayerMask enviroment;
    public Transform cam;
    public StaminaBar staminaBar;


    
    private float speed = 10f;
    private float walk_speed = 10f;
    private float sprint_speed = 50f;
    private float jumbHeight = 5f;
    private float gravityValue = 2 * 9.81f;
    private float horizontal;
    private float vertical;
    private float currentStamina;
    private Vector3 direction = Vector3.zero;
    private Vector3 playerVelocity = Vector3.zero;

    // keycode
    public KeyCode jumb = KeyCode.Space;
    public KeyCode sprint = KeyCode.LeftShift;
    public KeyCode skill_3 = KeyCode.Alpha3;
 
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {   
        
        //character Movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        direction = transform.right * horizontal + transform.forward * vertical;
        speed = walk_speed;
        currentStamina = staminaBar.GetCurrentStamina();
        if (IsGrounded())
        {
            if (Input.GetKeyDown(jumb))
            {
                playerVelocity.y += Mathf.Sqrt(2 * gravityValue * jumbHeight) + 9.81f;
            } else if (Input.GetKey(sprint) && currentStamina > 0)
            {
                speed = sprint_speed;
                
               
            } 
        }
        controller.Move(speed * Time.deltaTime * direction);

        // Apply gravity
        Gravity();

       

    }

    // check if player is grounded
    bool IsGrounded()
    {
              
        Vector3 groundedDirection = new Vector3(0f, -1f, 0f);
        if (Physics.Raycast(GroundCheck.position, groundedDirection, out _ , 0.5f))
        {
            return true;
        } else
        {
            return false;
        }
    }
   
    void Gravity()
    {
        if (IsGrounded() && playerVelocity.y < 0)
        {
            playerVelocity.y = -9.81f;
        }
        playerVelocity.y -= gravityValue  * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }



   
}