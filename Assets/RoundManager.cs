using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private Slider timerSlider;
    private bool _inRound;

    [Header("Calm Time")]
    [SerializeField]
    private float calmTime;
    private float _currentCalmTimer;

    [Header("Storm Time")]
    [SerializeField]
    private float stormTime;
    private float _currentStormTimer;

    void Start()
    {
        _currentCalmTimer = calmTime;

        timerSlider.maxValue = calmTime;
        timerSlider.value = calmTime;
        UpdateCalmTime();
    }

    void Update()
    {
        if (!_inRound)
        {
            UpdateCalmTime();
        }
        else
        {
            UpdateStormTime();
        }
    }

    #region Calm
    private void UpdateCalmTime()
    {
        if (_currentCalmTimer > 0)
        {
            _currentCalmTimer -= Time.deltaTime;
            timerSlider.value = Mathf.Lerp(
                timerSlider.value,
                _currentCalmTimer,
                5 * Time.deltaTime
            );
        }
        else
        {
            timerSlider.value = 0;
            StartRound();
        }
    }
    #endregion

    #region Storm
    private void UpdateStormTime()
    {
        if (_currentStormTimer > 0)
        {
            _currentStormTimer -= Time.deltaTime;
        }
        else
        {
            timerSlider.value = 0;
            EndRound();
        }
    }

    #endregion


    private void StartRound()
    {
        _inRound = true;
        Debug.Log("Starting Round");
    }

    private void EndRound()
    {
        _inRound = false;
        Debug.Log("End Round");
        _currentCalmTimer = calmTime;
    }
}
