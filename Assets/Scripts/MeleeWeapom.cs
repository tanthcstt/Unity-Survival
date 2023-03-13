using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapom : DamageSender
{
    public bool isEquiped;
    public float attackDistance;
    private void Update()
    {
        if (!isEquiped) return;
        if (Input.GetMouseButtonDown(0)) Attack();

    }

    public void Attack()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance))
        {
            base.Send(hit.collider.gameObject);
        }
    }
}
