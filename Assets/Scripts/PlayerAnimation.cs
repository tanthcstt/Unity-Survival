using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAnimation : MonoBehaviour
{
    public Rig aimingRig;
    protected Animator animator;
    public static PlayerAnimation instance;
    protected FPP_Interaction playerInteraction;
    
    private void Awake()
    {
        playerInteraction = gameObject.GetComponent<FPP_Interaction>();
        aimingRig.weight = 0;
        animator = GetComponent<Animator>();   
    }
    private void Update()
    {        
        if (Input.GetMouseButtonDown(1) && playerInteraction.selectedItemID == 9) AimingToggle(true);  
        if (playerInteraction.selectedItemID == 9 && Input.GetMouseButtonUp(1)) AimingToggle(false);
    }

    public void LowAimingToggle(bool state)
    {
        if (state)
        {
            aimingRig.weight = 1;
        }
        else aimingRig.weight = 0;
        animator.SetBool("aimingLow", state);
    }

    public void AimingToggle(bool state)
    {
        animator.SetBool("isAiming", state);
    }
}
