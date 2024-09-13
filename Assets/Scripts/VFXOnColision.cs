using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXOnColision : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
