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
    public const float movingSpeed = 1f;
    public ElementType type;
    public GameObject gameObject;
    private Component component;
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
            gameObject.transform.position = _xy;
        }
    }
}

public partial class Element
{
    public class Component : MonoBehaviour
    {
        public Direction direction;
        public float speed;
        public Element element;

        public void Initialize(Element element, Direction direction, float speed)
        {
            this.element = element;
            this.direction = direction;
            this.speed = speed;
        }

        void FixedUpdate()
        {
            switch (direction)
            {
                case Direction.Up:
                    element.xy += Vector2.up * speed * Time.deltaTime;
                    break;
                case Direction.Down:
                    element.xy += Vector2.down * speed * Time.deltaTime;
                    break;
                case Direction.Left:
                    element.xy += Vector2.left * speed * Time.deltaTime;
                    break;
                case Direction.Right:
                    element.xy += Vector2.right * speed * Time.deltaTime;
                    break;
                default:
                    break;
            }

            if (!Map.InsideMap(element.xy))
            {
                Destroy(gameObject);
            }
        }
    }
}

public partial class Element
{
    public Element(ElementType type, Vector2Int rc, Direction direction)
    {
        this.type = type;
        gameObject = new GameObject();
        component = gameObject.AddComponent<Component>();
        component.Initialize(this, direction, Element.movingSpeed);
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

        xy = Map.RCtoXY(rc);
    }
}
