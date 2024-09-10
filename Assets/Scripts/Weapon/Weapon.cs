using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected bool _placed;

    [SerializeField]
    private Color normalColor;

    [SerializeField]
    private Color unPlacedColor;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = unPlacedColor;
    }

    public void Placed()
    {
        _spriteRenderer.color = normalColor;
        _placed = true;
    }
}
