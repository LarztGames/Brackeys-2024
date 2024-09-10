using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingBarObject;

    [SerializeField]
    private Image loadingBarImage;

    [SerializeField]
    private GameObject[] objectsToHide;

    [SerializeField]
    private SceneField coreGamePlay;

    private List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();

    void Awake()
    {
        loadingBarObject.SetActive(false);
    }

    public void StartGame()
    {
        // Hide button and text
        HideMenu();
        loadingBarObject.SetActive(false);

        // Start loading the scenes we need
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(coreGamePlay));

        // Update the loading bar
        StartCoroutine(ProgressLoadingBar());
    }

    private void HideMenu()
    {
        for (int i = 0; i < objectsToHide.Length; i++)
        {
            objectsToHide[i].SetActive(false);
        }
    }

    private IEnumerator ProgressLoadingBar()
    {
        float loadProgress = 0;
        for (int i = 0; i < _scenesToLoad.Count; i++)
        {
            while (!_scenesToLoad[i].isDone)
            {
                loadProgress += _scenesToLoad[i].progress;
                loadingBarImage.fillAmount += loadProgress / _scenesToLoad.Count;
                yield return null;
            }
        }
    }
}
