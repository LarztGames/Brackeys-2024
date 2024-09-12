using System.Collections;
using System.Collections.Generic;
using Dungeon;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }

    [SerializeField]
    private GameObject[] dungeonObjects;

    [SerializeField]
    private GameObject[] laboratoryObjects;
    private bool _onLab;

    void Awake()
    {
        instance = (instance != null) ? instance : this;
    }

    void Start()
    {
        _onLab = true;
    }

    public void GoToDungeon()
    {
        RoomManager.instance.ReloadDungeon();
        Camera.main.GetComponent<CameraController>().SetPosition(new Vector2(0, 0));
        _onLab = false;
        foreach (GameObject item in dungeonObjects)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in laboratoryObjects)
        {
            item.SetActive(false);
        }
    }

    public void GoToLaboratory()
    {
        _onLab = true;
        foreach (GameObject item in laboratoryObjects)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in dungeonObjects)
        {
            item.SetActive(false);
        }
    }

    public void OnWin(string sceneName)
    {
        SceneManager.LoadScene(0);
    }

    public void OnLose(string sceneName)
    {
        SceneManager.LoadScene(0);
    }

    public bool OnLab() => _onLab;
}
