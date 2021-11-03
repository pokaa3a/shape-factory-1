using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public partial class LevelSelectionMenu
{
    // TODO: Load number of levels from file
    public GameObject gameObject;
    public Vector2 topLeft;
    public const int levelsPerRow = 5;
    public const float margin = 0.25f;
    public int numLevels = 10;
    public float buttonStep;
}

public partial class LevelSelectionMenu
{
    private static LevelSelectionMenu _instance;
    public static LevelSelectionMenu Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LevelSelectionMenu();
            }
            return _instance;
        }
    }

    private bool _enabled = false;
    public bool enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            // Image image = this.gameObject.GetComponent<Image>();
            // if (image == null) image = this.gameObject.AddComponent<Image>();
            // image.enabled = _enabled;
            gameObject.SetActive(_enabled);
            gameObject.transform.SetSiblingIndex(99);
        }
    }
}

public partial class LevelSelectionMenu
{
    private LevelSelectionMenu()
    {
        // gameObject = GameObject.Find(ObjectPath.levelSelectionMenu);
        // Assert.IsNotNull(gameObject);
        gameObject = new GameObject("LevelSelectionMenu");
        GameObject canvas = GameObject.Find(ObjectPath.canvas);
        Assert.IsNotNull(canvas);
        Utils.SetParent(gameObject, canvas);
        RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
        // Stretch
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = new Vector2(1, 1);
        // Image
        Image image = gameObject.AddComponent<Image>();
        Color imgColor;
        ColorUtility.TryParseHtmlString("#333333", out imgColor);
        image.color = imgColor;

        buttonStep = (Screen.width * (1f - margin)) / (levelsPerRow - 1);
        topLeft = new Vector2(
            -((float)levelsPerRow - 1f) / 2f * buttonStep,
            Screen.height / 2f * (1f - margin));

        CreateLevelButtons();
        CreateReturnButton();
    }

    public void CreateLevelButtons()
    {
        int rows = numLevels / levelsPerRow + (numLevels % levelsPerRow > 0 ? 1 : 0);
        int id = 1;
        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < levelsPerRow; ++c)
            {
                UIButton button = new UIButton($"LevelButton_{id}");
                Utils.SetParent(button.gameObject, this.gameObject);
                button.uv = topLeft + new Vector2(
                    c * buttonStep, -r * buttonStep);
                button.SetSize(new Vector2(buttonStep * 0.6f, buttonStep * 0.6f));
                button.SetImage(SpritePath.UI.levelFrame);

                // Add text object
                GameObject textObject = new GameObject("Number");
                Utils.SetParent(textObject, button.gameObject);
                RectTransform rectTransform = textObject.AddComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(buttonStep * 0.6f, buttonStep * 0.6f);

                Text text = textObject.AddComponent<Text>();
                text.fontSize = 80;
                text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                text.text = $"{id}";
                text.alignment = TextAnchor.MiddleCenter;

                Color textColor;
                ColorUtility.TryParseHtmlString("#CCCCCC", out textColor);
                text.color = textColor;

                id++;
            }
        }
    }

    public void CreateReturnButton()
    {
        UIButton returnButton = new UIButton($"Return");
        Utils.SetParent(returnButton.gameObject, this.gameObject);
        returnButton.xy = new Vector2(0, -4.3f);
        returnButton.SetSize(new Vector2(200, 200));
        returnButton.SetImage(SpritePath.UI.returnButton);

        Action clickReturn = () => { this.enabled = false; };
        returnButton.clickCallBack = clickReturn;
    }
}