using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotatePose
{
    ClockWise,
    CounterClockWise
}

public partial class Rotate : Module
{
    public const string name = "Rotate";
}

public partial class Rotate : Module
{
    private RotatePose _pose;
    public RotatePose pose
    {
        get => _pose;
        set
        {
            _pose = value;
            if (_pose == RotatePose.ClockWise)
                Utils.SetSprite(gameObject, SpritePath.Module.rotateClockwise);
            else    // RotatePose.CounterClockwise
                Utils.SetSprite(gameObject, SpritePath.Module.rotateCounterClockwise);
            Utils.SetSpriteSortingOrder(gameObject, 2);
        }
    }
}

public partial class Rotate : Module
{
    public Rotate(ModuleConfig config) : base(config)
    {
        this.config = config;
        this.pose = config.rotatePose;
        gameObject.name = Rotate.name;
    }

    public override CarrierTodo AcknowledgeModule(ElementCarrier carrier)
    {
        Direction rotatedDirection = carrier.direction;
        if (pose == RotatePose.ClockWise)
        {
            if (carrier.direction == Direction.Up)
                rotatedDirection = Direction.Right;
            else if (carrier.direction == Direction.Left)
                rotatedDirection = Direction.Up;
            else if (carrier.direction == Direction.Down)
                rotatedDirection = Direction.Left;
            else // carrier.direction == Direction.Right
                rotatedDirection = Direction.Down;
        }
        else    // Counter-Clockwise
        {
            if (carrier.direction == Direction.Up)
                rotatedDirection = Direction.Left;
            else if (carrier.direction == Direction.Left)
                rotatedDirection = Direction.Down;
            else if (carrier.direction == Direction.Down)
                rotatedDirection = Direction.Right;
            else // carrier.direction == Direction.Right
                rotatedDirection = Direction.Up;
        }
        RotateCarrier(carrier);
        // carrier.xy = Map.FirstFrameXy(this.rc, rotatedDirection);

        return CarrierTodo.Hide;
    }

    private void RotateCarrier(ElementCarrier carrier)
    {
        if (carrier.numElements == 1) return;

        if (pose == RotatePose.ClockWise)
        {
            if (carrier.numElements == 2)
            {
                //   0        3
                // 3   1 => 2   0
                //   2        1
                (carrier.topE, carrier.leftE) = (carrier.leftE, carrier.topE);
                (carrier.leftE, carrier.botE) = (carrier.botE, carrier.leftE);
                (carrier.botE, carrier.rightE) = (carrier.rightE, carrier.botE);
            }
            else
            {
                // 012    301
                // 345 => 642
                // 678    785
                (carrier.topLeftE, carrier.leftE) = (carrier.leftE, carrier.topLeftE);
                (carrier.leftE, carrier.botLeftE) = (carrier.botLeftE, carrier.leftE);
                (carrier.botLeftE, carrier.botE) = (carrier.botE, carrier.botLeftE);
                (carrier.botE, carrier.botRightE) = (carrier.botRightE, carrier.botE);
                (carrier.botRightE, carrier.rightE) = (carrier.rightE, carrier.botRightE);
                (carrier.rightE, carrier.topRightE) = (carrier.topRightE, carrier.rightE);
                (carrier.topRightE, carrier.topE) = (carrier.topE, carrier.topRightE);
            }
        }
        else    // Counter-clockwise
        {
            if (carrier.numElements == 2)
            {
                //   0        1
                // 3   1 => 0   2
                //   2        3
                (carrier.topE, carrier.rightE) = (carrier.rightE, carrier.topE);
                (carrier.rightE, carrier.botE) = (carrier.botE, carrier.rightE);
                (carrier.botE, carrier.leftE) = (carrier.leftE, carrier.botE);
            }
            else
            {
                // 012    125
                // 345 => 048
                // 678    367
                (carrier.topRightE, carrier.rightE) = (carrier.rightE, carrier.topRightE);
                (carrier.rightE, carrier.botRightE) = (carrier.botRightE, carrier.rightE);
                (carrier.botRightE, carrier.botE) = (carrier.botE, carrier.botRightE);
                (carrier.botE, carrier.botLeftE) = (carrier.botLeftE, carrier.botE);
                (carrier.botLeftE, carrier.leftE) = (carrier.leftE, carrier.botLeftE);
                (carrier.leftE, carrier.topLeftE) = (carrier.topLeftE, carrier.leftE);
                (carrier.topLeftE, carrier.topE) = (carrier.topE, carrier.topLeftE);
            }
        }
    }
}