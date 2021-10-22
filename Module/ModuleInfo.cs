using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleInfo
{
    public string name;
    public Vector2Int rc;
}

public class SourceInfo : ModuleInfo
{
    public ElementType elementType;
    public Direction direction;
}

public class TargetInfo : ModuleInfo
{
    public List<Target.ElementConfig> elements = new List<Target.ElementConfig>();
}

public class MirrorInfo : ModuleInfo
{
    public MirrorPose pose;
}

public class MergeInfo : ModuleInfo
{
    public Direction direction;
}

public class SplitInfo : ModuleInfo
{
    public Direction direction;
}