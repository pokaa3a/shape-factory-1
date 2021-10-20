using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public partial class ElementCarrier
{
    public const float movingSpeed = 1f;
    public const float patternStep = 0.08f;
    public GameObject gameObject;
    private Component component;
    private List<Element> elements = new List<Element>();
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
        }
    }
    public Element topE
    {
        get { return elements[1]; }
        set
        {
            elements[1] = value;
            elements[1].xy = new Vector2(-1, 0) * patternStep;
        }
    }
    public Element topRightE
    {
        get { return elements[2]; }
        set
        {
            elements[2] = value;
            elements[2].xy = new Vector2(-1, 1) * patternStep;
        }
    }
    public Element leftE
    {
        get { return elements[3]; }
        set
        {
            elements[3] = value;
            elements[3].xy = new Vector2(0, -1) * patternStep;
        }
    }
    public Element midE
    {
        get { return elements[4]; }
        set
        {
            elements[4] = value;
            elements[4].xy = new Vector2(0, 0) * patternStep;
        }
    }
    public Element rightE
    {
        get { return elements[5]; }
        set
        {
            elements[5] = value;
            elements[5].xy = new Vector2(0, 1) * patternStep;
        }
    }
    public Element botLeftE
    {
        get { return elements[6]; }
        set
        {
            elements[6] = value;
            elements[6].xy = new Vector2(1, -1) * patternStep;
        }
    }
    public Element botE
    {
        get { return elements[7]; }
        set
        {
            elements[7] = value;
            elements[7].xy = new Vector2(1, 0) * patternStep;
        }
    }
    public Element botRightE
    {
        get { return elements[8]; }
        set
        {
            elements[8] = value;
            elements[8].xy = new Vector2(1, 1) * patternStep;
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
            Vector2Int rcBefore = Map.XYtoRC(elementCarrier.xy);
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
            if (rc != rcBefore)
            {
                if (!Map.Instance.GetTile(rc).ElementHits(elementCarrier))
                {
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }
}

public partial class ElementCarrier
{
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
        // -1 <= (r, c) <= 1
        if (r < -1 || r > 1 || c < -1 || c > 1) return null;
        return elements[(r + 1) * 3 + (c + 1)];
    }

    public bool AddElementFrom(ElementType elementType, Direction direction)
    {
        if (numElements == 4) return false;
        int numElementsBefore = numElements;

        if (direction == Direction.Up)
        {
            if (numElements == 1)
            {
                Assert.IsNotNull(midE);
                botE = midE;
                midE = null;
                topE = new Element(elementType);
            }
            else if (numElements == 2)
            {
                Assert.IsNotNull(leftE);
                Assert.IsNotNull(rightE);

                botLeftE = leftE;
                botRightE = rightE;
                leftE = null;
                rightE = null;
                topE = new Element(elementType);
            }
            else if (numElements == 3)
            {
                Assert.IsNotNull(botLeftE);
                Assert.IsNotNull(botRightE);

                if (topLeftE == null) topLeftE = new Element(elementType);
                else topRightE = new Element(elementType);
            }
        }
        else if (direction == Direction.Down)
        {
            if (numElements == 1)
            {
                Assert.IsNotNull(midE);
                topE = midE;
                midE = null;
                botE = new Element(elementType);
            }
            else if (numElements == 2)
            {
                Assert.IsNotNull(leftE);
                Assert.IsNotNull(rightE);

                topLeftE = leftE;
                topRightE = rightE;
                leftE = null;
                rightE = null;
                botE = new Element(elementType);
            }
            else if (numElements == 3)
            {
                Assert.IsNotNull(topLeftE);
                Assert.IsNotNull(topRightE);

                if (botLeftE == null) botLeftE = new Element(elementType);
                else botRightE = new Element(elementType);
            }
        }
        else if (direction == Direction.Left)
        {
            if (numElements == 1)
            {
                Assert.IsNotNull(midE);
                rightE = midE;
                midE = null;
                leftE = new Element(elementType);
            }
            else if (numElements == 2)
            {
                Assert.IsNotNull(topE);
                Assert.IsNotNull(botE);

                topRightE = topE;
                botRightE = botE;
                topE = null;
                botE = null;
                leftE = new Element(elementType);
            }
            else if (numElements == 3)
            {
                Assert.IsNotNull(topRightE);
                Assert.IsNotNull(botRightE);

                if (topLeftE == null) topLeftE = new Element(elementType);
                else botLeftE = new Element(elementType);
            }
        }
        else if (direction == Direction.Right)
        {
            if (numElements == 1)
            {
                Assert.IsNotNull(midE);
                leftE = midE;
                midE = null;
                rightE = new Element(elementType);
            }
            else if (numElements == 2)
            {
                Assert.IsNotNull(topE);
                Assert.IsNotNull(botE);

                topLeftE = topE;
                botLeftE = botE;
                topE = null;
                botE = null;
                rightE = new Element(elementType);
            }
            else if (numElements == 3)
            {
                Assert.IsNotNull(topLeftE);
                Assert.IsNotNull(botLeftE);

                if (topRightE == null) topRightE = new Element(elementType);
                else botRightE = new Element(elementType);
            }
        }
        else
        {
            Assert.IsTrue(false, "[ElementCarrier > AddElementFrom] Invalid element type");
            return false;
        }
        Assert.IsTrue(numElementsBefore == numElements + 1);
        return true;
    }

    // public void SetElement(ElementType elementType, int r, int c)
    // {
    //     elements[(r - 1) * 3 + (c - 1)] = new Element(elementType);
    //     Utils.SetParent(elements[(r - 1) * 3 + (c - 1)].gameObject, gameObject);
    //     Utils.SetSpriteSortingOrder(elements[(r - 1) * 3 + (c - 1)].gameObject, 2);
    //     elements[(r - 1) * 3 + (c - 1)].xy = new Vector2(c, r) * patternStep;
    // }
}