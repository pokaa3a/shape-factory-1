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
    public static int rows = 10;
    public static int cols = 8;
    public const float margin = 0.05f;
    public const float verticalOffset = 1.2f;
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

        float tileSize = (screenW * (1 - margin)) / (float)Map.cols;
        Map.tileWH = new Vector2(tileSize, tileSize);

        Map.bottomLeftTileXy =
            new Vector2(-(Map.cols - 1) / 2f, -(Map.rows - 1) / 2f) * tileSize +
            new Vector2(0, verticalOffset);

        // Create tiles
        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < cols; ++c)
            {
                Tile tile = new Tile(new Vector2Int(r, c));
                tiles.Add(tile);
            }
        }
    }

    public void CreateMap()
    {
        // MapConfig config = MapConfig.LoadMapConfigFromScript();
        // Level level = LevelUtils.LoadLevel($"{Application.dataPath}/Resources/Levels/level_0.json");
        Level level = LevelUtils.MakeLevel();
        MakeModulesByConfig(level.modules);
    }

    public void CleanMap()
    {
        Debug.Log("[Map] CleanMap");
        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < cols; ++c)
            {
                GetTile(new Vector2Int(r, c)).DestroyModelFromTile();
            }
        }
    }

    public void CreateMapFromLevel(Level level)
    {
        CleanMap();
        MakeModulesByConfig(level.modules);
    }

    private void MakeModulesByConfig(List<ModuleConfig> configs)
    {
        foreach (ModuleConfig config in configs)
        {
            switch (config.name)
            {
                case Source.name:
                    Source source = new Source(config);
                    break;
                case Target.name:
                    Target target = new Target(config);
                    break;
                case Merge.name:
                    Merge merge = new Merge(config);
                    break;
                case Turn.name:
                    Turn turn = new Turn(config);
                    break;
                case Rotate.name:
                    Rotate rotate = new Rotate(config);
                    break;
                case Split.name:
                    Split split = new Split(config);
                    break;
                case Paint.name:
                    Paint paint = new Paint(config);
                    break;
                case Grow.name:
                    Grow grow = new Grow(config);
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
        float x = bottomLeftTileXy.x + (float)rc.y * tileWH.x;
        float y = bottomLeftTileXy.y + (float)rc.x * tileWH.y;
        return new Vector2(x, y);
    }

    public static Vector2Int XYtoRC(Vector2 xy)
    {
        int c = (int)((xy.x - bottomLeftTileXy.x + tileWH.x * 0.5f) / tileWH.x);
        int r = (int)((xy.y - bottomLeftTileXy.y + tileWH.y * 0.5f) / tileWH.y);
        return new Vector2Int(r, c);
    }

    public static Vector2 XYtoUVCentered(Vector2 xy)
    {
        Camera mainCam = Camera.main;
        float v = xy.y * Screen.height / 2f / mainCam.orthographicSize;
        float u = xy.x * Screen.width / 2f / (mainCam.orthographicSize * mainCam.aspect);
        return new Vector2(u, v);
    }

    public static Vector2 UVtoXYCentered(Vector2 uv)
    {
        Camera mainCam = Camera.main;
        float y = uv.y * mainCam.orthographicSize / (Screen.height / 2f);
        float x = uv.x * mainCam.orthographicSize * mainCam.aspect / (Screen.width / 2f);
        return new Vector2(x, y);
    }

    public static Vector2 XYtoUV(Vector2 xy)
    {
        Vector2 uv = XYtoUVCentered(xy);
        uv += new Vector2(Screen.width / 2f, Screen.height / 2f);
        return uv;
    }

    public static Vector2 UVtoXY(Vector2 uv)
    {
        return UVtoXYCentered(uv - new Vector2(Screen.width / 2f, Screen.height / 2f));
    }

    public static Vector2 FirstFrameXy(Vector2Int rc, Direction toward)
    {
        Vector2 xy = RCtoXY(rc);
        float movement = Time.deltaTime * Map.tileWH.y / ElementCarrier.timePerTile;
        if (toward == Direction.Up)
            return new Vector2(xy.x, xy.y - tileWH.y / 2f + movement);
        else if (toward == Direction.Down)
            return new Vector2(xy.x, xy.y + tileWH.y / 2f - movement);
        else if (toward == Direction.Left)
            return new Vector2(xy.x + tileWH.x / 2f - movement, xy.y);
        else // toward == Direction.Right
            return new Vector2(xy.x - tileWH.x / 2f + movement, xy.y);
    }
}