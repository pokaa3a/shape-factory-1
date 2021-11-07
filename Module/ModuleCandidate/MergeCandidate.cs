using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MergeCandidate : CandidateBase
{
    public MergeCandidate()
    {
        // ModuleConfig config = new ModuleConfig();
        // config.name = Merge.name;
        // mergeCandidate = new ModuleCandidate(config);
        // mergeCandidate.xy = new Vector2(0, verticalPos);

        // this.Disable();
    }
}

public partial class MergeCandidate : CandidateBase
{
    public override void ReleaseCandidate()
    {

    }
}
