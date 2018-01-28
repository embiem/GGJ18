using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviour {

    public CanvasGroup additionalCanvas;
    public CanvasGroup mainCanvas;
    public CanvasGroup creditsCanvas;

    public CanvasGroup[] allgroups;

    private void Awake()
    {
        ShowMainCanvas();
    }

    public void StartScene(string scene)
    {
        GameManager.instance.LoadLevel(0);
    }

    public void ShowTutorial()
    {
        HideAllGroups();

        ShowGroup(additionalCanvas);
    }

    public void ShowMainCanvas()
    {
        HideAllGroups();

        ShowGroup(mainCanvas);
    }

    public void ShowCredits()
    {
        HideAllGroups();
        ShowGroup(creditsCanvas);

    }

    void HideAllGroups()
    {
        foreach (var item in allgroups)
        {
            item.alpha = 0;
            item.blocksRaycasts = false;
            item.interactable = false;
        }
    }

    void ShowGroup(CanvasGroup group)
    {
        group.alpha = 1;
        group.blocksRaycasts = true;
        group.interactable = true;
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
