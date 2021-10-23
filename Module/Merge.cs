using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public partial class Merge : Module
{
    public const string name = "Merge";

    private ElementCarrier input1 = null;
    private ElementCarrier input2 = null;
}

public partial class Merge : Module
{
    private Direction _outputDirection;
    public Direction outputDirection
    {
        get => _outputDirection;
        set
        {
            _outputDirection = value;
            if (_outputDirection == Direction.Up)
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (_outputDirection == Direction.Left)
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            else if (_outputDirection == Direction.Down)
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
            else if (_outputDirection == Direction.Right)
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
    }
}

public partial class Merge : Module
{
    public Merge(MergeInfo info) : base(info.rc)
    {
        this.outputDirection = info.direction;
        gameObject.name = Merge.name;

        Utils.SetSprite(gameObject, SpritePath.Module.merge);
        Utils.SetSpriteSortingOrder(gameObject, 2);
    }

    public override CarrierTodo AcknowledgeModule(ElementCarrier carrier)
    {
        if (input1 == null && input2 == null)
        {
            input1 = new ElementCarrier(carrier);
            input1.enabled = false;

        }
        else if (input1 != null && input2 == null)
        {
            Assert.IsNotNull(input1);
            Assert.IsNull(input2);

            input2 = new ElementCarrier(carrier);
            ElementCarrier mergedCarrier = MergeCarriers(input1, input2);
            if (mergedCarrier != null)
            {
                mergedCarrier.xy = Map.FirstFrameXy(this.rc, this.outputDirection);
                mergedCarrier.enabled = false;
            }

            input1.Destroy();
            input2.Destroy();
            input1 = null;
            input2 = null;
            return CarrierTodo.Destroy;
        }

        return CarrierTodo.Destroy;
    }

    private ElementCarrier MergeCarriers(ElementCarrier here, ElementCarrier there)
    {
        // Can only merge carriers with same number of elements
        if (here.numElements != there.numElements) return null;

        if (here.direction == there.direction) return null;

        // Invalid if any carriers coming from the conflicting direction
        if (here.direction == GetOppositeDirection(this.outputDirection) ||
            there.direction == GetOppositeDirection(this.outputDirection))
            return null;

        if (there.direction == this.outputDirection)
            return MergeCarriers(there, here);

        ElementCarrier merged = MergeCarriersFrom(here, there, here.direction);
        merged.direction = this.outputDirection;

        return merged;
    }

    private ElementCarrier MergeCarriersFrom(
        ElementCarrier here, ElementCarrier there, Direction from)
    {
        ElementCarrier merged = new ElementCarrier(here);
        if (from == Direction.Up)
        {
            if (merged.numElements == 1)
            {
                // ↓ x
                //   o
                Assert.IsNotNull(merged.midE);
                (merged.botE, merged.midE) = (merged.midE, merged.botE);

                Assert.IsNotNull(there.midE);
                merged.topE = new Element(there.midE.type);
            }
            else if (merged.numElements == 2)
            {
                // ↓ xx
                //   oo
                Assert.IsNotNull(merged.leftE);
                Assert.IsNotNull(merged.rightE);
                (merged.botLeftE, merged.leftE) = (merged.leftE, merged.botLeftE);
                (merged.botRightE, merged.rightE) = (merged.rightE, merged.botRightE);

                Assert.IsNotNull(there.leftE);
                Assert.IsNotNull(there.rightE);
                merged.topLeftE = new Element(there.leftE.type);
                merged.topRightE = new Element(there.rightE.type);
            }
        }
        else if (from == Direction.Down)
        {
            if (merged.numElements == 1)
            {
                //   o
                // ↑ x
                Assert.IsNotNull(merged.midE);
                (merged.topE, merged.midE) = (merged.midE, merged.topE);

                Assert.IsNotNull(there.midE);
                merged.botE = new Element(there.midE.type);
            }
            else if (merged.numElements == 2)
            {
                //   oo
                // ↑ xx
                Assert.IsNotNull(merged.leftE);
                Assert.IsNotNull(merged.rightE);
                (merged.topLeftE, merged.leftE) = (merged.leftE, merged.topLeftE);
                (merged.topRightE, merged.rightE) = (merged.rightE, merged.topRightE);

                Assert.IsNotNull(there.leftE);
                Assert.IsNotNull(there.rightE);
                merged.botLeftE = new Element(there.leftE.type);
                merged.botRightE = new Element(there.rightE.type);
            }
        }
        else if (from == Direction.Left)
        {
            if (merged.numElements == 1)
            {
                // → xo
                Assert.IsNotNull(merged.midE);
                (merged.rightE, merged.midE) = (merged.midE, merged.rightE);

                Assert.IsNotNull(there.midE);
                merged.leftE = new Element(there.midE.type);
            }
            else if (merged.numElements == 2)
            {
                // → xo
                //   xo
                Assert.IsNotNull(merged.topE);
                Assert.IsNotNull(merged.botE);
                (merged.topRightE, merged.topE) = (merged.topE, merged.topRightE);
                (merged.botRightE, merged.botE) = (merged.botE, merged.botRightE);

                Assert.IsNotNull(there.topE);
                Assert.IsNotNull(there.botE);
                merged.topLeftE = new Element(there.topE.type);
                merged.botLeftE = new Element(there.botE.type);
            }
        }
        else if (from == Direction.Right)
        {
            if (merged.numElements == 1)
            {
                // ox ←
                Assert.IsNotNull(merged.midE);
                (merged.leftE, merged.midE) = (merged.midE, merged.leftE);

                Assert.IsNotNull(there.midE);
                merged.rightE = new Element(there.midE.type);
            }
            else if (merged.numElements == 2)
            {
                // ox ←
                // ox
                Assert.IsNotNull(merged.topE);
                Assert.IsNotNull(merged.botE);
                (merged.topLeftE, merged.topE) = (merged.topE, merged.topLeftE);
                (merged.botLeftE, merged.botE) = (merged.botE, merged.botLeftE);

                Assert.IsNotNull(there.topE);
                Assert.IsNotNull(there.botE);
                merged.topRightE = new Element(there.topE.type);
                merged.botRightE = new Element(there.botE.type);
            }
        }
        return merged;
    }


    private Direction GetOppositeDirection(Direction input)
    {
        if (input == Direction.Up) return Direction.Down;
        else if (input == Direction.Down) return Direction.Up;
        else if (input == Direction.Left) return Direction.Right;
        else return Direction.Left;
    }
}
