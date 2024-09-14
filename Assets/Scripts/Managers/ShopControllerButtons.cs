using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class AuxiliarControllerClass
{
    public SOWeapon weapon;

    public Button button;
}

public class ShopControllerButtons : MonoBehaviour
{
    public static ShopControllerButtons instance { get; set; }

    [SerializeField]
    private AuxiliarControllerClass[] buttonWeapon;

    void Awake()
    {
        instance = (instance != null) ? instance : this;
    }

    public void CheckDisableButton()
    {
        foreach (var item in buttonWeapon)
        {
            if (!TryBuyWeapon(item.weapon))
            {
                item.button.GetComponent<Button>().interactable = false;
            }
            else
            {
                item.button.GetComponent<Button>().interactable = true;
            }
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
