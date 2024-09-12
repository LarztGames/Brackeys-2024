using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance { get; set; }

    [SerializeField]
    private List<GameObject> waves = new List<GameObject>();

    private int _currentWave;

    void Awake()
    {
        instance = (instance != null) ? instance : this;
    }

    void Start()
    {
        _currentWave = 0;
    }

    public void StartWave()
    {
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
            GameManager.instance.OnWin("WinScene");
            Debug.Log($"Current wave: {_currentWave}. Max waves: {waves.Count}.");
            Debug.Log("No hay mas rondas, modo loop");
            _currentWave = waves.Count - 1;
        }
        else
        {
            _currentWave += 1;
        }
    }
}
