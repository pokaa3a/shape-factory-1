using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ElementCarrier
{
    public const float movingSpeed = 1f;
    public const float patternStep = 0.08f;
    public GameObject gameObject;
    private Component component;
}

public partial class ElementCarrier
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

    private Direction _direction;
    public Direction direction
    {
        get { return _direction; }
        set
        {
            _direction = value;
            component.direction = _direction;
        }
    }
}

public partial class ElementCarrier
{
    public class Component : MonoBehaviour
    {
        public Direction direction;
        public ElementCarrier elementCarrier;

        void FixedUpdate()
        {
            switch (direction)
            {
                case Direction.Up:
                    elementCarrier.xy += Vector2.up * movingSpeed * Time.deltaTime;
                    break;
                case Direction.Down:
                    elementCarrier.xy += Vector2.down * movingSpeed * Time.deltaTime;
                    break;
                case Direction.Left:
                    elementCarrier.xy += Vector2.left * movingSpeed * Time.deltaTime;
                    break;
                case Direction.Right:
                    elementCarrier.xy += Vector2.right * movingSpeed * Time.deltaTime;
                    break;
                default:
                    break;
            }

            if (!Map.InsideMap(elementCarrier.xy))
            {
                Destroy(gameObject);
                return;
            }

            Vector2Int rc = Map.XYtoRC(elementCarrier.xy);
            if (!Map.Instance.GetTile(rc).ElementHits(elementCarrier))
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}

public partial class ElementCarrier
{
    private List<Element> elements = new List<Element>();

    public ElementCarrier(ElementType elementType, Vector2Int rc, Direction direction)
    {
        gameObject = new GameObject("ElementCarrier");
        component = gameObject.AddComponent<Component>();
        component.elementCarrier = this;
        this.direction = direction;

        for (int i = 0; i < 9; ++i)
        {
            elements.Add(null);
        }

        // ElementCarrier always carries at least one element
        elements[4] = new Element(elementType);
        Utils.SetParent(elements[4].gameObject, gameObject);
        Utils.SetSpriteSortingOrder(elements[4].gameObject, 2);

        xy = Map.RCtoXY(rc);
    }

    public Element GetElement(int r, int c)
    {
        if (r < 0 || r >= 3 || c < 0 || c >= 3) return null;
        return elements[r * 3 + c];
    }

    public void SetElement(ElementType elementType, int r, int c)
    {
        elements[(r - 1) * 3 + (c - 1)] = new Element(elementType);
        Utils.SetParent(elements[(r - 1) * 3 + (c - 1)].gameObject, gameObject);
        Utils.SetSpriteSortingOrder(elements[(r - 1) * 3 + (c - 1)].gameObject, 2);
        elements[(r - 1) * 3 + (c - 1)].xy = new Vector2(c, r) * patternStep;
    }
}