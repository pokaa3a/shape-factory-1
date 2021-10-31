using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUtils
{
    public static void MakeLevelAndSave()
    {
        Level level = new Level();

        // (0, 0): Source, up, circle
        ModuleConfig config_0 = ModuleConfig.MakeSourceConfig(
            new Vector2Int(0, 0),
            Direction.Up,
            ElementType.Circle
        );
        level.modules.Add(config_0);

        // (0, 1): Source, right, square
        ModuleConfig config_1 = ModuleConfig.MakeSourceConfig(
            new Vector2Int(0, 1),
            Direction.Right,
            ElementType.Square
        );
        level.modules.Add(config_1);

        // (2, 0): Turn
        ModuleConfig config_2 = ModuleConfig.MakeTurnConfig(
            new Vector2Int(2, 0),
            Direction.Right,
            TurnPose.TurnRight
        );
        level.modules.Add(config_2);

        // (0, 3): Turn
        ModuleConfig config_3 = ModuleConfig.MakeTurnConfig(
            new Vector2Int(0, 3),
            Direction.Up,
            TurnPose.TurnLeft
        );
        level.modules.Add(config_3);

        // (2, 3): Merge
        ModuleConfig config_4 = ModuleConfig.MakeMergeConfig(
            new Vector2Int(2, 3),
            Direction.Up
        );
        level.modules.Add(config_4);

        // (4, 3): Rotate
        ModuleConfig config_5 = ModuleConfig.MakeRotateConfig(
            new Vector2Int(4, 3),
            RotatePose.ClockWise
        );
        level.modules.Add(config_5);

        // (6, 3): Split
        ModuleConfig config_6 = ModuleConfig.MakeSplitConfig(
            new Vector2Int(6, 3),
            Direction.Up
        );
        level.modules.Add(config_6);

        // (6, 2): Paint
        ModuleConfig config_7 = ModuleConfig.MakePaintConfig(
            new Vector2Int(6, 2),
            PaintColor.Yellow
        );
        level.modules.Add(config_7);

        // (6, 1): Grow
        ModuleConfig config_8 = ModuleConfig.MakeGrowConfig(
            new Vector2Int(6, 1),
            Direction.Left
        );
        level.modules.Add(config_8);

        SaveLevel(level, $"{Application.dataPath}/Resources/Levels/level_0.json");
    }

    public static Level LoadLevel(string input)
    {
        string jsonStr = System.IO.File.ReadAllText(input);
        return JsonUtility.FromJson<Level>(jsonStr);
    }

    public static void SaveLevel(Level level, string output)
    {
        string jsonStr = JsonUtility.ToJson(level);
        System.IO.File.WriteAllText(output, jsonStr);
    }
}
