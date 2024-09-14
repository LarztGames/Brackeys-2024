using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessMode : MonoBehaviour
{
    private bool _endlessMode;
    public static EndlessMode instance { get; set; }

    void Awake()
    {
        instance = (instance != null) ? instance : this;
        DontDestroyOnLoad(gameObject);
        _endlessMode = false;
    }

    public void UnSetEndLessMode() => _endlessMode = false;

    public void SetEndLessMode() => _endlessMode = true;

    public bool GetEndLessMode() => _endlessMode;
}
