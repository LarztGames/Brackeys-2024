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
        private bool _canStartCalm;
        private float _currentCalmTimer;

        [SerializeField]
        private Button disableOnStorm;

        [Header("Storm Time")]
        [SerializeField]
        private float stormTime;
        private float _currentStormTimer;

        [Header("Transition Time")]
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

            stormIconAnimator.SetFloat("calm", _currentCalmTimer);
            state = RoundState.Calm;
            #endregion
        }

        void Update()
        {
            switch (state)
            {
                case RoundState.Calm:
                    disableOnStorm.enabled = true;
                    stormIconAnimator.SetFloat("calm", _currentCalmTimer);
                    if (_canStartCalm)
                    {
                        CalmState();
                    }
                    break;
                case RoundState.Storm:
                    _canStartCalm = false;
                    disableOnStorm.enabled = false;
                    StormState();
                    break;
                case RoundState.Transition:
                    disableOnStorm.enabled = false;
                    TransitionState();
                    break;
                default:
                    break;
            }
        }

        #region Calm
        public void CalmState()
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

        public void StartCalm() => _canStartCalm = true;

        public RoundState GetRoundState() => state;

        public bool RemainingEnemies() => (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0);
    }
}
