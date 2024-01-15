using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireBallBehavior : ProjectileWeaponBehaviour
{
    protected virtual void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * currentSpeed * Time.deltaTime; //chuyen dong cua dao
    }
}
