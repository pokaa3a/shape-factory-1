using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PaintColor
{
    White,
    Yellow,
    Blue,
    Red
}

public partial class Paint : Module
{
    public const string name = "Paint";
}

public partial class Paint : Module
{
    private PaintColor _color;
    public PaintColor color
    {
        get => _color;
        set
        {
            _color = value;
            if (_color == PaintColor.White)
                Utils.SetSprite(gameObject, SpritePath.Module.paintWhite);
            else if (_color == PaintColor.Yellow)
                Utils.SetSprite(gameObject, SpritePath.Module.paintYellow);
            else if (_color == PaintColor.Blue)
                Utils.SetSprite(gameObject, SpritePath.Module.paintBlue);
            else if (_color == PaintColor.Red)
                Utils.SetSprite(gameObject, SpritePath.Module.paintRed);

            Utils.SetSpriteSortingOrder(gameObject, 2);
        }
    }
}

public partial class Paint : Module
{
    public Paint(PaintInfo info) : base(info.rc)
    {
        this.color = info.color;
        gameObject.name = Paint.name;
    }

    public override CarrierTodo AcknowledgeModule(ElementCarrier carrier)
    {
        if (carrier.topLeftE != null) carrier.topLeftE.color = this.color;
        if (carrier.topE != null) carrier.topE.color = this.color;
        if (carrier.topRightE != null) carrier.topRightE.color = this.color;
        if (carrier.leftE != null) carrier.leftE.color = this.color;
        if (carrier.midE != null) carrier.midE.color = this.color;
        if (carrier.rightE != null) carrier.rightE.color = this.color;
        if (carrier.botLeftE != null) carrier.botLeftE.color = this.color;
        if (carrier.botE != null) carrier.botE.color = this.color;
        if (carrier.botRightE != null) carrier.botRightE.color = this.color;

        return CarrierTodo.Hide;
    }
}