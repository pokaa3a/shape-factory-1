using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnPose
{
    TurnLeft,
    TurnRight
}

public partial class Turn : Module
{
    public const string name = "Turn";
}

public partial class Turn : Module
{
    private TurnPose _pose;
    public TurnPose pose
    {
        get => _pose;
        set
        {
            _pose = value;
            if (_pose == TurnPose.TurnLeft)
                Utils.SetSprite(gameObject, SpritePath.Module.turnLeft);
            else    // TurnPose.TurnRight
                Utils.SetSprite(gameObject, SpritePath.Module.turnRight);
            Utils.SetSpriteSortingOrder(gameObject, 2);
        }
    }

    private Direction _direction;
    public Direction direction
    {
        get => _direction;
        set
        {
            _direction = value;
            if (_pose == TurnPose.TurnLeft)
            {
                if (_direction == Direction.Down)
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                else if (_direction == Direction.Right)
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
                else if (_direction == Direction.Up)
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
                else
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else    // TurnRight
            {
                if (_direction == Direction.Up)
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                else if (_direction == Direction.Left)
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
                else if (_direction == Direction.Down)
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
                else
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}

public partial class Turn : Module
{
    public Turn(ModuleConfig config) : base(config)
    {
        this.config = config;
        this.pose = config.turnPose;
        this.direction = config.direction;
        gameObject.name = Turn.name;
    }

    public override CarrierTodo AcknowledgeModule(ElementCarrier carrier)
    {
        if (pose == TurnPose.TurnLeft)
        {
            if ((this.direction == Direction.Left &&
                carrier.direction == Direction.Up) ||
                (this.direction == Direction.Down &&
                carrier.direction == Direction.Left) ||
                (this.direction == Direction.Right &&
                carrier.direction == Direction.Down) ||
                (this.direction == Direction.Up &&
                carrier.direction == Direction.Right))
            {
                carrier.xy = Map.FirstFrameXy(this.rc, this.direction);
                carrier.direction = this.direction;
            }
            else return CarrierTodo.Destroy;

        }
        else    // TurnPose.TurnRight
        {
            if ((this.direction == Direction.Right &&
                carrier.direction == Direction.Up) ||
                (this.direction == Direction.Down &&
                carrier.direction == Direction.Right) ||
                (this.direction == Direction.Left &&
                carrier.direction == Direction.Down) ||
                (this.direction == Direction.Up &&
                carrier.direction == Direction.Left))
            {
                carrier.xy = Map.FirstFrameXy(this.rc, this.direction);
                carrier.direction = this.direction;
            }
            else return CarrierTodo.Destroy;
        }
        return CarrierTodo.Hide;
    }

    public override void ClockwiseRotate()
    {
        if (direction == Direction.Up) direction = Direction.Right;
        else if (direction == Direction.Right) direction = Direction.Down;
        else if (direction == Direction.Down) direction = Direction.Left;
        else /* Direction.Left */ direction = Direction.Up;
    }
}
