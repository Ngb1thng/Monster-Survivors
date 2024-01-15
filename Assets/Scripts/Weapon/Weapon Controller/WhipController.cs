using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedWhip = Instantiate(weaponData.Prefab);

        float Xoffset = pm.lastMovedVector.x < 0 ? -1.5f : 1.5f;
        Vector3 playerPosition = transform.position + transform.right * Xoffset;
        spawnedWhip.transform.position = playerPosition;
        spawnedWhip.GetComponent<WhipBehavior>().DirectionChecker(pm.lastMovedVector);
    }
}
