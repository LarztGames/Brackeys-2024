using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class AcidBullet : MonoBehaviour
{
    private float _bulletDamage;
    private float _bulletSpeed;
    private float _bulletLifeTime;
    private float _bulletLifeTimer;

    [SerializeField]
    private Transform target; // Objetivo al que disparar el proyectil

    [SerializeField]
    private float launchAngle = 45f; // Ángulo de lanzamiento en grados

    [SerializeField]
    private float gravity = -9.8f; // Gravedad aplicada al proyectil

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position; // Posición inicial del proyectil
        LaunchProjectile();
    }

    void Reset()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    void Update()
    {
        _bulletLifeTimer += Time.deltaTime;
        if (_bulletLifeTimer > _bulletLifeTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetProperties(
        float bulletDamage,
        float bulletSpeed,
        float bulletLifeTime,
        Transform bulletTargetPoint
    )
    {
        _bulletDamage = bulletDamage;
        _bulletSpeed = bulletSpeed;
        _bulletLifeTime = bulletLifeTime;
        target = bulletTargetPoint;
    }

    private void LaunchProjectile()
    {
        // Calcular la distancia horizontal hacia el objetivo
        float targetDistance = Vector3.Distance(startPos, target.position);

        // Calcular la altura relativa
        float heightDifference = target.position.y - startPos.y;

        // Convertir ángulo a radianes
        float angleRad = launchAngle * Mathf.Deg2Rad;

        // Calcular la velocidad inicial necesaria para alcanzar el objetivo
        float initialVelocity = CalculateInitialVelocity(
            targetDistance,
            heightDifference,
            angleRad
        );

        // Calcular el vector de dirección inicial
        Vector3 direction = (target.position - startPos).normalized;
        direction.y = 0; // Queremos que el proyectil siga una parábola solo en el eje Y

        // Calcular la componente de la velocidad horizontal
        float velocityX = Mathf.Cos(angleRad) * initialVelocity;
        float velocityY = Mathf.Sin(angleRad) * initialVelocity;

        // Aplicar la velocidad al Rigidbody o al transform (si no usamos físicas)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(direction.x * velocityX, velocityY);
            rb.gravityScale = gravity / Physics2D.gravity.y; // Ajustar la gravedad
        }
    }

    private float CalculateInitialVelocity(float distance, float heightDifference, float angleRad)
    {
        // Ecuación de la velocidad inicial usando la fórmula de lanzamiento de proyectiles
        float velocity = Mathf.Sqrt(
            (gravity * distance * distance)
                / (2 * (heightDifference - Mathf.Tan(angleRad) * distance))
        );
        return velocity;
    }

    private void OnDrawGizmos()
    {
        // Dibuja la trayectoria estimada para visualizarla en la escena
        Gizmos.color = Color.green;

        Vector3 previousPos = transform.position;
        float simulationStep = 0.1f;
        Vector3 velocity = CalculateVelocityForGizmo();

        for (float t = 0; t < 10f; t += simulationStep)
        {
            Vector3 newPos = previousPos + velocity * simulationStep;
            velocity.y += gravity * simulationStep;
            Gizmos.DrawLine(previousPos, newPos);
            previousPos = newPos;
        }
    }

    private Vector3 CalculateVelocityForGizmo()
    {
        float targetDistance = Vector3.Distance(startPos, target.position);
        float angleRad = launchAngle * Mathf.Deg2Rad;
        float initialVelocity = CalculateInitialVelocity(
            targetDistance,
            target.position.y - startPos.y,
            angleRad
        );
        Vector3 direction = (target.position - startPos).normalized;
        float velocityX = Mathf.Cos(angleRad) * initialVelocity;
        float velocityY = Mathf.Sin(angleRad) * initialVelocity;
        return new Vector3(direction.x * velocityX, velocityY, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BaseEnemy>())
        {
            // TODO: Animation
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }

    public float GetDoDamage() => _bulletDamage;
}
