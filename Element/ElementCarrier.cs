using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum CarrierTodo
{
    Reveal,
    Hide,
    Destroy
}

public partial class ElementCarrier
{
    public const float patternStep = 0.08f;
    public const float timePerTile = 1.1f;
    public Direction direction;
    public GameObject gameObject;
    public List<Element> elements = new List<Element>();
    public bool moveable = true;
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

    private bool _enabled = false;
    public bool enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            foreach (Element e in elements)
            {
                if (e != null) e.enabled = _enabled;
            }
        }
    }

    public int numElements
    {
        get
        {
            int n = 0;
            foreach (var e in elements) n += e == null ? 0 : 1;
            return n;
        }
    }

    public Element topLeftE
    {
        get { return elements[0]; }
        set
        {
            elements[0] = value;
            if (elements[0] != null)
            {
                Utils.SetParent(elements[0].gameObject, gameObject);
                Utils.SetSpriteSortingOrder(elements[0].gameObject, 2);
                elements[0].xy = new Vector2(-1, 1) * patternStep;
            }
        }
    }
    public Element topE
    {
        get { return elements[1]; }
        set
        {
            elements[1] = value;
            if (elements[1] != null)
            {
                Utils.SetParent(elements[1].gameObject, gameObject);
                Utils.SetSpriteSortingOrder(elements[1].gameObject, 2);
                elements[1].xy = new Vector2(0, 1) * patternStep;
            }
        }
    }
    public Element topRightE
    {
        get { return elements[2]; }
        set
        {
            elements[2] = value;
            if (elements[2] != null)
            {
                Utils.SetParent(elements[2].gameObject, gameObject);
                Utils.SetSpriteSortingOrder(elements[2].gameObject, 2);
                elements[2].xy = new Vector2(1, 1) * patternStep;
            }
        }
    }
    public Element leftE
    {
        get { return elements[3]; }
        set
        {
            elements[3] = value;
            if (elements[3] != null)
            {
                Utils.SetParent(elements[3].gameObject, gameObject);
                Utils.SetSpriteSortingOrder(elements[3].gameObject, 2);
                elements[3].xy = new Vector2(-1, 0) * patternStep;
            }
        }
    }
    public Element midE
    {
        get { return elements[4]; }
        set
        {
            elements[4] = value;
            if (elements[4] != null)
            {
                Utils.SetParent(elements[4].gameObject, gameObject);
                Utils.SetSpriteSortingOrder(elements[4].gameObject, 2);
                elements[4].xy = new Vector2(0, 0) * patternStep;
            }
        }
    }
    public Element rightE
    {
        get { return elements[5]; }
        set
        {
            elements[5] = value;
            if (elements[5] != null)
            {
                Utils.SetParent(elements[5].gameObject, gameObject);
                Utils.SetSpriteSortingOrder(elements[5].gameObject, 2);
                elements[5].xy = new Vector2(1, 0) * patternStep;
            }
        }
    }
    public Element botLeftE
    {
        get { return elements[6]; }
        set
        {
            elements[6] = value;
            if (elements[6] != null)
            {
                Utils.SetParent(elements[6].gameObject, gameObject);
                Utils.SetSpriteSortingOrder(elements[6].gameObject, 2);
                elements[6].xy = new Vector2(-1, -1) * patternStep;
            }
        }
    }
    public Element botE
    {
        get { return elements[7]; }
        set
        {
            elements[7] = value;
            if (elements[7] != null)
            {
                Utils.SetParent(elements[7].gameObject, gameObject);
                Utils.SetSpriteSortingOrder(elements[7].gameObject, 2);
                elements[7].xy = new Vector2(0, -1) * patternStep;
            }
        }
    }
    public Element botRightE
    {
        get { return elements[8]; }
        set
        {
            elements[8] = value;
            if (elements[8] != null)
            {
                Utils.SetParent(elements[8].gameObject, gameObject);
                Utils.SetSpriteSortingOrder(elements[8].gameObject, 2);
                elements[8].xy = new Vector2(1, -1) * patternStep;
            }
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
            if (!elementCarrier.moveable) return;

            Vector2Int prevRc = Map.XYtoRC(elementCarrier.xy);

            // Update carrier's position
            float movement = Time.deltaTime * Map.tileWH.y / timePerTile;
            if (elementCarrier.direction == Direction.Up)
                elementCarrier.xy += Vector2.up * movement;
            else if (elementCarrier.direction == Direction.Down)
                elementCarrier.xy += Vector2.down * movement;
            else if (elementCarrier.direction == Direction.Left)
                elementCarrier.xy += Vector2.left * movement;
            else if (elementCarrier.direction == Direction.Right)
                elementCarrier.xy += Vector2.right * movement;

            if (!Map.InsideMap(elementCarrier.xy))
            {
                elementCarrier.Destroy();
                return;
            }

            Vector2Int cntRc = Map.XYtoRC(elementCarrier.xy);
            if (prevRc != cntRc) // step into a new tile
            {
                CarrierTodo todo = Map.Instance.GetTile(cntRc).AcknowledgeTile(elementCarrier);
                if (todo == CarrierTodo.Destroy)
                {
                    elementCarrier.Destroy();
                    return;
                }
                else if (todo == CarrierTodo.Hide)
                {
                    elementCarrier.enabled = false;
                }
                else if (todo == CarrierTodo.Reveal)
                {
                    elementCarrier.enabled = true;
                }
            }
        }
    }
}

public partial class ElementCarrier
{
    public ElementCarrier(
        ElementType elementType, PaintColor color, Vector2 xy, Direction direction)
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
        midE = new Element(elementType, color);

        this.xy = xy;
        // ElementRunner.Instance.AddCarrier(this);
    }

    // Copy constructor
    public ElementCarrier(ElementCarrier other)
    {
        gameObject = new GameObject("ElementCarrier");
        component = gameObject.AddComponent<Component>();
        component.elementCarrier = this;
        this.direction = other.direction;

        for (int i = 0; i < 9; ++i)
        {
            elements.Add(null);
        }

        for (int r = -1; r <= 1; ++r)
        {
            for (int c = -1; c <= 1; ++c)
            {
                Element e = other.GetElement(r, c);
                if (e != null)
                {
                    Element newElement = new Element(e);
                    elements[(r + 1) * 3 + (c + 1)] = newElement;
                    Utils.SetParent(newElement.gameObject, gameObject);
                    Utils.SetSpriteSortingOrder(newElement.gameObject, 2);
                    newElement.xy = new Vector2(c, r) * patternStep;
                }
            }
        }
        this.xy = other.xy;
        // ElementRunner.Instance.AddCarrier(this);
    }

    public ElementCarrier(Vector2Int rc, Direction direction)
    {
        gameObject = new GameObject("ElementCarrier");
        component = gameObject.AddComponent<Component>();
        component.elementCarrier = this;
        this.direction = direction;

        for (int i = 0; i < 9; ++i)
        {
            elements.Add(null);
        }
        this.xy = Map.RCtoXY(rc);
        // ElementRunner.Instance.AddCarrier(this);
    }

    public Element GetElement(int r, int c)
    {
        // -1 <= (r, c) <= 1
        if (r < -1 || r > 1 || c < -1 || c > 1) return null;
        return elements[(r + 1) * 3 + (c + 1)];
    }

    public void Destroy()
    {
        UnityEngine.Object.Destroy(gameObject);
        // ElementRunner.Instance.RemoveCarrier(this);
    }
}