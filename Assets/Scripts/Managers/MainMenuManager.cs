using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsToHide;

    [SerializeField]
    private SceneField coreGamePlay;

    private List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();

    public void StartGame()
    {
        // Hide button and text
        HideMenu();
        // Start loading the scenes we need
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(coreGamePlay));
    }

    private void HideMenu()
    {
        for (int i = 0; i < objectsToHide.Length; i++)
        {
            objectsToHide[i].SetActive(false);
        }
    }
}
