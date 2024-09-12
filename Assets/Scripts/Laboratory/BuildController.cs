using System;
using System.Collections;
using System.Collections.Generic;
using Collect;
using DG.Tweening;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public static BuildController instance { get; set; }

    private GameObject _currentWeapon;

    [SerializeField]
    private GameObject sidePoints;

    [SerializeField]
    private GameObject topPoints;

    [SerializeField]
    private Vector3 _mousePosition;

    private List<GameObject> points = new List<GameObject>();

    private List<LootType> _actualLootType = new List<LootType>();
    private List<float> _actualLootCost = new List<float>();

    void Awake()
    {
        instance = (instance != null) ? instance : this;
    }

    public void SetWeapon(SOWeapon weapon)
    {
        if (_currentWeapon != null)
        {
            CancelPlace();
        }
        switch (weapon.weaponType)
        {
            case WeaponType.MiniGun:
            case WeaponType.Canon:
            case WeaponType.Acid:
                sidePoints.SetActive(true);
                break;

            case WeaponType.AutoTarget:
            case WeaponType.Radiation:
                topPoints.SetActive(true);
                break;
        }
        _currentWeapon = Instantiate(weapon.weaponPrefab, _mousePosition, Quaternion.identity);
        _currentWeapon.GetComponent<Collider2D>().isTrigger = true;
    }

    private void CancelPlace()
    {
        Destroy(_currentWeapon.gameObject);
        _currentWeapon = null;
        _actualLootType = null;
        _actualLootCost = null;
    }

    void Start()
    {
        points.Add(sidePoints);
        points.Add(topPoints);
    }

    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition.z = 0;
        if (_currentWeapon != null)
        {
            _currentWeapon.transform.position = _mousePosition;
        }
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }

        if (Input.GetMouseButtonDown(1))
        {
            CancelPlace();
        }
    }

    private void CastRay()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(
            worldPosition,
            Vector2.right,
            10f,
            LayerMask.GetMask("WeaponPlace")
        );
        if (hit.collider != null && _currentWeapon != null)
        {
            PlaceWeapon(hit);
        }
    }

    #region Place
    private void PlaceWeapon(RaycastHit2D hit)
    {
        _currentWeapon.transform.DOPunchScale(Vector2.one / .95f, 0.1f);
        hit.collider.gameObject.SetActive(false);
        foreach (GameObject item in points)
        {
            if (item.activeSelf)
            {
                item.SetActive(false);
            }
        }
        for (int i = 0; i < _actualLootType.Count; i++)
        {
            StorageManager.instance.RemoveLoot(_actualLootType[i], _actualLootCost[i]);
        }
        _currentWeapon.GetComponent<Weapon>().Placed();
        _currentWeapon.transform.position = hit.collider.transform.position;
        _currentWeapon = null;
    }

    public void SetCost(List<LootType> lootType, List<float> lootCost)
    {
        _actualLootType = lootType;
        _actualLootCost = lootCost;
    }
    #endregion
}
