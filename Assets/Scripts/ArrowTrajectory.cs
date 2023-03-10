using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class ArrowTrajectory : MonoBehaviour
{
    public Transform arrowSpawn;
    public GameObject arrow;
    public Camera cam;
    public static ArrowTrajectory Instance;
    public bool isLaunched;
    public LayerMask envLayer;

    private void Awake()
    {
        Instance = this;    
    }
    private void Update()
    {
        if (isLaunched)
        {
            SetDoDamage();
        }
    }
    public void ArrowFly(GameObject arrowPrefab)
    {
        arrow = Instantiate(arrowPrefab, arrowSpawn.position, arrowSpawn.rotation);
        arrow.GetComponent<GeneralItemData>().item.isDoDamage = true;
        Rigidbody arrow_rb = arrow.GetComponent<Rigidbody>();
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        arrow_rb.velocity = ray.direction * 50f;
        isLaunched = true;
    }

    public void SetDoDamage()
    {
        if (Physics.CheckBox(arrow.transform.position, new Vector3(.3f, .3f, .3f), arrow.transform.rotation, envLayer))
        {
            arrow.GetComponent<GeneralItemData>().item.isDoDamage = false;
            isLaunched = false;
        }
    }
}
