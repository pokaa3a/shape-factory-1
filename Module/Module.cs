using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModuleDirection
{
    Up,
    Down,
    Left,
    Right
}

public partial class Module
{
    public Vector2Int rc;
    public GameObject gameObject;
}

public partial class Module
{
    public Module(Vector2Int rc)
    {
        this.rc = rc;
        gameObject = new GameObject("Module");
        Map.Instance.GetTile(rc).AddModuleToTile(this);
    }
}