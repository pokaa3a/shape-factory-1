using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public partial class ElementCarrier
{
    public const float patternStep = 0.08f;
    public Direction direction;
    public GameObject gameObject;
    public List<Element> elements = new List<Element>();
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
            elements[0].xy = new Vector2(-1, -1) * patternStep;
            Utils.SetParent(elements[0].gameObject, gameObject);
            Utils.SetSpriteSortingOrder(elements[0].gameObject, 2);
        }
    }
    public Element topE
    {
        get { return elements[1]; }
        set
        {
            elements[1] = value;
            elements[1].xy = new Vector2(-1, 0) * patternStep;
            Utils.SetParent(elements[1].gameObject, gameObject);
            Utils.SetSpriteSortingOrder(elements[1].gameObject, 2);
        }
    }
    public Element topRightE
    {
        get { return elements[2]; }
        set
        {
            elements[2] = value;
            elements[2].xy = new Vector2(-1, 1) * patternStep;
            Utils.SetParent(elements[2].gameObject, gameObject);
            Utils.SetSpriteSortingOrder(elements[2].gameObject, 2);
        }
    }
    public Element leftE
    {
        get { return elements[3]; }
        set
        {
            elements[3] = value;
            elements[3].xy = new Vector2(0, -1) * patternStep;
            Utils.SetParent(elements[3].gameObject, gameObject);
            Utils.SetSpriteSortingOrder(elements[3].gameObject, 2);
        }
    }
    public Element midE
    {
        get { return elements[4]; }
        set
        {
            elements[4] = value;
            elements[4].xy = new Vector2(0, 0) * patternStep;
            Utils.SetParent(elements[4].gameObject, gameObject);
            Utils.SetSpriteSortingOrder(elements[4].gameObject, 2);
        }
    }
    public Element rightE
    {
        get { return elements[5]; }
        set
        {
            elements[5] = value;
            elements[5].xy = new Vector2(0, 1) * patternStep;
            Utils.SetParent(elements[5].gameObject, gameObject);
            Utils.SetSpriteSortingOrder(elements[5].gameObject, 2);
        }
    }
    public Element botLeftE
    {
        get { return elements[6]; }
        set
        {
            elements[6] = value;
            elements[6].xy = new Vector2(1, -1) * patternStep;
            Utils.SetParent(elements[6].gameObject, gameObject);
            Utils.SetSpriteSortingOrder(elements[6].gameObject, 2);
        }
    }
    public Element botE
    {
        get { return elements[7]; }
        set
        {
            elements[7] = value;
            elements[7].xy = new Vector2(1, 0) * patternStep;
            Utils.SetParent(elements[7].gameObject, gameObject);
            Utils.SetSpriteSortingOrder(elements[7].gameObject, 2);
        }
    }
    public Element botRightE
    {
        get { return elements[8]; }
        set
        {
            elements[8] = value;
            elements[8].xy = new Vector2(1, 1) * patternStep;
            Utils.SetParent(elements[8].gameObject, gameObject);
            Utils.SetSpriteSortingOrder(elements[8].gameObject, 2);
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
            if (!Map.InsideMap(elementCarrier.xy))
            {
                elementCarrier.Destroy();
                return;
            }

            if (ElementRunner.Instance.firstFrame)
            {
                Vector2Int rc = Map.XYtoRC(elementCarrier.xy);
                CarrierTodo todo = Map.Instance.GetTile(rc).AcknowledgeTile(elementCarrier);
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
    public ElementCarrier(ElementType elementType, Vector2 xy, Direction direction)
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
        midE = new Element(elementType);

        this.xy = xy;
        ElementRunner.Instance.AddCarrier(this);
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
                    Element newElement = new Element(e.type);
                    elements[(r + 1) * 3 + (c + 1)] = newElement;
                    Utils.SetParent(newElement.gameObject, gameObject);
                    Utils.SetSpriteSortingOrder(newElement.gameObject, 2);
                    newElement.xy = new Vector2(c, r) * patternStep;
                }
            }
        }
        this.xy = other.xy;
        ElementRunner.Instance.AddCarrier(this);
    }

    public Element GetElement(int r, int c)
    {
        // -1 <= (r, c) <= 1
        if (r < -1 || r > 1 || c < -1 || c > 1) return null;
        return elements[(r + 1) * 3 + (c + 1)];
    }

    // x ->  o = xo
    // x ->  o = xo
    //       o    o
    // x ->  o = xo
    //      oo   oo
    // public bool AddElementFrom(ElementType elementType, Direction direction)
    // {
    //     if (numElements == 4) return false;
    //     if (direction == this.direction) return false;
    //     int numElementsBefore = numElements;

    //     if (direction == Direction.Up)
    //     {
    //         if (numElements == 1)
    //         {
    //             Assert.IsNotNull(midE);
    //             botE = midE;
    //             midE = null;
    //             topE = new Element(elementType);
    //         }
    //         else if (numElements == 2)
    //         {
    //             if (topE != null) return false;

    //             Assert.IsNotNull(leftE);
    //             Assert.IsNotNull(rightE);

    //             botLeftE = leftE;
    //             botRightE = rightE;
    //             leftE = null;
    //             rightE = null;
    //             topE = new Element(elementType);
    //         }
    //         else if (numElements == 3)
    //         {
    //             Assert.IsNotNull(botLeftE);
    //             Assert.IsNotNull(botRightE);

    //             if (topLeftE == null) topLeftE = new Element(elementType);
    //             else topRightE = new Element(elementType);
    //         }
    //     }
    //     else if (direction == Direction.Down)
    //     {
    //         if (numElements == 1)
    //         {
    //             Assert.IsNotNull(midE);
    //             topE = midE;
    //             midE = null;
    //             botE = new Element(elementType);
    //         }
    //         else if (numElements == 2)
    //         {
    //             Assert.IsNotNull(leftE);
    //             Assert.IsNotNull(rightE);

    //             topLeftE = leftE;
    //             topRightE = rightE;
    //             leftE = null;
    //             rightE = null;
    //             botE = new Element(elementType);
    //         }
    //         else if (numElements == 3)
    //         {
    //             Assert.IsNotNull(topLeftE);
    //             Assert.IsNotNull(topRightE);

    //             if (botLeftE == null) botLeftE = new Element(elementType);
    //             else botRightE = new Element(elementType);
    //         }
    //     }
    //     else if (direction == Direction.Left)
    //     {
    //         if (numElements == 1)
    //         {
    //             Assert.IsNotNull(midE);
    //             rightE = midE;
    //             midE = null;
    //             leftE = new Element(elementType);
    //         }
    //         else if (numElements == 2)
    //         {
    //             Assert.IsNotNull(topE);
    //             Assert.IsNotNull(botE);

    //             topRightE = topE;
    //             botRightE = botE;
    //             topE = null;
    //             botE = null;
    //             leftE = new Element(elementType);
    //         }
    //         else if (numElements == 3)
    //         {
    //             Assert.IsNotNull(topRightE);
    //             Assert.IsNotNull(botRightE);

    //             if (topLeftE == null) topLeftE = new Element(elementType);
    //             else botLeftE = new Element(elementType);
    //         }
    //     }
    //     else if (direction == Direction.Right)
    //     {
    //         if (numElements == 1)
    //         {
    //             Assert.IsNotNull(midE);
    //             leftE = midE;
    //             midE = null;
    //             rightE = new Element(elementType);
    //         }
    //         else if (numElements == 2)
    //         {
    //             Assert.IsNotNull(topE);
    //             Assert.IsNotNull(botE);

    //             topLeftE = topE;
    //             botLeftE = botE;
    //             topE = null;
    //             botE = null;
    //             rightE = new Element(elementType);
    //         }
    //         else if (numElements == 3)
    //         {
    //             Assert.IsNotNull(topLeftE);
    //             Assert.IsNotNull(botLeftE);

    //             if (topRightE == null) topRightE = new Element(elementType);
    //             else botRightE = new Element(elementType);
    //         }
    //     }
    //     else
    //     {
    //         Assert.IsTrue(false, "[ElementCarrier > AddElementFrom] Invalid element type");
    //         return false;
    //     }
    //     Assert.IsTrue(numElementsBefore == numElements + 1);
    //     return true;
    // }

    public void Destroy()
    {
        UnityEngine.Object.Destroy(gameObject);
        ElementRunner.Instance.RemoveCarrier(this);
    }
}