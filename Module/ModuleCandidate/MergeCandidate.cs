using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MergeCandidate : CandidateBase
{
    public MergeCandidate()
    {
        this.name = Merge.name;

        Module candidate = MakeMergeCandidate();
        candidates.Add(candidate);

        this.Disable();
    }

    private Module MakeMergeCandidate()
    {
        ModuleConfig config = ModuleConfig.MakeMergeConfig(
            Direction.Up);
        config.inMap = false;

        Module candidate = new Merge(config);
        candidate.xy = new Vector2(0, verticalPos);

        return candidate;
    }
}

public partial class MergeCandidate : CandidateBase
{
    public override void ReleaseCandidate(Module module)
    {
        candidates[0] = MakeMergeCandidate();
    }
}
