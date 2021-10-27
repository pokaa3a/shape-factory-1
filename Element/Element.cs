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

    private ElementType _type;
    public ElementType type
    {
        get => _type;
        set
        {
            _type = value;
            SetElementSprite(_type, this.color);
        }
    }

    private PaintColor _color;
    public PaintColor color
    {
        get => _color;
        set
        {
            _color = value;
            SetElementSprite(this.type, _color);
        }
    }
}

public partial class Element
{
    public Element(ElementType type, PaintColor color = PaintColor.White)
    {
        gameObject = new GameObject();
        this.type = type;
        this.color = color;

        Utils.SetSpriteSortingOrder(gameObject, 2);
    }

    public Element(Element other)
    {
        gameObject = new GameObject();
        this.type = other.type;
        this.color = other.color;

        Utils.SetSpriteSortingOrder(gameObject, 2);
    }

    private void SetElementSprite(ElementType type, PaintColor color)
    {
        switch (type)
        {
            case ElementType.Circle:
                switch (color)
                {
                    case PaintColor.White:
                        Utils.SetSprite(gameObject, SpritePath.Element.Circle.white);
                        break;
                    case PaintColor.Blue:
                        Utils.SetSprite(gameObject, SpritePath.Element.Circle.blue);
                        break;
                    case PaintColor.Yellow:
                        Utils.SetSprite(gameObject, SpritePath.Element.Circle.yellow);
                        break;
                    case PaintColor.Red:
                        Utils.SetSprite(gameObject, SpritePath.Element.Circle.red);
                        break;
                    default:
                        break;
                }
                gameObject.name = "Element-circle";
                break;
            case ElementType.Cross:
                switch (color)
                {
                    case PaintColor.White:
                        Utils.SetSprite(gameObject, SpritePath.Element.Cross.white);
                        break;
                    case PaintColor.Blue:
                        Utils.SetSprite(gameObject, SpritePath.Element.Cross.blue);
                        break;
                    case PaintColor.Yellow:
                        Utils.SetSprite(gameObject, SpritePath.Element.Cross.yellow);
                        break;
                    case PaintColor.Red:
                        Utils.SetSprite(gameObject, SpritePath.Element.Cross.red);
                        break;
                    default:
                        break;
                }
                gameObject.name = "Element-cross";
                break;
            case ElementType.Square:
                switch (color)
                {
                    case PaintColor.White:
                        Utils.SetSprite(gameObject, SpritePath.Element.Square.white);
                        break;
                    case PaintColor.Blue:
                        Utils.SetSprite(gameObject, SpritePath.Element.Square.blue);
                        break;
                    case PaintColor.Yellow:
                        Utils.SetSprite(gameObject, SpritePath.Element.Square.yellow);
                        break;
                    case PaintColor.Red:
                        Utils.SetSprite(gameObject, SpritePath.Element.Square.red);
                        break;
                    default:
                        break;
                }
                gameObject.name = "Element-square";
                break;
            case ElementType.Triangle:
                switch (color)
                {
                    case PaintColor.White:
                        Utils.SetSprite(gameObject, SpritePath.Element.Triangle.white);
                        break;
                    case PaintColor.Blue:
                        Utils.SetSprite(gameObject, SpritePath.Element.Triangle.blue);
                        break;
                    case PaintColor.Yellow:
                        Utils.SetSprite(gameObject, SpritePath.Element.Triangle.yellow);
                        break;
                    case PaintColor.Red:
                        Utils.SetSprite(gameObject, SpritePath.Element.Triangle.red);
                        break;
                    default:
                        break;
                }
                gameObject.name = "Element-triangle";
                break;
            default:
                break;
        }
    }
}
