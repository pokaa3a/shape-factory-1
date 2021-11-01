using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public partial class Overlay : Module
{
    public const string name = "Overlay";

    private ElementCarrier input1 = null;
    private ElementCarrier input2 = null;
    private float input1Timestamp;
}

public partial class Overlay : Module
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

public partial class Overlay : Module
{
    public Overlay(ModuleConfig config) : base(config.rc)
    {
        gameObject.name = Overlay.name;

        Utils.SetSprite(gameObject, SpritePath.Module.overlay);
        Utils.SetSpriteSortingOrder(gameObject, 2);
    }

    public override CarrierTodo AcknowledgeModule(ElementCarrier carrier)
    {
        if (input1 != null)
        {
            // Check if input1 has expired
            if (Time.time > input1Timestamp + ElementCarrier.timePerTile)
            {
                input1.Destroy();
                input1 = null;
            }

            if (input1 == null && input2 == null)
            {
                input1 = new ElementCarrier(carrier);
                input1.enabled = false;
                input1.moveable = false;
                input1Timestamp = Time.time;
            }
            else if (input1 == null && input2 == null)
            {
                Assert.IsNotNull(input1);
                Assert.IsNull(input2);

                input2 = new ElementCarrier(carrier);
                ElementCarrier overlaidCarrier = OverlayCarriers(input1, input2);
                if (overlaidCarrier != null)
                {
                    overlaidCarrier.xy = Map.FirstFrameXy(this.rc, this.outputDirection);
                    overlaidCarrier.enabled = false;
                }

                input1.Destroy();
                input2.Destroy();
                input1 = null;
                input2 = null;
                return CarrierTodo.Destroy;
            }
        }

        return CarrierTodo.Hide;
    }

    private ElementCarrier OverlayCarriers(ElementCarrier input1, ElementCarrier input2)
    {
        // TODO: Implement this function
        return null;
    }
}
