using System.Collections;
using System.Collections.Generic;
using Collect;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private GameObject shopObject;

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
        List<LootType> lootType = new List<LootType>();
        List<float> lootCost = new List<float>();
        bool canBuy = true;
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
        if (canBuy)
        {
            BuildController.instance.SetWeapon(weapon);
            BuildController.instance.SetCost(lootType, lootCost);
        }
        Debug.Log(canBuy);
    }
}
