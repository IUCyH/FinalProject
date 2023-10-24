using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum LevelUIImages
{
    ProgressBG,
    ProgressBar,
    LevelBG
}

public class ProfileUI : MonoBehaviour
{
    [SerializeField]
    GameObject LevelUIParent;

    void Start()
    {
        var image = GetComponent<Image>();
        image.sprite = DataManager.Instance.GetSprite(SpriteAssetBundleTag.UI, "UI_Nickname_Level.png");

        var images = LevelUIParent.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            if (i == (int)LevelUIImages.LevelBG)
            {
                images[i].sprite = DataManager.Instance.GetSprite(SpriteAssetBundleTag.UI, "UI_LevelBG.png");
            }
            else
            {
                images[i].sprite = DataManager.Instance.GetSprite(SpriteAssetBundleTag.UI, "UI_NEMO.png");
            }
        }
    }
}
