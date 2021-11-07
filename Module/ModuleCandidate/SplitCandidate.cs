using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SplitCandidate : CandidateBase
{
    public SplitCandidate()
    {
        this.name = Split.name;

        Module candidate = MakeSplitCandidate();
        candidates.Add(candidate);

        this.Disable();
    }

    private Module MakeSplitCandidate()
    {
        ModuleConfig config = ModuleConfig.MakeSplitConfig(Direction.Up);
        config.inMap = false;

        Module candidate = new Split(config);
        candidate.xy = new Vector2(0, verticalPos);

        return candidate;
    }
}

public partial class SplitCandidate : CandidateBase
{
    public override void ReleaseCandidate(Module module)
    {
        candidates[0] = MakeSplitCandidate();
    }
}