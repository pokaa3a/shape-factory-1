using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tile
{
    public Vector2Int rc { get; private set; }
    public GameObject gameObject;
    public static GameObject mapObject;
    public Module module;
}

public partial class Tile
{
    public Tile(Vector2Int rc)
    {
        this.rc = rc;
        gameObject = new GameObject($"tile_{rc.x}_{rc.y}");

        if (mapObject == null)
        {
            mapObject = new GameObject("Map");
            mapObject.transform.position = Vector2.zero;
        }
        gameObject.transform.SetParent(mapObject.transform);
        gameObject.transform.localPosition = Map.RCtoXY(rc);

        Utils.SetSprite(gameObject, SpritePath.Map.tile);
        Utils.SetSpriteSortingOrder(gameObject, 1);
        Utils.SetScale(gameObject, Map.tileWH);
    }

    public void AddModuleToTile(Module module)
    {
        this.module = module;
        Utils.SetParent(module.gameObject, gameObject);
    }
}