using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModuleConfig
{
    public string name;
    public bool inMap = true;
    public Vector2Int rc;
    public ElementType elementType;
    public bool sourceSpawns = false;
    public Direction direction;
    public TurnPose turnPose;
    public RotatePose rotatePose;
    public PaintColor paintColor;
    public List<ElementConfig> elementConfigs;

    public ModuleConfig(string name)
    {
        this.name = name;
    }

    // Source
    public static ModuleConfig MakeSourceConfig(
        Direction direction,
        ElementType elementType)
    {
        ModuleConfig config = new ModuleConfig(Source.name);
        config.direction = direction;
        config.elementType = elementType;

        return config;
    }

    // Target
    public static ModuleConfig MakeTargetConfig(
        List<ElementConfig> elementConfigs)
    {
        ModuleConfig config = new ModuleConfig(Target.name);
        config.elementConfigs = elementConfigs;

        return config;
    }


    // Turn
    public static ModuleConfig MakeTurnConfig(
        Direction direction,
        TurnPose turnPose)
    {
        ModuleConfig config = new ModuleConfig(Turn.name);
        config.direction = direction;
        config.turnPose = turnPose;

        return config;
    }

    // Merge
    public static ModuleConfig MakeMergeConfig(
        Direction direction)
    {
        ModuleConfig config = new ModuleConfig(Merge.name);
        config.direction = direction;

        return config;
    }

    // Split
    public static ModuleConfig MakeSplitConfig(
        Direction direction)
    {
        ModuleConfig config = new ModuleConfig(Split.name);
        config.direction = direction;

        return config;
    }

    // Rotate
    public static ModuleConfig MakeRotateConfig(
        RotatePose rotatePose)
    {
        ModuleConfig config = new ModuleConfig(Rotate.name);
        config.rotatePose = rotatePose;

        return config;
    }

    // Paint
    public static ModuleConfig MakePaintConfig(
        PaintColor color)
    {
        ModuleConfig config = new ModuleConfig(Paint.name);
        config.paintColor = color;

        return config;
    }

    // Grow
    public static ModuleConfig MakeGrowConfig(
        Direction direction)
    {
        ModuleConfig config = new ModuleConfig(Grow.name);
        config.direction = direction;

        return config;
    }

    // Overlay
    public static ModuleConfig MakeOverlayConfig(
        Direction direction)
    {
        ModuleConfig config = new ModuleConfig(Overlay.name);
        config.direction = direction;

        return config;
    }
}
