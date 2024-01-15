using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipBehavior : ProjectileWeaponBehaviour
{

    protected virtual void Start()
    {
        base.Start();
    }

    void Update()
    {
        transform.position += direction * Time.deltaTime; 
        
    }
}
