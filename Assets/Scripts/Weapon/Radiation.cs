using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Radiation : Weapon
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
        weaponData.weaponLevel = 2;
    }

    protected override void Shoot()
    {
        GameObject bulletInstance = Instantiate(
            weaponData.bulletPrefab,
            bulletSpawnPoint.position,
            Quaternion.identity
        );
        Vector3 bulletScale = bulletInstance.transform.localScale;
        bulletInstance.transform.DOPunchScale(bulletScale * 0.25f, 1);
        RadiationBullet bullet = bulletInstance.GetComponent<RadiationBullet>();
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
