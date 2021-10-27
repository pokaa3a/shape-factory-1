using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Target : Module
{
    public const string name = "Target";
    public const float patternStep = 0.09f;

    private List<ElementConfig> elementConfigs;
}

public partial class Target : Module
{
    public class ElementConfig
    {
        public Vector2 xy;
        public ElementType type;
        public ElementConfig(Vector2 xy, ElementType type)
        {
            this.xy = xy;
            this.type = type;
        }
    }
}

public partial class Target : Module
{
    public Target(TargetInfo info) : base(info.rc)
    {
        gameObject.name = Target.name;
        Utils.SetSprite(gameObject, SpritePath.Module.target);
        Utils.SetSpriteSortingOrder(gameObject, 2);

        this.elementConfigs = info.elements;
        MakeTargetPattern(info.elements);
    }

    private void MakeTargetPattern(List<ElementConfig> elementConfigs)
    {
        foreach (ElementConfig config in elementConfigs)
        {
            GameObject obj = new GameObject("TargetElement");

            switch (config.type)
            {
                case ElementType.Circle:
                    Utils.SetSprite(obj, SpritePath.Element.Circle.white); break;
                case ElementType.Cross:
                    Utils.SetSprite(obj, SpritePath.Element.Cross.white); break;
                case ElementType.Square:
                    Utils.SetSprite(obj, SpritePath.Element.Square.white); break;
                case ElementType.Triangle:
                    Utils.SetSprite(obj, SpritePath.Element.Triangle.white); break;
                default: break;
            }
            Utils.SetParent(obj, this.gameObject);
            Utils.SetSpriteSortingOrder(obj, 3);
            obj.transform.localPosition = config.xy * patternStep;
        }
    }
}