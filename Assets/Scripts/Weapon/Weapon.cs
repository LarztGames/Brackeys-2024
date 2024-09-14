using System.Data.Common;
using Managers;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected bool _placed;
    protected GameObject _placeObj;

    [SerializeField]
    protected SOWeapon weaponData;
    protected float _fireRateTime;

    #region Upgrade values
    protected float _fireRate;
    protected float _bulletDamage;
    #endregion

    [SerializeField]
    protected Color normalColor;

    [SerializeField]
    protected Color unPlacedColor;
    protected SpriteRenderer _spriteRenderer;
    protected int _level;

    private bool _enemyOnRange;

    public void Placed(GameObject placeObj)
    {
        ShopControllerButtons.instance.CheckDisableButton();
        StorageManager.instance.UpdateText();
        GetComponent<Collider2D>().isTrigger = false;
        _placeObj = placeObj;
        _spriteRenderer.color = normalColor;
        _placed = true;
    }

    void Update()
    {
        if (_placed)
        {
            if (_enemyOnRange)
            {
                Shooting();
            }
        }
        else
        {
            if (transform.position.x > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        }
        if (_level >= 3)
        {
            // _spriteRenderer.color = GetComponent<UpgradeManager>().normalColor;
            // GetComponent<UpgradeManager>().enabled = false;
        }
    }

    public int GetLevel() => _level;

    protected void Shooting()
    {
        _fireRateTime += Time.deltaTime;
        if (
            !RoundManager.instance.RemainingEnemies()
            && _fireRateTime > _fireRate
            && GameManager.instance.OnLab()
            && _enemyOnRange
        )
        {
            SFXManager.instance.PlaySoundFXClip(weaponData.shoot, transform, 0.25f);
            _fireRateTime = 0;
            Shoot();
        }
    }

    public void Remove()
    {
        // TODO: Animation
        // TODO: Sound
        _placeObj.SetActive(true);
        switch (_level)
        {
            case 2:
                foreach (var item in weaponData.levelTwoCost)
                {
                    StorageManager.instance.AddLoot(item.Loot, item.Amount);
                }
                foreach (var item in weaponData.levelOneCost)
                {
                    StorageManager.instance.AddLoot(item.Loot, item.Amount);
                }
                break;
            case 3:
                foreach (var item in weaponData.levelThreeCost)
                {
                    StorageManager.instance.AddLoot(item.Loot, item.Amount);
                }
                foreach (var item in weaponData.levelOneCost)
                {
                    StorageManager.instance.AddLoot(item.Loot, item.Amount);
                }
                break;
            case 1:
            default:
                foreach (var item in weaponData.levelOneCost)
                {
                    StorageManager.instance.AddLoot(item.Loot, item.Amount);
                }
                break;
        }
        Destroy(gameObject);
    }

    public abstract void UpdateLevel(int level);
    protected abstract void Shoot();

    public void SetEnemyOnRange(bool value) => _enemyOnRange = value;
}
