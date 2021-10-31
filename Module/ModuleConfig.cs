using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModuleConfig
{
    public string name;
    public Vector2Int rc;
    public ElementType elementType;
    public Direction direction;
    public TurnPose turnPose;
    public RotatePose rotatePose;
    public PaintColor paintColor;
    public List<ElementConfig> elementConfigs;

    public ModuleConfig(string name, Vector2Int rc)
    {
        this.name = name;
        this.rc = rc;
    }

    // Source
    public static ModuleConfig MakeSourceConfig(
        Vector2Int rc,
        Direction direction,
        ElementType elementType)
    {
        ModuleConfig config = new ModuleConfig(Source.name, rc);
        config.direction = direction;
        config.elementType = elementType;

        return config;
    }

    // Target
    public static ModuleConfig MakeTargetConfig(
        Vector2Int rc,
        List<ElementConfig> elementConfigs)
    {
        ModuleConfig config = new ModuleConfig(Target.name, rc);
        config.elementConfigs = elementConfigs;

        return config;
    }


    // Turn
    public static ModuleConfig MakeTurnConfig(
        Vector2Int rc,
        Direction direction,
        TurnPose turnPose)
    {
        ModuleConfig config = new ModuleConfig(Turn.name, rc);
        config.direction = direction;
        config.turnPose = turnPose;

        return config;
    }

    // Merge
    public static ModuleConfig MakeMergeConfig(
        Vector2Int rc,
        Direction direction)
    {
        ModuleConfig config = new ModuleConfig(Merge.name, rc);
        config.direction = direction;

        return config;
    }

    // Split
    public static ModuleConfig MakeSplitConfig(
        Vector2Int rc,
        Direction direction)
    {
        ModuleConfig config = new ModuleConfig(Split.name, rc);
        config.direction = direction;

        return config;
    }

    // Rotate
    public static ModuleConfig MakeRotateConfig(
        Vector2Int rc,
        RotatePose rotatePose)
    {
        ModuleConfig config = new ModuleConfig(Rotate.name, rc);
        config.rotatePose = rotatePose;

        return config;
    }

    // Paint
    public static ModuleConfig MakePaintConfig(
        Vector2Int rc,
        PaintColor color)
    {
        ModuleConfig config = new ModuleConfig(Paint.name, rc);
        config.paintColor = color;

        return config;
    }

    // Grow
    public static ModuleConfig MakeGrowConfig(
        Vector2Int rc,
        Direction direction)
    {
        ModuleConfig config = new ModuleConfig(Grow.name, rc);
        config.direction = direction;

        return config;
    }
}
