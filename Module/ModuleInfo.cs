#if false

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModuleInfo
{
    public string name;
    public Vector2Int rc;
    public ElementType elementType;
    public Direction direction;
    public TurnPose turnPose;
    public RotationPose rotationPose;
    public PaintColor paintColor;
}

[System.Serializable]
public class SourceInfo : ModuleInfo
{
    // public ElementType elementType;
    public Direction direction;
}

[System.Serializable]
public class TargetInfo : ModuleInfo
{
    public List<Target.ElementConfig> elements = new List<Target.ElementConfig>();
}

[System.Serializable]
public class TurnInfo : ModuleInfo
{
    public TurnPose pose;
    public Direction direction;
}

[System.Serializable]
public class MergeInfo : ModuleInfo
{
    public Direction direction;
}

[System.Serializable]
public class SplitInfo : ModuleInfo
{
    public Direction direction;
}

[System.Serializable]
public class RotateInfo : ModuleInfo
{
    public RotatePose pose;
}

[System.Serializable]
public class PaintInfo : ModuleInfo
{
    public PaintColor color;
}

[System.Serializable]
public class GrowInfo : ModuleInfo
{
    public Direction direction;
}

#endif