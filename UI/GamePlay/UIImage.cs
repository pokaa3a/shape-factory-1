using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class UIImage
{
    public GameObject gameObject;
}

public partial class UIImage
{
    private Vector2 _xy;
    public Vector2 xy
    {
        get => Map.UVtoXY(_uv);
        set { uv = Map.XYtoUV(value); }
    }

    private Vector2 _uv;
    public Vector2 uv
    {
        get => _uv;
        set
        {
            _uv = value;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                rectTransform = gameObject.AddComponent<RectTransform>();
            }
            rectTransform.localPosition = _uv;
        }
    }
}

public partial class UIImage
{
    public UIImage(string name)
    {
        gameObject = new GameObject(name);

        GameObject canvas = GameObject.Find(ObjectPath.canvas);
        Assert.IsNotNull(canvas);
        Utils.SetParent(gameObject, canvas);

        gameObject.AddComponent<RectTransform>();
        gameObject.AddComponent<Image>();
    }

    public void SetImage(string path)
    {
        Image image = gameObject.GetComponent<Image>();
        if (image == null)
        {
            image = gameObject.AddComponent<Image>();
        }
        image.sprite = Resources.Load<Sprite>(path);
    }

    public void SetSize(Vector2 wh)
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            rectTransform = gameObject.AddComponent<RectTransform>();
        }
        rectTransform.sizeDelta = wh;
    }
}