using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // return true: keep element carrier alive
    // return false: destroy element carrier
    public virtual bool ElementHits(ElementCarrier elementCarrier)
    {
        return false;
    }
}