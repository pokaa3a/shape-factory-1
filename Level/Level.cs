using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int rows = 9;
    public int cols = 7;
    public int id = 0;
    public List<ModuleConfig> modules = new List<ModuleConfig>();
}
