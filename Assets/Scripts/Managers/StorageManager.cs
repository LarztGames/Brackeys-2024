using System.Collections;
using System.Collections.Generic;
using Collect;
using TMPro;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    public static StorageManager instance { get; set; }

    [SerializeField]
    private TMP_Text silverText;
    private float _silverAmount;

    [SerializeField]
    private TMP_Text zafiroText;
    private float _zafiroAmount;

    [SerializeField]
    private TMP_Text opaloText;
    private float _opaloAmount;

    [SerializeField]
    private bool debug;

    void Awake()
    {
        instance = (instance != null) ? instance : this;
    }

    void Start()
    {
        _silverAmount = 0;
        _zafiroAmount = 0;
        _opaloAmount = 0;
        UpdateText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && debug)
        {
            _silverAmount += 10;
            _zafiroAmount += 10;
            _opaloAmount += 10;
            UpdateText();
        }
    }

    public void AddLoot(List<SOCollectableResource> resources)
    {
        Debug.Log("adding loot");
        foreach (var resource in resources)
        {
            switch (resource.lootType)
            {
                case LootType.Silver:
                    _silverAmount += resource.amount;
                    break;
                case LootType.Zafiro:
                    _zafiroAmount += resource.amount;
                    break;
                case LootType.Opalo:
                    _opaloAmount += resource.amount;
                    break;
            }
        }
        UpdateText();
    }

    public void RemoveLoot(LootType lootType, float amount)
    {
        switch (lootType)
        {
            case LootType.Silver:
                _silverAmount -= amount;
                break;
            case LootType.Zafiro:
                _zafiroAmount -= amount;
                break;
            case LootType.Opalo:
                _opaloAmount -= amount;
                break;
        }
        UpdateText();
    }

    private void UpdateText()
    {
        silverText.text = $"{_silverAmount}";
        zafiroText.text = $"{_zafiroAmount}";
        opaloText.text = $"{_opaloAmount}";
    }

    public float GetLootByType(LootType lootType)
    {
        switch (lootType)
        {
            case LootType.Silver:
                return _silverAmount;
            case LootType.Zafiro:
                return _zafiroAmount;
            case LootType.Opalo:
                return _opaloAmount;
        }
        return -1;
    }
}
