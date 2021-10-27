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

        // (2, 0): Turn
        TurnInfo info3 = new TurnInfo();
        info3.rc = new Vector2Int(2, 0);
        info3.pose = TurnPose.TurnRight;
        info3.direction = Direction.Right;
        config.modules.Add((ModuleInfo)info3);

        // (0, 3): Turn
        TurnInfo info4 = new TurnInfo();
        info4.rc = new Vector2Int(0, 3);
        info4.pose = TurnPose.TurnLeft;
        info4.direction = Direction.Up;
        config.modules.Add((ModuleInfo)info4);

        // (2, 3): Merge
        MergeInfo info5 = new MergeInfo();
        info5.rc = new Vector2Int(2, 3);
        info5.direction = Direction.Up;
        config.modules.Add((ModuleInfo)info5);

        // (4, 3): Rotate
        RotateInfo info6 = new RotateInfo();
        info6.rc = new Vector2Int(4, 3);
        info6.pose = RotatePose.ClockWise;
        config.modules.Add((ModuleInfo)info6);

        // (6, 3): Split
        SplitInfo info7 = new SplitInfo();
        info7.rc = new Vector2Int(6, 3);
        info7.direction = Direction.Up;
        config.modules.Add((ModuleInfo)info7);

        // (6, 1): Paint
        PaintInfo info8 = new PaintInfo();
        info8.rc = new Vector2Int(6, 1);
        info8.color = PaintColor.Yellow;
        config.modules.Add((ModuleInfo)info8);

        // (6, 5): Paint
        PaintInfo info9 = new PaintInfo();
        info9.rc = new Vector2Int(6, 5);
        info9.color = PaintColor.Blue;
        config.modules.Add((ModuleInfo)info9);

        // (5, 3): Paint
        PaintInfo info10 = new PaintInfo();
        info10.rc = new Vector2Int(5, 3);
        info10.color = PaintColor.Red;
        config.modules.Add((ModuleInfo)info10);

        return config;
    }
}
