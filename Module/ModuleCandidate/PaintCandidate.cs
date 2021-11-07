using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PaintCandidate : CandidateBase
{
    public PaintCandidate()
    {
        this.name = Paint.name;

        Module candidate0 = MakePaintCandidata(PaintColor.Blue);
        Module candidate1 = MakePaintCandidata(PaintColor.Red);
        Module candidate2 = MakePaintCandidata(PaintColor.Yellow);
        Module candidate3 = MakePaintCandidata(PaintColor.White);

        candidates.Add(candidate0);
        candidates.Add(candidate1);
        candidates.Add(candidate2);
        candidates.Add(candidate3);

        this.Disable();
    }

    private Module MakePaintCandidata(PaintColor color)
    {
        Vector2 xy = Vector2.zero;
        if (color == PaintColor.Blue)
        {
            xy = new Vector2(-0.9f, verticalPos);
        }
        else if (color == PaintColor.Red)
        {
            xy = new Vector2(-0.3f, verticalPos);
        }
        else if (color == PaintColor.Yellow)
        {
            xy = new Vector2(0.3f, verticalPos);
        }
        else    // white
        {
            xy = new Vector2(0.9f, verticalPos);
        }

        ModuleConfig config = ModuleConfig.MakePaintConfig(color);
        config.inMap = false;

        Module candidate = new Paint(config);
        candidate.xy = xy;

        return candidate;
    }
}

public partial class PaintCandidate : CandidateBase
{
    public override void ReleaseCandidate(Module module)
    {
        int found = candidates.FindIndex(
            x => x.config.paintColor == module.config.paintColor);
        candidates[found] = MakePaintCandidata(module.config.paintColor);
    }
}
