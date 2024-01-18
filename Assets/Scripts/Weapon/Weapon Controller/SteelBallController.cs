using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelBallController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }


    protected override void Attack()
    {
        base.Attack();
        GameObject spawnSteelBall = Instantiate(weaponData.Prefab);
        spawnSteelBall.transform.position = transform.position; // gan vi tri cung voi vat the co cha la player
        spawnSteelBall.GetComponent<SteelBallBehavior>().DirectionChecker(pm.lastMovedVector);
    }
}
