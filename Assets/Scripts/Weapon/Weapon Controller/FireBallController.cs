using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnFireBall = Instantiate(weaponData.Prefab);
        spawnFireBall.transform.position = transform.position; // gan vi tri cung voi vat the co cha la player
        spawnFireBall.GetComponent<FireBallBehavior>().DirectionChecker(pm.lastMovedVector);
    }
}
