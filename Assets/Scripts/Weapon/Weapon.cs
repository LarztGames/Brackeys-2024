using Managers;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected bool _placed;

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

    public void Placed()
    {
        GetComponent<Collider2D>().isTrigger = false;
        _spriteRenderer.color = normalColor;
        _placed = true;
    }

    void Update()
    {
        if (_placed)
        {
            Shooting();
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
            _spriteRenderer.color = GetComponent<UpgradeManager>().normalColor;
            GetComponent<UpgradeManager>().enabled = false;
        }
    }

    public int GetLevel() => _level;

    protected void Shooting()
    {
        _fireRateTime += Time.deltaTime;
        if (RoundManager.instance.GetRoundState() != RoundState.Calm && _fireRateTime > _fireRate)
        {
            _fireRateTime = 0;
            Shoot();
        }
    }

    public abstract void UpdateLevel(int level);
    protected abstract void Shoot();
}
