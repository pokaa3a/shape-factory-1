using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GrowCandidate : CandidateBase
{
    public GrowCandidate()
    {
        this.name = Grow.name;

        Module candidate = MakeGrowCandidate();
        candidates.Add(candidate);

        this.Disable();
    }

    private Module MakeGrowCandidate()
    {
        ModuleConfig config = ModuleConfig.MakeGrowConfig(Direction.Up);
        config.inMap = false;

        Module candidate = new Grow(config);
        candidate.xy = new Vector2(0, verticalPos);

        return candidate;
    }
}

public partial class GrowCandidate : CandidateBase
{
    public override void ReleaseCandidate(Module module)
    {
        if (candidates[0] == module) candidates[0] = MakeGrowCandidate();
    }
}
