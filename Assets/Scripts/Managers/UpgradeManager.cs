using System;
using System.Collections;
using System.Collections.Generic;
using Collect;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UpgradeManager : MonoBehaviour, IPointerClickHandler
{
    private GameObject _upgradeObject;

    public UnityEvent onClick;

    private bool firstTime = true;

    private void Update()
    {
        if (_upgradeObject != null && firstTime)
        {
            CloseUpgrade();
            firstTime = false;
        }
    }

    public void OpenUpgrade(GameObject upgradeObject)
    {
        if (_upgradeObject != null)
        {
            CloseUpgrade();
        }
        _upgradeObject = upgradeObject;
        _upgradeObject.SetActive(true);
    }

    public void CloseUpgrade()
    {
        _upgradeObject.SetActive(false);
        _upgradeObject = null;
    }

    public void UpgradeToLevelTwo(SOWeapon weaponData)
    {
        Weapon weapon = this.gameObject.GetComponent<Weapon>();
        if (weapon.GetLevel() != 0)
        {
            return;
        }
        List<LootType> lootType = new List<LootType>();
        List<float> lootCost = new List<float>();
        bool canUpgrade = true;
        foreach (var item in weaponData.levelTwoCost)
        {
            float storageAmount = StorageManager.instance.GetLootByType(item.Loot);
            if (storageAmount < item.Amount && canUpgrade)
            {
                canUpgrade = false;
            }
            else
            {
                lootType.Add(item.Loot);
                lootCost.Add(item.Amount);
            }
        }
        if (canUpgrade)
        {
            Debug.Log("Update level to 2");
            UpdateStorage(lootType, lootCost);
            weapon.UpdateLevel(2);
        }
    }

    public void UpgradeToLevelThree(SOWeapon weaponData)
    {
        Weapon weapon = this.gameObject.GetComponent<Weapon>();
        if (weapon.GetLevel() == 3)
        {
            return;
        }
        List<LootType> lootType = new List<LootType>();
        List<float> lootCost = new List<float>();
        bool canUpgrade = true;
        foreach (var item in weaponData.levelThreeCost)
        {
            float storageAmount = StorageManager.instance.GetLootByType(item.Loot);
            if (storageAmount < item.Amount && canUpgrade)
            {
                canUpgrade = false;
            }
            else
            {
                lootType.Add(item.Loot);
                lootCost.Add(item.Amount);
            }
        }
        if (canUpgrade)
        {
            Debug.Log("Update level to 3");
            UpdateStorage(lootType, lootCost);
            weapon.UpdateLevel(3);
        }
        else
        {
            Debug.Log("No se pudo subir de nivel lootType: " + lootType);
            Debug.Log("No se pudo subir de nivel lootCost: " + lootCost);
        }
    }

    private static void UpdateStorage(List<LootType> lootType, List<float> lootCost)
    {
        for (int i = 0; i < lootType.Count; i++)
        {
            StorageManager.instance.RemoveLoot(lootType[i], lootCost[i]);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Invocar el evento OnClick
        if (onClick != null)
        {
            onClick.Invoke();
        }
    }
}
