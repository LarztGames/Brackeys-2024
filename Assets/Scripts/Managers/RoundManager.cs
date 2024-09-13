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
        private Image timerImage; // Cambiado a Image en lugar de Slider
        private RoundState state;

        [SerializeField]
        private Animator stormIconAnimator;

        [SerializeField]
        private GameObject rainEffect;

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

            // Inicializar el valor del fillAmount del timer
            timerImage.fillAmount = 1f; // Esto representa 100% lleno

            stormIconAnimator.SetFloat("calm", _currentCalmTimer);
            state = RoundState.Calm;
            #endregion
        }

        void Update()
        {
            if (state != RoundState.Calm)
            {
                rainEffect.SetActive(true);
            }
            else
            {
                rainEffect.SetActive(false);
            }
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
                // Actualiza el fillAmount de la imagen basado en el tiempo restante
                timerImage.fillAmount = Mathf.Lerp(
                    timerImage.fillAmount,
                    _currentCalmTimer / calmTime,
                    5 * Time.deltaTime
                );
            }
            else
            {
                // Transition to Storm
                state = RoundState.Storm;
                timerImage.fillAmount = 0;
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
                timerImage.fillAmount = 0;
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
                timerImage.fillAmount = Mathf.Lerp(
                    timerImage.fillAmount,
                    _currentTransitionTimer / transitionTime,
                    5 * Time.deltaTime
                );
            }
            else
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
                {
                    WaveManager.instance.NextWave();
                    // Reset de los temporizadores
                    stormIconAnimator.SetBool("transition", false);
                    _currentCalmTimer = calmTime;
                    _currentStormTimer = stormTime;
                    _currentTransitionTimer = 0;
                    timerImage.fillAmount = 1f; // Reiniciar el fillAmount a 100%
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
