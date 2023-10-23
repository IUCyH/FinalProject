using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoadTest : MonoBehaviour
{
    Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        var sprite = DataManager.Instance.GetSprite(SpriteAssetBundleTag.UI, "UI_Friend");

        image.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
