using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapom : MonoBehaviour
{
    public bool isEquiped;
    public float attackDistance;
    private Transform cam;
    private void Start()
    {
        cam = DamageSender.instance.cam;
    }
    private void Update()
    {
        if (!isEquiped) return;
        if (Input.GetMouseButtonDown(0)) Attack();

    }

    public void Attack()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance))
        {
            DamageSender.instance.DoDamage(gameObject.GetComponent<GeneralItemData>().item.itemDamage, hit.collider.gameObject);
        }
    }
}
