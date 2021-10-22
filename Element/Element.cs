using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
    Square,
    Triangle,
    Circle,
    Cross
}

public partial class Element
{
    public ElementType type;
    public GameObject gameObject;
}

public partial class Element
{
    private Vector2 _xy;
    public Vector2 xy
    {
        get { return _xy; }
        set
        {
            _xy = value;
            gameObject.transform.localPosition = _xy;
        }
    }

    private bool _enabled = false;
    public bool enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            SpriteRenderer sprRender = gameObject.GetComponent<SpriteRenderer>();
            if (sprRender != null) sprRender.enabled = _enabled;
        }
    }
}

public partial class Element
{
    public Element(ElementType type)
    {
        this.type = type;
        gameObject = new GameObject();
        // component = gameObject.AddComponent<Component>();
        // component.Initialize(this, direction, Element.movingSpeed);
        switch (type)
        {
            case ElementType.Circle:
                Utils.SetSprite(gameObject, SpritePath.Element.circle);
                gameObject.name = "Element-circle";
                break;
            case ElementType.Cross:
                Utils.SetSprite(gameObject, SpritePath.Element.cross);
                gameObject.name = "Element-cross";
                break;
            case ElementType.Square:
                Utils.SetSprite(gameObject, SpritePath.Element.square);
                gameObject.name = "Element-square";
                break;
            case ElementType.Triangle:
                Utils.SetSprite(gameObject, SpritePath.Element.triangle);
                gameObject.name = "Element-triangle";
                break;
            default:
                break;
        }
        Utils.SetSpriteSortingOrder(gameObject, 2);
    }
}
