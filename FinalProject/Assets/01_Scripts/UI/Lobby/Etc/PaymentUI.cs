using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum PaymentUIImages
{
    Background,
    SunCoin,
    SolarCoin
}

public class PaymentUI : MonoBehaviour
{
    void Start()
    {
        var images = GetComponentsInChildren<Image>();

        for (int i = 0; i < images.Length; i++)
        {
            if (i != (int)PaymentUIImages.Background)
            {
                var spriteName = string.Format("UI_{0}.png", images[i].name);

                images[i].sprite = DataManager.Instance.GetSprite(SpriteAssetBundleTag.UI, spriteName);
            }
            else
            {
                images[i].sprite = DataManager.Instance.GetSprite(SpriteAssetBundleTag.UI, "UI_NEMO.png");
            }
        }
    }
}
