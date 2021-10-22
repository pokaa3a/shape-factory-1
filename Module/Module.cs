using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Module
{
    public Vector2Int rc;
    public Vector2 xy { get => Map.RCtoXY(this.rc); }
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

    public virtual CarrierTodo AcknowledgeModule(ElementCarrier carrier)
    {
        return CarrierTodo.Destroy;
    }
}