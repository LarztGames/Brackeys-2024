using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

public class Canon : MonoBehaviour
{
    private RoundManager RoundManager;

    [SerializeField]
    private SOWeapon weaponData;

    [SerializeField]
    private Transform bulletSpawnPoint;

    private float _fireRateTime;

    void Start()
    {
        RoundManager = RoundManager.instance;
    }

    void Update()
    {
        _fireRateTime += Time.deltaTime;
        if (RoundManager.GetRoundState() != RoundState.Calm && _fireRateTime > weaponData.fireRate)
        {
            _fireRateTime = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bulletInstance = Instantiate(
            weaponData.bulletPrefab,
            bulletSpawnPoint.position,
            Quaternion.identity
        );
        int direction = (bulletInstance.transform.position.x > 0) ? 1 : -1;
        Bullet bullet = bulletInstance.GetComponent<Bullet>();
        bullet.SetProperties(
            weaponData.bulletDamage,
            weaponData.bulletSpeed,
            weaponData.bulletLifeTime,
            direction
        );
    }
}
