using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laboratory : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private float graceTimer;
    private float _graceTime;

    [SerializeField]
    private float health;
    private float _currentHealth;

    void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        _currentHealth = health;
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
            healthSlider.value = Mathf.Lerp(healthSlider.value, _currentHealth, 5 * Time.deltaTime);
        }
    }

    public float LabHealth() => _currentHealth;
}
