using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// uv: Screen space (1170 x 2532)
// xy: World space (2.31*2 x 5*2)
// rc: Row Column

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public partial class Map
{
    public const int rows = 8;
    public const int cols = 6;
    public static Vector2 screenWH { get; private set; }
    public static Vector2 tileWH { get; private set; }
    public static Vector2 bottomLeftTileXy { get; private set; }
    public List<Tile> tiles = new List<Tile>();
}

public partial class Map
{
    // singleton
    private static Map _instance;
    public static Map Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Map();
            }
            return _instance;
        }
    }
}

public partial class Map
{
    private Map()
    {
        Camera mainCam = Camera.main;
        Assert.IsNotNull(mainCam);

        float screenH = mainCam.orthographicSize * 2f;
        float screenW = screenH * mainCam.aspect;
        Map.screenWH = new Vector2(screenW, screenH);

        float tileSize = (screenW * 0.9f) / Map.cols;
        Map.tileWH = new Vector2(tileSize, tileSize);

        Map.bottomLeftTileXy = new Vector2(
            -(Map.cols - 1) / 2f, -(Map.rows - 1) / 2f) * tileSize;

    }

    public void CreateMap()
    {
        // Create tiles
        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < cols; ++c)
            {
                Tile tile = new Tile(new Vector2Int(r, c));
                tiles.Add(tile);
            }
        }

        MapConfig config = MapConfig.LoadMapConfigFromScript();
        MakeModulesByConfig(config);
    }

    private void MakeModulesByConfig(MapConfig config)
    {
        foreach (ModuleInfo info in config.modules)
        {
            switch (info)
            {
                case SourceInfo sourceInfo:
                    Source source = new Source(sourceInfo);
                    break;
                case TargetInfo targetInfo:
                    Target target = new Target(targetInfo);
                    break;
                case MirrorInfo mirrorInfo:
                    Mirror mirror = new Mirror(mirrorInfo);
                    break;
                default: break;
            }
        }
    }

    public Tile GetTile(Vector2Int rc)
    {
        Assert.IsTrue(InsideMap(rc), $"[GetTile] rc ({rc}) is not inside the map");
        return tiles[rc.x * cols + rc.y];
    }

    public static bool InsideMap(Vector2Int rc)
    {
        return rc.x >= 0 && rc.x < rows && rc.y >= 0 && rc.y < cols;
    }

    public static bool InsideMap(Vector2 xy)
    {
        Vector2 topRightTilexy = new Vector2(
            bottomLeftTileXy.x + tileWH.x * (cols - 1),
            bottomLeftTileXy.y + tileWH.y * (rows - 1));
        return xy.x > bottomLeftTileXy.x - tileWH.x * 0.5f &&
            xy.x < topRightTilexy.x + tileWH.x * 0.5f &&
            xy.y > bottomLeftTileXy.y - tileWH.y * 0.5f &&
            xy.y < topRightTilexy.y + tileWH.y * 0.5f;
    }

    public static Vector2 RCtoXY(Vector2Int rc)
    {
        float x = bottomLeftTileXy.x + rc.y * tileWH.x;
        float y = bottomLeftTileXy.y + rc.x * tileWH.y;
        return new Vector2(x, y);
    }

    public static Vector2Int XYtoRC(Vector2 xy)
    {
        int c = (int)((xy.x - bottomLeftTileXy.x + tileWH.x * 0.5f) / tileWH.x);
        int r = (int)((xy.y - bottomLeftTileXy.y + tileWH.y * 0.5f) / tileWH.y);
        return new Vector2Int(r, c);
    }
}