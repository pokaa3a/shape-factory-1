using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Merge : Module
{
    public const string name = "Merge";
    public Direction direction;
}

public partial class Merge : Module
{

    public Merge(MergeInfo info) : base(info.rc)
    {
        this.direction = info.direction;
        gameObject.name = Merge.name;

        Utils.SetSprite(gameObject, SpritePath.Module.merge);
        Utils.SetSpriteSortingOrder(gameObject, 2);
    }
}
