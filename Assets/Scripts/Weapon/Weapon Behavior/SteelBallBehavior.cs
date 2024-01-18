using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelBallBehavior : ProjectileWeaponBehaviour
{
    private Transform targetEnemy; // Tham chiếu đến kẻ địch gần nhất

    protected override void Start()
    {
        base.Start();

        // Tìm kiếm và lưu trữ tham chiếu đến kẻ địch gần nhất
        FindNearestEnemy();
    }

    void Update()
    {
        if (targetEnemy != null)
        {
            // Tính toán hướng mới từ vị trí hiện tại đến vị trí của kẻ địch
            Vector3 newDirection = (targetEnemy.position - transform.position).normalized;

            // Di chuyển FireBall theo hướng mới tính toán được
            transform.position += newDirection * currentSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += direction * currentSpeed * Time.deltaTime;
        }
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            // Tìm ra kẻ địch gần nhất
            Transform nearestEnemy = enemies[0].transform;
            float shortestDistance = Vector3.Distance(transform.position, nearestEnemy.position);

            for (int i = 1; i < enemies.Length; i++)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemies[i].transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemies[i].transform;
                }
            }

            // Lưu trữ tham chiếu đến kẻ địch gần nhất
            targetEnemy = nearestEnemy;
        }
    }
}
