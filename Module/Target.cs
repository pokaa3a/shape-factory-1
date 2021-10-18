using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Target : Module
{
    public const string name = "Target";
}

public partial class Target : Module
{
    public Target(Vector2Int rc) : base(rc)
    {
        gameObject.name = Target.name;
        Utils.SetSprite(gameObject, SpritePath.Module.target);
        Utils.SetSpriteSortingOrder(gameObject, 2);
    }
}