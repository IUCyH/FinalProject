using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoadTest : MonoBehaviour
{
    Image image;
    
    // Start is called before the first frame update
    /*void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(LoadImage());
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }

    /*IEnumerator LoadImage()
    {
        while (!DataManager.Instance.LoadCompleted) yield return null;
        Debug.Log("s");
        var sprite = DataManager.Instance.GetSprite(SpriteAssetBundleTag.UI, "UI_Nickname_Level.png");
        image.sprite = sprite;
        yield return new WaitForSeconds(1f);
        sprite = DataManager.Instance.GetSprite(SpriteAssetBundleTag.UI, "UI_Achievements.png");
        image.sprite = sprite;
        yield return new WaitForSeconds(1f);
        sprite = DataManager.Instance.GetSprite(SpriteAssetBundleTag.UI, "UI_Nickname_Level.png");
        image.sprite = sprite;
    }*/
}
