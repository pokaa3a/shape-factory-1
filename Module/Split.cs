using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Split : Module
{
    public const string name = "Split";
    public Direction direction;
}

public partial class Split : Module
{
    public Split(SplitInfo info) : base(info.rc)
    {
        this.direction = info.direction;
        gameObject.name = Split.name;

        Utils.SetSprite(gameObject, SpritePath.Module.split);
        Utils.SetSpriteSortingOrder(gameObject, 2);
    }
}