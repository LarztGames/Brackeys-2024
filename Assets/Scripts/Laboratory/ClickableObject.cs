using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickableObject
    : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerClickHandler
{
    public Color normalColor = Color.white;
    public Color hoverColor = Color.gray;
    public Color clickColor = Color.green;

    private SpriteRenderer spriteRenderer;

    // "OnClick" de los botones, se puede asignar desde el Inspector
    public UnityEvent onClick;
    public AudioClip clickAudio;

    private void Start()
    {
        // Obtener el SpriteRenderer para cambiar el color del sprite
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = normalColor;
        }
    }

    // Cuando el mouse pasa sobre el objeto
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = hoverColor;
        }
    }

    // Cuando el mouse sale del objeto
    public void OnPointerExit(PointerEventData eventData)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = normalColor;
        }
    }

    // Cuando se hace clic en el objeto
    public void OnPointerClick(PointerEventData eventData)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = clickColor;
        }

        // Invocar el evento OnClick
        if (onClick != null)
        {
            SFXManager.instance.PlaySoundFXClip(clickAudio, transform);
            onClick.Invoke();
        }
    }
}
