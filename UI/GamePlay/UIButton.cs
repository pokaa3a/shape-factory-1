using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class UIButton
{
    public GameObject gameObject;
    public Action clickCallBack;
}

public partial class UIButton
{
    private Vector2 _xy;
    public Vector2 xy
    {
        get => Map.UVtoXYCentered(_uv);
        set { uv = Map.XYtoUVCentered(value); }
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

public partial class UIButton
{
    public class ButtonComponent : MonoBehaviour, IPointerClickHandler
    {
        public UIButton uiButton;

        public void OnPointerClick(PointerEventData data)
        {
            uiButton.Click();
        }
    }
}

public partial class UIButton
{
    public UIButton(string name)
    {
        gameObject = new GameObject(name);
        ButtonComponent component = gameObject.AddComponent<ButtonComponent>();
        component.uiButton = this;

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

    public virtual void Click()
    {
        clickCallBack();
    }
}