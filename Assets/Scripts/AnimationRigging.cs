/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationRigging : MonoBehaviour
{
    public static AnimationRigging Instance;

    [Header("Rig")]
    private Rig rig;
    public Rig holdTools;
    private bool isActive;
    private float animationTime;
    private GameObject hand;
 
    private void Awake()
    {
        Instance = this;
        hand = GameObject.Find("FPP_Player/PlayerBody/metarig/spine/spine.001/spine.002/spine.003/shoulder.R/upper_arm.R/forearm.R/hand.R/Hand");

    }
    private void Update()
    {
        
        if (rig == null) return;

        if (isActive)
        {
            if (rig.weight < 1) rig.weight += (Time.deltaTime/animationTime);
        } else
        {
            if (rig.weight > 0) rig.weight -= (Time.deltaTime/animationTime);   
        }
    }
    public void AnimationToggle(bool state, Rig rig, float animationTime)
    {
        isActive = state;   
        this.rig = rig;
        this.animationTime = animationTime;
       //test
    }


}
*/