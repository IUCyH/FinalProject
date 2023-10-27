using System.Collections;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField]
    Texture2D cursor1Img;

    [SerializeField]
    Texture2D cursor2Img;

    void Start()
    {
        StartCoroutine(UpdateCoroutine());
    }

    IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            if (Time.timeScale > 0f)
            {
                ChangeToCursor(cursor2Img, 0.03f);
            }
            else
            {
                ChangeToCursor(cursor1Img, 0.03f);
            }

            yield return null;
        }
    }

    void ChangeToCursor(Texture2D cursorImage, float cusorScale)
    {
        Texture2D resizedCursor = ScaleTexture(cursorImage, cusorScale);


        Vector2 hotSpot = new Vector2(resizedCursor.width / 2, resizedCursor.height / 2); //커서 이미지 위치를 커서의 중앙으로 보정
        Cursor.SetCursor(resizedCursor, hotSpot, CursorMode.ForceSoftware);
    }

    public Texture2D ScaleTexture(Texture2D source, float _scaleFactor)
    {
        if (_scaleFactor == 1f)
        {
            return source;
        }
        else if (_scaleFactor == 0f)
        {
            return Texture2D.blackTexture;
        }

        int _newWidth = Mathf.RoundToInt(source.width * _scaleFactor);
        int _newHeight = Mathf.RoundToInt(source.height * _scaleFactor);



        Color[] _scaledTexPixels = new Color[_newWidth * _newHeight];

        for (int _yCord = 0; _yCord < _newHeight; _yCord++)
        {
            float _vCord = _yCord / (_newHeight * 1f);
            int _scanLineIndex = _yCord * _newWidth;

            for (int _xCord = 0; _xCord < _newWidth; _xCord++)
            {
                float _uCord = _xCord / (_newWidth * 1f);

                _scaledTexPixels[_scanLineIndex + _xCord] = source.GetPixelBilinear(_uCord, _vCord);
            }
        }

        // Create Scaled Texture
        Texture2D result = new Texture2D(_newWidth, _newHeight, source.format, false);
        result.SetPixels(_scaledTexPixels, 0);
        result.Apply();

        return result;
    }
}
