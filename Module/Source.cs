using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Source : Module
{
    public const string name = "Source";
    public const float spawnPeriod = 2f;    // sec
    public ElementType elementType;
    public Direction direction;
    private GameObject directionObject;
    private GameObject elementObject;
    private Component component;
}

public partial class Source : Module
{
    public partial class Component : MonoBehaviour
    {
        private Source source;
        private float spawnPeriod;
        private float nextSpawnTime;

        public void Initialize(Source source, float spawnPeriod)
        {
            this.source = source;
            this.spawnPeriod = spawnPeriod;
            this.nextSpawnTime = Time.time;
        }

        void Update()
        {
            if (Time.time >= nextSpawnTime)
            {
                source.Spawn();
                nextSpawnTime = Time.time + spawnPeriod;
            }
        }
    }
}

public partial class Source : Module
{
    public Source(SourceInfo info) : base(info.rc)
    {
        this.direction = info.direction;
        this.elementType = info.elementType;

        gameObject.name = Source.name;
        component = gameObject.AddComponent<Component>();
        component.Initialize(this, Source.spawnPeriod);

        Utils.SetSprite(gameObject, SpritePath.Module.source);
        Utils.SetSpriteSortingOrder(gameObject, 2);

        directionObject = new GameObject("Direction");
        Utils.SetParent(directionObject, gameObject);
        switch (info.direction)
        {
            case Direction.Up:
                Utils.SetSprite(directionObject, SpritePath.Module.sourceUp); break;
            case Direction.Down:
                Utils.SetSprite(directionObject, SpritePath.Module.sourceDown); break;
            case Direction.Left:
                Utils.SetSprite(directionObject, SpritePath.Module.sourceLeft); break;
            case Direction.Right:
                Utils.SetSprite(directionObject, SpritePath.Module.sourceRight); break;
            default: break;
        }
        Utils.SetSpriteSortingOrder(directionObject, 3);

        elementObject = new GameObject("Element");
        Utils.SetParent(elementObject, gameObject);
        switch (info.elementType)
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

    public void Spawn()
    {
        Element e = new Element(elementType, rc, direction);
    }
}