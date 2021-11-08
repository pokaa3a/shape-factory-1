using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class LevelButton : UIButton
{
    int id;
}

public partial class LevelButton : UIButton
{
    public LevelButton(int id, Vector2 buttonSize) : base($"LevelButton_{id}")
    {
        this.id = id;

        // Add text object
        GameObject textObject = new GameObject("Number");
        Utils.SetParent(textObject, this.gameObject);
        this.SetSize(buttonSize);
        this.SetImage(SpritePath.UI.levelFrame);

        RectTransform rectTransform = textObject.AddComponent<RectTransform>();
        rectTransform.sizeDelta = buttonSize;


        Text text = textObject.AddComponent<Text>();
        text.fontSize = 80;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.text = $"{id}";
        text.alignment = TextAnchor.MiddleCenter;

        Color textColor;
        ColorUtility.TryParseHtmlString("#CCCCCC", out textColor);
        text.color = textColor;

        this.clickCallBack = () =>
        {
            Level lv = LevelUtils.LoadLevel(id);
            Map.Instance.CreateMapFromLevel(lv);
        };
    }
}