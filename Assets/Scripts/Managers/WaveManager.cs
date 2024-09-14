using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance { get; set; }

    [SerializeField]
    private List<GameObject> waves = new List<GameObject>();

    private int _currentWave;
    private bool _endlessMode;

    void Awake()
    {
        instance = (instance != null) ? instance : this;
    }

    void Start()
    {
        _endlessMode = EndlessMode.instance.GetEndLessMode();
        _currentWave = 0;
        if (_endlessMode)
        {
            Debug.Log("Se ha activado el modo endless");
        }
    }

    public void StartWave()
    {
        _currentWave = Mathf.Min(_currentWave, waves.Count - 1);
        if (!waves[_currentWave].activeSelf)
        {
            waves[_currentWave].SetActive(true);
        }
    }

    public void NextWave()
    {
        if (_currentWave >= waves.Count)
        {
            // TODO hay que poner si el jugador quiere un modo infinito
            Debug.Log($"Current wave: {_currentWave}. Max waves: {waves.Count}.");
            if (!_endlessMode)
            {
                GameManager.instance.OnWin("WinScene");
            }
        }
        else
        {
            _currentWave += 1;
        }
    }

    public void SetEndlessMode() => _endlessMode = true;
}
