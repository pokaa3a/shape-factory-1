using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Grow : Module
{
    public const string name = "Grow";
}

public partial class Grow : Module
{
    private Direction _direction;
    public Direction direction
    {
        get => _direction;
        set
        {
            _direction = value;
            if (_direction == Direction.Up || _direction == Direction.Down)
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}

public partial class Grow : Module
{
    public Grow(ModuleConfig config) : base(config.rc)
    {
        this.direction = config.direction;
        gameObject.name = Grow.name;

        Utils.SetSprite(gameObject, SpritePath.Module.grow);
        Utils.SetSpriteSortingOrder(gameObject, 2);
    }

    public override CarrierTodo AcknowledgeModule(ElementCarrier carrier)
    {
        ElementCarrier splitCarrier = GrowCarrier(carrier);
        if (splitCarrier != null)
        {
            splitCarrier.xy = Map.FirstFrameXy(this.rc, carrier.direction);
            splitCarrier.enabled = false;
        }
        return CarrierTodo.Destroy;
    }

    private ElementCarrier GrowCarrier(ElementCarrier carrier)
    {
        ElementCarrier splitCarrier = new ElementCarrier(this.rc, carrier.direction);

        if (this.direction == Direction.Up ||
            this.direction == Direction.Down)
        {
            // Grow vertically
            if (carrier.numElements == 1)
            {
                splitCarrier.topE = new Element(carrier.midE);
                splitCarrier.botE = new Element(carrier.midE);
            }
            else if (carrier.numElements == 2)
            {
                if (carrier.topE != null || carrier.botE != null) return null;
                splitCarrier.topLeftE = new Element(carrier.leftE);
                splitCarrier.botLeftE = new Element(carrier.leftE);
                splitCarrier.topRightE = new Element(carrier.rightE);
                splitCarrier.botRightE = new Element(carrier.rightE);
            }
        }
        else    // Direction.Left || Direction.Right
        {
            // Gow horizontally
            if (carrier.numElements == 1)
            {
                splitCarrier.leftE = new Element(carrier.midE);
                splitCarrier.rightE = new Element(carrier.midE);
            }
            else if (carrier.numElements == 2)
            {
                if (carrier.leftE != null || carrier.rightE != null) return null;
                splitCarrier.topLeftE = new Element(carrier.topE);
                splitCarrier.topRightE = new Element(carrier.topE);
                splitCarrier.botLeftE = new Element(carrier.botE);
                splitCarrier.botRightE = new Element(carrier.botE);
            }
        }
        return splitCarrier;
    }
}