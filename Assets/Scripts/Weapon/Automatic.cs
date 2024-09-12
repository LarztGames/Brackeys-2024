using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automatic : Weapon
{
    [SerializeField]
    private Transform bulletSpawnPoint;

    void Start()
    {
        _fireRate = weaponData.fireRate;
        _bulletDamage = weaponData.bulletDamage;

        _placed = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = unPlacedColor;
        _fireRateTime = 0;
        weaponData.weaponLevel = 0;
        Shoot();
    }

    protected override void Shoot()
    {
        GameObject bulletInstance = Instantiate(
            weaponData.bulletPrefab,
            bulletSpawnPoint.position,
            Quaternion.identity
        );
        AutoBullet bullet = bulletInstance.GetComponent<AutoBullet>();
        bullet.SetProperties(
            weaponData.bulletDamage,
            weaponData.bulletSpeed,
            weaponData.bulletLifeTime
        );
    }

    public override void UpdateLevel(int level)
    {
        _level = level;
        if (level == 3)
        {
            _bulletDamage *= weaponData.levelThreeMultiplier;
            _fireRate /= weaponData.levelThreeMultiplier;
            _spriteRenderer.sprite = weaponData.weaponSpriteLevelThree;
        }
    }
}
