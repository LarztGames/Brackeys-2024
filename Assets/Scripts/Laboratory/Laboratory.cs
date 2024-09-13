using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laboratory : MonoBehaviour
{
    public static Laboratory instance { get; set; }

    [SerializeField]
    private Image healthImage; // Reemplazamos el Slider por una Image

    [SerializeField]
    private float graceTimer;
    private float _graceTime;

    [SerializeField]
    private float health;
    private float _currentHealth;

    void Awake()
    {
        instance = (instance != null) ? instance : this;
    }

    void Start()
    {
        // Al iniciar, el fillAmount debe estar lleno (1) ya que la salud está completa
        healthImage.fillAmount = 1f;
        _currentHealth = health;
        _graceTime = graceTimer;
    }

    void Update()
    {
        _graceTime += Time.deltaTime;
        if (_currentHealth <= 0)
        {
            GameManager.instance.OnLose("LoseScene");
        }
    }

    public void ReceiveDamage(float damage)
    {
        if (_graceTime > graceTimer)
        {
            _graceTime = 0;
            _currentHealth -= damage;

            // Normalizar el valor de salud para que fillAmount esté entre 0 y 1
            healthImage.fillAmount = Mathf.Lerp(
                healthImage.fillAmount,
                _currentHealth / health,
                5 * Time.deltaTime
            );
        }
    }

    public float LabHealth() => _currentHealth;
}
