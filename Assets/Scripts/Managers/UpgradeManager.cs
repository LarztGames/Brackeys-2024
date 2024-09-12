using System;
using System.Collections;
using System.Collections.Generic;
using Collect;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UpgradeManager
    : MonoBehaviour,
        IPointerClickHandler,
        IPointerEnterHandler,
        IPointerExitHandler
{
    private GameObject _upgradeObject;

    private SpriteRenderer _spriteRenderer;
    public Color normalColor = Color.white;
    public Color hoverColor = Color.gray;
    public UnityEvent onClick;

    private bool firstTime = true;

    private void Start()
    {
        // Obtener el SpriteRenderer para cambiar el color del sprite
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = normalColor;
        }
    }

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
        Debug.Log("Upgrade old obj: " + _upgradeObject);
        if (_upgradeObject != null)
        {
            CloseUpgrade();
        }
        Debug.Log("Upgrade new obj: " + upgradeObject);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = normalColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Invocar el evento OnClick
        if (onClick != null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Upgrades");
            if (obj != null)
            {
                obj.SetActive(false);
            }
            onClick.Invoke();
        }
    }
}
