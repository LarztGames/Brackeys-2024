using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : Weapon
{
    [SerializeField]
    private Transform bulletSpawnPoint;

    [SerializeField]
    private Transform[] bulletSpawnPointDouble;

    private int _index;

    void Start()
    {
        _fireRate = weaponData.fireRate;
        _bulletDamage = weaponData.bulletDamage;

        _index = 0;
        _placed = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = unPlacedColor;
        weaponData.weaponLevel = 0;
    }

    protected override void Shoot()
    {
        GameObject bulletInstance;
        if (_level != 0)
        {
            bulletInstance = Instantiate(
                weaponData.bulletPrefab,
                bulletSpawnPointDouble[_index].position,
                Quaternion.identity
            );
            _index = (_index == 1) ? 0 : 1;
        }
        else
        {
            bulletInstance = Instantiate(
                weaponData.bulletPrefab,
                bulletSpawnPoint.position,
                Quaternion.identity
            );
        }
        int direction = (bulletInstance.transform.position.x > 0) ? 1 : -1;
        Bullet bullet = bulletInstance.GetComponent<Bullet>();
        bullet.SetProperties(
            weaponData.bulletDamage,
            weaponData.bulletSpeed,
            weaponData.bulletLifeTime,
            direction
        );
    }

    public override void UpdateLevel(int level)
    {
        _level = level;
        if (level == 2)
        {
            _bulletDamage *= weaponData.levelTwoMultiplier;
            _fireRate /= weaponData.levelTwoMultiplier;
            _spriteRenderer.sprite = weaponData.weaponSpriteLevelTwo;
        }
        else if (level == 3)
        {
            _bulletDamage *= weaponData.levelThreeMultiplier;
            _fireRate /= weaponData.levelThreeMultiplier;
            _spriteRenderer.sprite = weaponData.weaponSpriteLevelThree;
        }
    }
}
