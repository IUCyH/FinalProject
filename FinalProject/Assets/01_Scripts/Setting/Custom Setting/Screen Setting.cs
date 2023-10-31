using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PGC_ScreenSetting : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI screenSizeSettingText;
    [SerializeField]
    TextMeshProUGUI resolutionSettingText;

    List<string> screenSizeTextList = new List<string>();
    List<string> resolutionTextList = new List<string>();
    List<(int width, int height)> screenResolution = new List<(int width, int height)>();

    int ScreensizeCount;
    int resolutionCount;

    float fixedWidth;
    float fixedHeight;

    bool isOneClick;

    void Start()
    {
        SetScreenValue();
        StartCoroutine(UpdateCoroutine());


        Screen.SetResolution(1920, 1080, true);

        if (PlayerPrefs.HasKey("ChangeWidth"))
        {
            fixedWidth = PlayerPrefs.GetInt("ChangeWidth");
            fixedHeight = PlayerPrefs.GetInt("ChangeHeight");
        }
        else
        {
            fixedWidth = Screen.width;
            fixedHeight = Screen.height;
            PlayerPrefs.SetInt("ChangeWidth", (int)fixedWidth);
            PlayerPrefs.SetInt("ChangeHeight", (int)fixedHeight);
            PlayerPrefs.Save();
        }
    }

    IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            screenSizeSettingText.text = screenSizeTextList[ScreensizeCount];
            resolutionSettingText.text = resolutionTextList[resolutionCount];

            yield return null;
        }
    }
        
    public void ONFullScreenButton()
    {
        if (!isOneClick)
        {
            Screen.fullScreen = false;
            isOneClick = true;
            ScreensizeCount = 1;
        }
        else
        {
            Screen.fullScreen = true;
            isOneClick = false;
            ScreensizeCount = 0;
        }
    }

    public void OnResolutionRightButton()
    {
        resolutionCount++;
        if (resolutionCount > screenResolution.Count - 1)
        {
            resolutionCount = 0;
        }

        Screen.SetResolution(screenResolution[resolutionCount].width, screenResolution[resolutionCount].height, true);
    }

    public void OnResolutionLeftButton()
    {
        resolutionCount--;
        if (resolutionCount < 0)
        {
            resolutionCount = screenResolution.Count - 1; //¸®½ºÆ®ÀÇ ÃÖ´ë ÀÎµ¦½º
        }

        Screen.SetResolution(screenResolution[resolutionCount].width, screenResolution[resolutionCount].height, true);
    }


    void SetScreenValue()
    {
        ScreensizeCount = 0;
        resolutionCount = 0;

        screenSizeTextList.Add("ÄÑÁü");
        screenSizeTextList.Add("²¨Áü");

        screenResolution.Add((1920, 1080));
        screenResolution.Add((1600, 900));
        screenResolution.Add((1440, 900));
        screenResolution.Add((1280, 720));
        screenResolution.Add((1024, 768));
        screenResolution.Add((960, 720));

        resolutionTextList.Add("1920 x 1080");
        resolutionTextList.Add("1600 x 900");
        resolutionTextList.Add("1440 x 900");
        resolutionTextList.Add("1280 x 720");
        resolutionTextList.Add("1024 x 768");
        resolutionTextList.Add("960 x 720");

    }
}
