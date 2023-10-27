using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressBarManager : Singleton_DontDestroy<ProgressBarManager>
{
    [SerializeField]
    Image progressBar;
    [SerializeField]
    Image progressBg;
    [SerializeField]
    Canvas loadCanvas;

    protected override void OnStart()
    {
        HideLoadingWindow();
    }

    public void ShowLoadingWindow()
    {
        loadCanvas.enabled = true;
    }

    public void HideLoadingWindow()
    {
        loadCanvas.enabled = false;
    }

    public void UpdateProgressBar(Sprite progressbarBg, Sprite progressbar, float fillAmount)
    {

        progressBar.sprite = progressbar;
        progressBg.sprite = progressbarBg;

        progressBar.fillAmount = fillAmount;
    }
}
