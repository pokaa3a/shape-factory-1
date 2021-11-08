using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelUtils
{
    public static Level MakeLevel()
    {
        Level level = new Level();

        // (0, 0): Source, up, circle
        ModuleConfig config_0 = ModuleConfig.MakeSourceConfig(
            Direction.Up,
            ElementType.Circle
        );
        config_0.inMap = true;
        config_0.rc = new Vector2Int(0, 0);
        config_0.sourceSpawns = true;
        level.modules.Add(config_0);

        // (0, 1): Source, right, square
        ModuleConfig config_1 = ModuleConfig.MakeSourceConfig(
            Direction.Right,
            ElementType.Square
        );
        config_1.inMap = true;
        config_1.rc = new Vector2Int(0, 1);
        config_1.sourceSpawns = true;
        level.modules.Add(config_1);

        // (2, 0): Turn
        ModuleConfig config_2 = ModuleConfig.MakeTurnConfig(
            Direction.Right,
            TurnPose.TurnRight
        );
        config_2.inMap = true;
        config_2.rc = new Vector2Int(2, 0);
        level.modules.Add(config_2);

        // (0, 3): Turn
        ModuleConfig config_3 = ModuleConfig.MakeTurnConfig(
            Direction.Up,
            TurnPose.TurnLeft
        );
        config_3.inMap = true;
        config_3.rc = new Vector2Int(0, 3);
        level.modules.Add(config_3);

        // (2, 3): Merge
        ModuleConfig config_4 = ModuleConfig.MakeMergeConfig(
            Direction.Up
        );
        config_4.inMap = true;
        config_4.rc = new Vector2Int(2, 3);
        level.modules.Add(config_4);

        // (4, 3): Rotate
        ModuleConfig config_5 = ModuleConfig.MakeRotateConfig(
            RotatePose.ClockWise
        );
        config_5.inMap = true;
        config_5.rc = new Vector2Int(4, 3);
        level.modules.Add(config_5);

        // (6, 3): Split
        ModuleConfig config_6 = ModuleConfig.MakeSplitConfig(
            Direction.Up
        );
        config_6.inMap = true;
        config_6.rc = new Vector2Int(6, 3);
        level.modules.Add(config_6);

        // (6, 2): Paint
        ModuleConfig config_7 = ModuleConfig.MakePaintConfig(
            PaintColor.Yellow
        );
        config_7.inMap = true;
        config_7.rc = new Vector2Int(6, 2);
        level.modules.Add(config_7);

        // (6, 1): Grow
        ModuleConfig config_8 = ModuleConfig.MakeGrowConfig(
            Direction.Left
        );
        config_8.inMap = true;
        config_8.rc = new Vector2Int(6, 1);
        level.modules.Add(config_8);

        // SaveLevel(level, $"{Application.dataPath}/Resources/Levels/level_0.json");
        return level;
    }

    public static Level LoadLevel(int number)
    {
        Debug.Log($"[LevelUtils] Load level {number}");
        string inputPath = $"{Application.dataPath}/Resources/Levels/level_{number}.json";
        return LoadLevel(inputPath);
    }

    public static Level LoadLevel(string input)
    {
        if (!File.Exists(input)) return new Level();

        string jsonStr = System.IO.File.ReadAllText(input);
        return JsonUtility.FromJson<Level>(jsonStr);
    }

    public static void SaveLevel(Level level, string output)
    {
        string jsonStr = JsonUtility.ToJson(level);
        System.IO.File.WriteAllText(output, jsonStr);
    }
}
