using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class FlyEnemyEgg : MonoBehaviour
{
    [SerializeField]
    private GameObject vfxColision;

    [SerializeField]
    private SOEnemy data;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LabCol"))
        {
            Laboratory.instance.ReceiveDamage(data.damage);
            GameObject vfx = Instantiate(vfxColision, transform.position, Quaternion.identity);
            vfx.SetActive(true);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Floor"))
        {
            GameObject vfx = Instantiate(vfxColision, transform.position, Quaternion.identity);
            vfx.SetActive(true);
            Destroy(gameObject);
        }
    }
}
