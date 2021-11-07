using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class OverlayCandidate : CandidateBase
{
    public OverlayCandidate()
    {
        // ModuleConfig config = new ModuleConfig();
        // config.name = Overlay.name;
        // overlayCandidate = new ModuleCandidate(config);
        // overlayCandidate.xy = new Vector2(0, verticalPos);

        // this.Disable();
    }
}

public partial class OverlayCandidate : CandidateBase
{
    public override void ReleaseCandidate()
    {

    }
}
