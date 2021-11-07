using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PaintCandidate : CandidateBase
{
    public PaintCandidate()
    {
        // // Blue
        // ModuleConfig config0 = new ModuleConfig();
        // config0.name = Paint.name;
        // config0.paintColor = PaintColor.Blue;
        // paintCandidate0 = new ModuleCandidate(config0);
        // paintCandidate0.xy = new Vector2(-0.9f, verticalPos);

        // // Red
        // ModuleConfig config1 = new ModuleConfig();
        // config1.name = Paint.name;
        // config1.paintColor = PaintColor.Red;
        // paintCandidate1 = new ModuleCandidate(config1);
        // paintCandidate1.xy = new Vector2(-0.3f, verticalPos);

        // // Yellow
        // ModuleConfig config2 = new ModuleConfig();
        // config2.name = Paint.name;
        // config2.paintColor = PaintColor.Yellow;
        // paintCandidate2 = new ModuleCandidate(config2);
        // paintCandidate2.xy = new Vector2(0.3f, verticalPos);

        // // White
        // ModuleConfig config3 = new ModuleConfig();
        // config3.name = Paint.name;
        // config3.paintColor = PaintColor.White;
        // paintCandidate3 = new ModuleCandidate(config3);
        // paintCandidate3.xy = new Vector2(0.9f, verticalPos);

        // this.Disable();
    }
}

public partial class PaintCandidate : CandidateBase
{
    public override void ReleaseCandidate()
    {

    }
}
