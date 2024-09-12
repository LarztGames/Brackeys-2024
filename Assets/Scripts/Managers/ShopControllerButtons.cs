using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopControllerButtons : MonoBehaviour
{
    [SerializeField]
    private SOWeapon weapon;

    public void CheckDisableButton()
    {
        if (!TryBuyWeapon(weapon))
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
    }

    private bool TryBuyWeapon(SOWeapon weapon)
    {
        foreach (var item in weapon.levelOneCost)
        {
            float storageAmount = StorageManager.instance.GetLootByType(item.Loot);
            if (storageAmount < item.Amount)
            {
                return false;
            }
        }
        return true;
    }
}
