using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public partial class Split : Module
{
    public const string name = "Split";
}

public partial class Split : Module
{
    private Direction _direction;
    public Direction direction
    {
        get => _direction;
        set
        {
            _direction = value;
            if (_direction == Direction.Up)
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (_direction == Direction.Left)
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            else if (_direction == Direction.Down)
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
            else if (_direction == Direction.Right)
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
    }
}

public partial class Split : Module
{
    public Split(ModuleConfig config) : base(config)
    {
        this.config = config;
        this.direction = config.direction;
        gameObject.name = Split.name;

        Utils.SetSprite(gameObject, SpritePath.Module.split);
        Utils.SetSpriteSortingOrder(gameObject, 2);
    }

    public override CarrierTodo AcknowledgeModule(ElementCarrier carrier)
    {
        if (carrier.direction != this.direction) return CarrierTodo.Destroy;
        if (carrier.numElements == 1) return CarrierTodo.Destroy;

        SplitCarrier(carrier);
        return CarrierTodo.Destroy;
    }

    private void SplitCarrier(ElementCarrier carrier)
    {
        if (carrier.numElements != 2 && carrier.numElements != 4) return;

        ElementCarrier left =
                    new ElementCarrier(Map.XYtoRC(carrier.xy), carrier.direction);
        ElementCarrier right =
                    new ElementCarrier(Map.XYtoRC(carrier.xy), carrier.direction);
        if (carrier.numElements == 2)
        {
            if (carrier.direction == Direction.Up)
            {
                // x , o
                //   ↑
                //   xo
                left.midE = new Element(carrier.leftE);
                right.midE = new Element(carrier.rightE);
            }
            else if (carrier.direction == Direction.Down)
            {
                //   xo
                //   ↓
                // x , o
                left.midE = new Element(carrier.rightE);
                right.midE = new Element(carrier.leftE);
            }
            else if (carrier.direction == Direction.Left)
            {
                // o, ← o
                // x    x
                left.midE = new Element(carrier.botE);
                right.midE = new Element(carrier.topE);
            }
            else if (carrier.direction == Direction.Right)
            {
                // o → o,
                // x   x
                left.midE = new Element(carrier.topE);
                right.midE = new Element(carrier.botE);
            }
        }
        else if (carrier.numElements == 4)
        {
            if (carrier.direction == Direction.Up)
            {
                // x , o
                // x   o
                //   ↑
                //   xo
                //   xo
                left.topE = new Element(carrier.topLeftE);
                left.botE = new Element(carrier.botLeftE);
                right.topE = new Element(carrier.topRightE);
                right.botE = new Element(carrier.botRightE);
            }
            else if (carrier.direction == Direction.Down)
            {
                //   xo
                //   xo
                //   ↓
                // x , o
                // x   o
                left.topE = new Element(carrier.topRightE);
                left.botE = new Element(carrier.botRightE);
                right.topE = new Element(carrier.topLeftE);
                right.botE = new Element(carrier.botLeftE);
            }
            else if (carrier.direction == Direction.Left)
            {
                // xo, ← xo 
                // xo    xo
                left.leftE = new Element(carrier.botLeftE);
                left.rightE = new Element(carrier.botRightE);
                right.leftE = new Element(carrier.topLeftE);
                right.rightE = new Element(carrier.topRightE);
            }
            else if (carrier.direction == Direction.Right)
            {
                // xo → xo,
                // xo   xo
                left.leftE = new Element(carrier.topLeftE);
                left.rightE = new Element(carrier.topRightE);
                right.leftE = new Element(carrier.botLeftE);
                right.rightE = new Element(carrier.botRightE);
            }
        }
        left.direction = GetSplitLeftDirection(carrier.direction);
        right.direction = GetSplitRightDirection(carrier.direction);

        left.xy = Map.FirstFrameXy(this.rc, left.direction);
        right.xy = Map.FirstFrameXy(this.rc, right.direction);

        left.enabled = false;
        right.enabled = false;
    }

    private Direction GetSplitLeftDirection(Direction output)
    {
        if (output == Direction.Up)
            return Direction.Left;
        else if (output == Direction.Left)
            return Direction.Down;
        else if (output == Direction.Down)
            return Direction.Right;
        else // this.direction == Direction.Right
            return Direction.Up;
    }

    private Direction GetSplitRightDirection(Direction output)
    {
        if (output == Direction.Up)
            return Direction.Right;
        else if (output == Direction.Right)
            return Direction.Down;
        else if (output == Direction.Down)
            return Direction.Left;
        else // this.direction == Direction.Left
            return Direction.Up;
    }

    public override void ClockwiseRotate()
    {
        if (direction == Direction.Up) direction = Direction.Right;
        else if (direction == Direction.Right) direction = Direction.Down;
        else if (direction == Direction.Down) direction = Direction.Left;
        else /* Direction.Left */ direction = Direction.Up;
    }
}