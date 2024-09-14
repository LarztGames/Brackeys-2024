using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    public void ShowHide(GameObject showUI) => showUI.SetActive(!showUI.activeSelf);

    public void Hide(GameObject showUI) => showUI.SetActive(false);

    public void Show(GameObject showUI) => showUI.SetActive(true);
}
