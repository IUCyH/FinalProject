using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLayout_Vertical
{
    List<RectTransform> items = new List<RectTransform>();
    (float x, float y) firstItemPos;
    (int horizontal, int vertical) distEachButton;
    int maxCountInLine;

    public GridLayout_Vertical(int maxCountInLine, (float x, float y) firstItemPos, (int horizontal, int vertical) distEachButton)
    {
        this.maxCountInLine = maxCountInLine;
        this.firstItemPos = firstItemPos;
        this.distEachButton = distEachButton;
    }

    public void AddItem(RectTransform item)
    {
        items.Add(item);
    }

    public void SetItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];

            if (i > 0)
            {
                var posX = items[i - 1].anchoredPosition.x;
                var posY = items[i - 1].anchoredPosition.y - distEachButton.vertical;

                if (i % maxCountInLine == 0)
                {
                    posX += distEachButton.horizontal;
                    posY = firstItemPos.y;
                }

                item.anchoredPosition = new Vector2(posX, posY);
            }
            else
            {
                item.anchoredPosition = new Vector2(firstItemPos.x, firstItemPos.y);
            }
        }
    }
}
