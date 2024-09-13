using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : Weapon
{
    [SerializeField]
    private Transform bulletSpawnPoint;

    [SerializeField]
    private Transform bulletTargetPoint;

    private void Start()
    {
        _fireRate = weaponData.fireRate;
        _bulletDamage = weaponData.bulletDamage;

        _placed = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = unPlacedColor;
        _fireRateTime = 0;
        weaponData.weaponLevel = 0;
    }

    protected override void Shoot()
    {
        GameObject bulletInstance = Instantiate(
            weaponData.bulletPrefab,
            bulletSpawnPoint.position,
            Quaternion.identity
        );
        AcidBullet bullet = bulletInstance.GetComponent<AcidBullet>();
        bullet.SetProperties(
            weaponData.bulletDamage,
            weaponData.bulletSpeed,
            weaponData.bulletLifeTime,
            bulletTargetPoint
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
