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
    public ModuleDirection direction;
}

public class TargetInfo : ModuleInfo
{

}