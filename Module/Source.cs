using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Source : Module
{
    public const string name = "Source";
    private GameObject directionObject;
    private GameObject elementObject;
}

public partial class Source : Module
{
    public Source(Vector2Int rc, ModuleDirection direction, ElementType type) : base(rc)
    {
        gameObject.name = Source.name;
        Utils.SetSprite(gameObject, SpritePath.Module.source);
        Utils.SetSpriteSortingOrder(gameObject, 2);

        directionObject = new GameObject("Direction");
        Utils.SetParent(directionObject, gameObject);
        switch (direction)
        {
            case ModuleDirection.Up:
                Utils.SetSprite(directionObject, SpritePath.Module.sourceUp); break;
            case ModuleDirection.Down:
                Utils.SetSprite(directionObject, SpritePath.Module.sourceDown); break;
            case ModuleDirection.Left:
                Utils.SetSprite(directionObject, SpritePath.Module.sourceLeft); break;
            case ModuleDirection.Right:
                Utils.SetSprite(directionObject, SpritePath.Module.sourceRight); break;
            default: break;
        }
        Utils.SetSpriteSortingOrder(directionObject, 3);

        elementObject = new GameObject("Element");
        Utils.SetParent(elementObject, gameObject);
        switch (type)
        {
            case ElementType.Circle:
                Utils.SetSprite(elementObject, SpritePath.Element.circle); break;
            case ElementType.Cross:
                Utils.SetSprite(elementObject, SpritePath.Element.cross); break;
            case ElementType.Square:
                Utils.SetSprite(elementObject, SpritePath.Element.square); break;
            case ElementType.Triangle:
                Utils.SetSprite(elementObject, SpritePath.Element.triangle); break;
            default: break;
        }
        Utils.SetSpriteSortingOrder(elementObject, 4);
    }
}