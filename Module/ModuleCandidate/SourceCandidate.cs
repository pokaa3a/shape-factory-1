using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SourceCandidate : CandidateBase
{
    public SourceCandidate()
    {
        this.name = Source.name;

        Module candidate0 = MakeSourceCandidate(ElementType.Circle);
        Module candidate1 = MakeSourceCandidate(ElementType.Cross);
        Module candidate2 = MakeSourceCandidate(ElementType.Square);
        Module candidate3 = MakeSourceCandidate(ElementType.Triangle);

        candidates.Add(candidate0);
        candidates.Add(candidate1);
        candidates.Add(candidate2);
        candidates.Add(candidate3);

        this.Disable();
    }

    private Module MakeSourceCandidate(ElementType elementType)
    {
        Vector2 xy = Vector2.zero;
        if (elementType == ElementType.Circle)
        {
            xy = new Vector2(-0.9f, verticalPos);
        }
        else if (elementType == ElementType.Cross)
        {
            xy = new Vector2(-0.3f, verticalPos);
        }
        else if (elementType == ElementType.Square)
        {
            xy = new Vector2(0.3f, verticalPos);
        }
        else if (elementType == ElementType.Triangle)
        {
            xy = new Vector2(0.9f, verticalPos);
        }

        ModuleConfig config = ModuleConfig.MakeSourceConfig(
            Direction.Up,
            elementType);
        config.inMap = false;

        Module candidate = new Source(config);
        candidate.xy = xy;

        return candidate;
    }

    public override void ReleaseCandidate(Module module)
    {
        int found = candidates.FindIndex(
            x => x.config.elementType == module.config.elementType);
        candidates[found] = MakeSourceCandidate(module.config.elementType);
    }
}
