using System;
using System.Collections;
using System.Collections.Generic;
using Collect;
using UnityEngine;

public enum WeaponType
{
    Canon,
    MiniGun,
    Acid,
    AutoTarget,
    Radiation,
}

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 0)]
public class SOWeapon : ScriptableObject
{
    [Serializable]
    public class BuildComponents
    {
        [SerializeField]
        private LootType loot;

        [SerializeField]
        private int amount;

        public LootType Loot
        {
            get => loot;
            set => loot = value; // Ahora podemos asignar un valor desde fuera
        }
        public int Amount => amount;
    }

    [Header("Weapon Attributes")]
    public WeaponType weaponType;
    public GameObject weaponPrefab;
    public Sprite[] weaponSpritePerLevel;
    public int weaponLevel;

    [Header("Bullet Attributes")]
    public GameObject bulletPrefab;
    public float fireRate;
    public float bulletDamage;
    public float bulletSpeed;
    public float bulletLifeTime;

    [Header("Costs")]
    public BuildComponents[] levelOneCost = new BuildComponents[3]; // Ahora es simplemente un BuildComponents, no una tupla.
    public BuildComponents[] levelTwoCost = new BuildComponents[3]; // Ahora es simplemente un BuildComponents, no una tupla.
    public BuildComponents[] levelThreeCost = new BuildComponents[3]; // Ahora es simplemente un BuildComponents, no una tupla.

    private void OnValidate()
    {
        levelOneCost[0].Loot = LootType.Silver;
        levelOneCost[1].Loot = LootType.Zafiro;
        levelOneCost[2].Loot = LootType.Opalo;

        levelTwoCost[0].Loot = LootType.Silver;
        levelTwoCost[1].Loot = LootType.Zafiro;
        levelTwoCost[2].Loot = LootType.Opalo;

        levelThreeCost[0].Loot = LootType.Silver;
        levelThreeCost[1].Loot = LootType.Zafiro;
        levelThreeCost[2].Loot = LootType.Opalo;
    }
}
