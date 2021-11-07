using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class OverlayCandidate : CandidateBase
{
    public OverlayCandidate()
    {
        this.name = Overlay.name;

        Module candidate = MakeOverlayCandidate();
        candidates.Add(candidate);

        this.Disable();
    }

    private Module MakeOverlayCandidate()
    {
        ModuleConfig config = ModuleConfig.MakeOverlayConfig();
        config.inMap = false;

        Module candidate = new Overlay(config);
        candidate.xy = new Vector2(0, verticalPos);

        return candidate;
    }
}

public partial class OverlayCandidate : CandidateBase
{
    public override void ReleaseCandidate(Module module)
    {
        if (candidates[0] == module) candidates[0] = MakeOverlayCandidate();
    }
}
