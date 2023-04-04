using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class DayNightCycle : MonoBehaviour
{
    public bool isDay;
    public Light sun;
    public float dayLong;
    public float startTime;
    public float runtime = 0f;
    private Vector3 rot;
    private float exposureRuntime;
    private float exposure = 0;
    private float angle = 0f;

    private void Start()
    {
      
        runtime = startTime / dayLong * 360f;
        rot = new Vector3(runtime, 0f, 0f);
        sun.transform.Rotate(rot);
        runtime = Time.fixedDeltaTime / dayLong * 360f;
        rot = new Vector3(runtime, 0f, 0f);
        exposure += Time.fixedDeltaTime * (1 / dayLong);
       
       
    }
    private void FixedUpdate()
    {
       // daylong(s) -> 360deg
       //0.2s --> ?deg
        angle += Time.fixedDeltaTime * 360f / dayLong ;
        sun.transform.Rotate(rot);
        
        exposure = Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad));

        if (angle % 360f > 180f)
        {
            isDay = false;
            exposure = 0.08f;
        } else isDay = true;        

        RenderSettings.skybox.SetFloat("_Exposure", exposure);
    }

}
