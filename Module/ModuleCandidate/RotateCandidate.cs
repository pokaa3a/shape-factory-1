using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RotateCandidate : CandidateBase
{
    public RotateCandidate()
    {
        this.name = Rotate.name;

        Module candidate0 = MakeRotateCandidate(RotatePose.ClockWise);
        Module candidate1 = MakeRotateCandidate(RotatePose.CounterClockWise);

        candidates.Add(candidate0);
        candidates.Add(candidate1);

        this.Disable();
    }

    private Module MakeRotateCandidate(RotatePose rotatePose)
    {
        Vector2 xy = Vector2.zero;
        if (rotatePose == RotatePose.CounterClockWise)
        {
            xy = new Vector2(-0.5f, verticalPos);
        }
        else    // rotatePose == RotatePose.CounterClockWise
        {
            xy = new Vector2(0.5f, verticalPos);
        }

        ModuleConfig config = ModuleConfig.MakeRotateConfig(rotatePose);
        config.inMap = false;

        Module candidate = new Rotate(config);
        candidate.xy = xy;

        return candidate;
    }
}

public partial class RotateCandidate : CandidateBase
{
    public override void ReleaseCandidate(Module module)
    {
        int found = candidates.FindIndex(x => x == module);
        if (found >= 0)
            candidates[found] = MakeRotateCandidate(module.config.rotatePose);
    }
}
