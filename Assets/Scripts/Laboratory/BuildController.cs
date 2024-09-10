using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    private GameObject _currentWeapon;

    [SerializeField]
    private GameObject pointsHolder;

    public void SetWeapon(SOWeapon weapon)
    {
        pointsHolder.SetActive(true);
        _currentWeapon = weapon.weaponPrefab;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
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

    private void PlaceWeapon(RaycastHit2D hit)
    {
        GameObject weapon = Instantiate(
            _currentWeapon,
            hit.collider.gameObject.transform.position,
            hit.collider.gameObject.transform.rotation
        );
        weapon.transform.DOPunchScale(Vector2.one / .95f, 0.1f);
        hit.collider.gameObject.SetActive(false);
        pointsHolder.SetActive(false);
        _currentWeapon = null;
    }
}
