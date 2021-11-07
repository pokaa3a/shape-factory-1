using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GrowCandidate : CandidateBase
{
    public GrowCandidate()
    {
        // ModuleConfig config = new ModuleConfig();
        // config = Grow.name;
        // growCandidate = new ModuleCandidate(config);
        // growCandidate.xy = new Vector2(0, verticalPos);

        this.Disable();
    }
}

public partial class GrowCandidate : CandidateBase
{
    public override void ReleaseCandidate()
    {

    }
}
