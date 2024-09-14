using UnityEngine;

public class WeaponRange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GetComponentInParent<Weapon>().SetEnemyOnRange(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GetComponentInParent<Weapon>().SetEnemyOnRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GetComponentInParent<Weapon>().SetEnemyOnRange(false);
        }
    }
}
