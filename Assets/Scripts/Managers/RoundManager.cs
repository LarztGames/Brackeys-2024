using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public enum RoundState
    {
        Calm,
        Storm,
        Transition
    }

    public class RoundManager : MonoBehaviour
    {
        public static RoundManager instance { get; set; }

        [SerializeField]
        private Slider timerSlider;
        private RoundState state;

        //FF2D2D
        [SerializeField]
        private Animator stormIconAnimator;

        [Header("Calm Time")]
        [SerializeField]
        private float calmTime;
        private float _currentCalmTimer;

        [Header("Storm Time")]
        [SerializeField]
        private float stormTime;
        private float _currentStormTimer;

        [Header("Storm Time")]
        [SerializeField]
        private float transitionTime;
        private float _currentTransitionTimer;

        void Awake()
        {
            instance = (instance != null) ? instance : this;
        }

        void Start()
        {
            #region Setup
            _currentCalmTimer = calmTime;
            _currentStormTimer = stormTime;
            _currentTransitionTimer = 0;

            timerSlider.maxValue = calmTime;
            timerSlider.value = calmTime;

            state = RoundState.Calm;
            #endregion
        }

        void Update()
        {
            switch (state)
            {
                case RoundState.Calm:
                    CalmState();
                    break;
                case RoundState.Storm:
                    StormState();
                    break;
                case RoundState.Transition:
                    TransitionState();
                    break;
            }
        }

        #region Calm
        private void CalmState()
        {
            stormIconAnimator.SetFloat("calm", _currentCalmTimer);
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
                // Transition to Storm
                state = RoundState.Storm;
                timerSlider.value = 0;
            }
        }
        #endregion

        #region Storm
        private void StormState()
        {
            WaveManager.instance.StartWave();
            stormIconAnimator.SetBool("storm", true);
            if (_currentStormTimer > 0)
            {
                _currentStormTimer -= Time.deltaTime;
            }
            else
            {
                state = RoundState.Transition;
                timerSlider.maxValue = transitionTime;
                timerSlider.value = 0;
            }
        }
        #endregion

        #region Transition
        private void TransitionState()
        {
            if (
                _currentTransitionTimer < transitionTime
                && GameObject.FindGameObjectsWithTag("Enemy").Length <= 0
            )
            {
                stormIconAnimator.SetBool("storm", false);
                stormIconAnimator.SetBool("transition", true);
                _currentTransitionTimer += Time.deltaTime;
                timerSlider.value = Mathf.Lerp(
                    timerSlider.value,
                    _currentTransitionTimer,
                    5 * Time.deltaTime
                );
            }
            else
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
                {
                    // TODO: Comprobar que todos los enemigos han muerto
                    WaveManager.instance.NextWave();
                    // Reset de los temporizadores
                    stormIconAnimator.SetBool("transition", false);
                    _currentCalmTimer = calmTime;
                    _currentStormTimer = stormTime;
                    _currentTransitionTimer = 0;
                    timerSlider.maxValue = calmTime;
                    timerSlider.value = calmTime;
                    state = RoundState.Calm;
                }
            }
        }
        #endregion

        public RoundState GetRoundState() => state;
    }
}
