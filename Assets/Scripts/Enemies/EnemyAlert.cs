using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAlert : MonoBehaviour
{
    private Camera mainCamera; // Cámara principal

    [SerializeField]
    private GameObject alertIconPrefab; // Prefab del icono de alerta

    [SerializeField]
    private Canvas canvas; // Canvas donde se mostrarán los iconos

    private GameObject alertIcon;
    private RectTransform iconRectTransform; // RectTransform del icono para ajustar su tamaño correctamente
    private bool _isEnemyOnScreen;

    private void Start()
    {
        // Crear el icono de alerta y asociarlo al Canvas
        alertIcon = Instantiate(alertIconPrefab, canvas.transform);
        iconRectTransform = alertIcon.GetComponent<RectTransform>();
        alertIcon.SetActive(false); // Se oculta inicialmente
        mainCamera = Camera.main;
        // Debug.Log(alertIcon);
    }

    void Update()
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);

        // Comprobar si el enemigo está fuera de la pantalla
        if (
            screenPosition.z > 0
            && (
                screenPosition.x < 0
                || screenPosition.x > Screen.width
                || screenPosition.y < 0
                || screenPosition.y > Screen.height
            )
        )
        {
            alertIcon.SetActive(true); // Mostrar el icono si el enemigo está fuera de la pantalla
            _isEnemyOnScreen = false;

            // Clampear la posición del icono dentro de los bordes de la pantalla con un margen para que no se salga
            screenPosition.x = Mathf.Clamp(
                screenPosition.x,
                iconRectTransform.sizeDelta.x / 2,
                Screen.width - iconRectTransform.sizeDelta.x / 2
            );
            screenPosition.y = Mathf.Clamp(
                screenPosition.y,
                iconRectTransform.sizeDelta.y / 2,
                Screen.height - iconRectTransform.sizeDelta.y / 2
            );

            // Actualizar la posición del icono en el Canvas
            alertIcon.transform.position = screenPosition;
        }
        else
        {
            alertIcon.SetActive(false); // Ocultar el icono si el enemigo está en la pantalla
            _isEnemyOnScreen = true;
        }
    }
}
