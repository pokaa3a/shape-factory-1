using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MapConfig
{
    public List<ModuleInfo> modules = new List<ModuleInfo>();
}

public partial class MapConfig
{
    public MapConfig() { }

    public static MapConfig LoadMapConfigFromScript()
    {
        MapConfig config = new MapConfig();

        // (0, 0): Source, up, circle
        SourceInfo info0 = new SourceInfo();
        info0.name = Source.name;
        info0.rc = new Vector2Int(0, 0);
        info0.direction = Direction.Up;
        info0.elementType = ElementType.Circle;
        config.modules.Add((ModuleInfo)info0);

        // (0, 1): Source, right, square
        SourceInfo info1 = new SourceInfo();
        info1.name = Source.name;
        info1.rc = new Vector2Int(0, 1);
        info1.direction = Direction.Right;
        info1.elementType = ElementType.Square;
        config.modules.Add((ModuleInfo)info1);

        // (5, 5): Target
        TargetInfo info2 = new TargetInfo();
        info2.name = Target.name;
        info2.rc = new Vector2Int(4, 4);

        Target.ElementConfig ec1 = new Target.ElementConfig(Vector2.up, ElementType.Triangle);
        info2.elements.Add(ec1);
        Target.ElementConfig ec2 = new Target.ElementConfig(Vector2.down, ElementType.Square);
        info2.elements.Add(ec2);

        // config.modules.Add((ModuleInfo)info2);

        // (4, 0): Mirror
        MirrorInfo info3 = new MirrorInfo();
        info3.rc = new Vector2Int(4, 0);
        info3.pose = MirrorPose.TopRightBotLeft;
        config.modules.Add((ModuleInfo)info3);

        // (0, 3): Mirror
        MirrorInfo info4 = new MirrorInfo();
        info4.rc = new Vector2Int(0, 3);
        info4.pose = MirrorPose.TopRightBotLeft;
        config.modules.Add((ModuleInfo)info4);

        // (4, 3): Merge
        MergeInfo info5 = new MergeInfo();
        info5.rc = new Vector2Int(4, 3);
        info5.direction = Direction.Up;
        config.modules.Add((ModuleInfo)info5);

        return config;
    }
}
