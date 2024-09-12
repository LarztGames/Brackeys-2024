using System.Collections;
using System.Collections.Generic;
using Collect;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance { get; set; }

    [SerializeField]
    private GameObject shopObject;

    void Awake()
    {
        instance = (instance != null) ? instance : this;
    }

    void Start()
    {
        CloseShop();
    }

    public void OpenShop()
    {
        shopObject.SetActive(true);
    }

    public void CloseShop()
    {
        shopObject.SetActive(false);
    }

    public void BuyWeapon(SOWeapon weapon)
    {
        bool canBuy = true;
        List<LootType> lootType;
        List<float> lootCost;
        canBuy = TryBuyWeapon(weapon, canBuy, out lootType, out lootCost);
        if (canBuy)
        {
            BuildController.instance.SetWeapon(weapon);
            BuildController.instance.SetCost(lootType, lootCost);
        }
    }

    private static bool TryBuyWeapon(
        SOWeapon weapon,
        bool canBuy,
        out List<LootType> lootType,
        out List<float> lootCost
    )
    {
        lootType = new List<LootType>();
        lootCost = new List<float>();
        foreach (var item in weapon.levelOneCost)
        {
            float storageAmount = StorageManager.instance.GetLootByType(item.Loot);
            if (storageAmount < item.Amount && canBuy)
            {
                canBuy = false;
            }
            else
            {
                lootType.Add(item.Loot);
                lootCost.Add(item.Amount);
            }
        }

        return canBuy;
    }

    public void DisableUpgradesCanvases()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Upgrades");
        if (obj != null)
        {
            obj.SetActive(false);
        }
    }
}
