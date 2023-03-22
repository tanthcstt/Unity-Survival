using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : DamageReceiver
{
    [Header("player parameters")]
    public GameObject deathCam;

    [Header("cold")]
    public float coldTime;
    public float maxColdTime = 5f;    
    public float coldDamage = 1f;

    [Header("hungry")]
    public float hungryTime;
    public float maxHungryTime = 5f;
    public float hungryDamage = 1f;
    public bool isHungry;


    [Header("thirsty")]
    public float thirstyTime;
    public float maxThirstyTime = 5f;
    public float thirstyDamage = 1f;
    public bool isThirsty;


    [Header("UI")]
    public GameObject UIHealth;
    public GameObject UIFood;
    public GameObject UIWater;
    private Slider healthSlider;
    private Slider foodSlider;
    private Slider waterSlider;

    public override void Awake()
    {
     
        base.maxHealth = 100;
        base.Awake();
        this.LoadComponent();
      
       
    }
    private void LoadComponent()
    {
        healthSlider = UIHealth.GetComponent<Slider>();
        foodSlider = UIFood.GetComponent<Slider>();
        waterSlider = UIWater.GetComponent<Slider>();

        healthSlider.maxValue = base.maxHealth;
        foodSlider.maxValue = maxHungryTime;
        waterSlider.maxValue = maxThirstyTime;

        healthSlider.value = maxHealth;
        foodSlider.value = maxHungryTime;
        waterSlider.value = maxThirstyTime;

        isHungry = false;
        isThirsty = false;

        deathCam.SetActive(false);
    }



    private void UpdateUI()
    {
   
        healthSlider.value = base.health;
        if (!isHungry)
        {
            foodSlider.value = maxHungryTime - hungryTime;
        }

        if (!isThirsty)
        {
            waterSlider.value = maxThirstyTime - thirstyTime;
        }
    }

    // minus health when cold || hungry || thirsty || something else
    private void FixedUpdate()
    {
     
        this.DoDamageByRuntime(ref coldTime, ref maxColdTime,  ref coldDamage);
        this.DoDamageByRuntime(ref hungryTime, ref maxHungryTime,  ref hungryDamage);
        this.DoDamageByRuntime(ref thirstyTime, ref maxThirstyTime,  ref thirstyDamage);
        this.UpdateUI();

    }

 

    private void DoDamageByRuntime(ref float time, ref float maxTime, ref float damage)
    {
         
        time += Time.deltaTime; 
        if (time  >= maxTime)
        {
            this.SetPlayerState();   
            this.Receive((int)damage);
            maxTime -= 2f; // make player take damage faster
            time = 0;            
        }
    }
    
    private void SetPlayerState()
    {
        if (hungryTime >= maxHungryTime) isHungry = true;
        if (thirstyTime >= maxThirstyTime) isThirsty = true;
    }
    
   public void IncreasePlayerParameters(ref float parameterToIncrease, float amount, float maxAmountOfParameter)
   {
        float temp = parameterToIncrease;
        if (temp + amount > maxAmountOfParameter)
        {
            parameterToIncrease = maxAmountOfParameter;
        } else
        {
            parameterToIncrease -= amount; // amount = time => decrease amount equal with increase delay time to get damage
        }
        
        SetStateAfterIncrease(ref parameterToIncrease);
        UpdateUI(); 

   }
   public void SetStateAfterIncrease(ref float parameterToIncrease)
   {
        if (parameterToIncrease == thirstyTime) isThirsty = false;
        if (parameterToIncrease == hungryTime) isHungry = false;
   }
    public override void Receive(int damage)
    {
        base.Hurt(damage);
        Debug.Log("- " + damage + " " + gameObject.name);
        if (IsDeath(base.health))
        {
            UpdateUI(); //make  the hp bar dont have hp when player die
            // player death
            deathCam.SetActive(true);
            deathCam.transform.parent = null;   
            gameObject.SetActive(false);
           
        }
    }

}
