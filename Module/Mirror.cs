using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MirrorPose
{
    TopLeftBotRight,
    TopRightBotLeft
}

public partial class Mirror : Module
{
    public const string name = "Mirror";

    private MirrorPose _pose = MirrorPose.TopLeftBotRight;
    public MirrorPose pose
    {
        get { return _pose; }
        set
        {
            _pose = value;
            if (_pose == MirrorPose.TopLeftBotRight)
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            else    // MirrorPose.TopRightBotLeft
                gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }
}

public partial class Mirror : Module
{
    public Mirror(MirrorInfo info) : base(info.rc)
    {
        this.pose = info.pose;
        gameObject.name = Mirror.name;

        Utils.SetSprite(gameObject, SpritePath.Module.mirror);
        Utils.SetSpriteSortingOrder(gameObject, 2);
    }

    public override CarrierTodo AcknowledgeModule(ElementCarrier elementCarrier)
    {
        if (pose == MirrorPose.TopLeftBotRight)
        {
            if (elementCarrier.direction == Direction.Up)
            {
                // up -> left
                elementCarrier.xy = Map.FirstFrameXy(this.rc, Direction.Left);
                elementCarrier.direction = Direction.Left;
            }
            else if (elementCarrier.direction == Direction.Down)
            {
                // down -> right
                elementCarrier.xy = Map.FirstFrameXy(this.rc, Direction.Right);
                elementCarrier.direction = Direction.Right;
            }
            else if (elementCarrier.direction == Direction.Left)
            {
                // left -> down
                elementCarrier.xy = Map.FirstFrameXy(this.rc, Direction.Down);
                elementCarrier.direction = Direction.Down;
            }
            else if (elementCarrier.direction == Direction.Right)
            {
                // right -> up
                elementCarrier.xy = Map.FirstFrameXy(this.rc, Direction.Up);
                elementCarrier.direction = Direction.Up;
            }
        }
        else    // pose == MirrorPose.TopRightBotLeft
        {
            if (elementCarrier.direction == Direction.Up)
            {
                // up -> right
                elementCarrier.xy = Map.FirstFrameXy(this.rc, Direction.Right);
                elementCarrier.direction = Direction.Right;
            }
            else if (elementCarrier.direction == Direction.Down)
            {
                // down -> left
                elementCarrier.xy = Map.FirstFrameXy(this.rc, Direction.Left);
                elementCarrier.direction = Direction.Left;
            }
            else if (elementCarrier.direction == Direction.Left)
            {
                // left -> down
                elementCarrier.xy = Map.FirstFrameXy(this.rc, Direction.Down);
                elementCarrier.direction = Direction.Down;
            }
            else if (elementCarrier.direction == Direction.Right)
            {
                // right -> up
                elementCarrier.xy = Map.FirstFrameXy(this.rc, Direction.Up);
                elementCarrier.direction = Direction.Up;
            }
        }
        return CarrierTodo.Hide;
    }
}