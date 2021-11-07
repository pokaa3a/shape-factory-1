using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SourceCandidate : CandidateBase
{
    public SourceCandidate()
    {
        this.name = Source.name;

        Module candidate0 = AddSourceCandidate(ElementType.Circle);
        Module candidate1 = AddSourceCandidate(ElementType.Cross);
        Module candidate2 = AddSourceCandidate(ElementType.Square);
        Module candidate3 = AddSourceCandidate(ElementType.Triangle);

        candidates.Add(candidate0);
        candidates.Add(candidate1);
        candidates.Add(candidate2);
        candidates.Add(candidate3);

        this.Disable();
    }

    private Module AddSourceCandidate(ElementType elementType)
    {
        ModuleConfig config = new ModuleConfig(Source.name);
        config.inMap = false;
        Vector2 xy = Vector2.zero;
        if (elementType == ElementType.Circle)
        {
            config.elementType = ElementType.Circle;
            xy = new Vector2(-0.9f, verticalPos);
        }
        else if (elementType == ElementType.Cross)
        {
            config.elementType = ElementType.Cross;
            xy = new Vector2(-0.3f, verticalPos);
        }
        else if (elementType == ElementType.Square)
        {
            config.elementType = ElementType.Square;
            xy = new Vector2(0.3f, verticalPos);
        }
        else if (elementType == ElementType.Triangle)
        {
            config.elementType = ElementType.Triangle;
            xy = new Vector2(0.9f, verticalPos);
        }
        Module candidate = new Source(config);
        candidate.xy = xy;

        return candidate;
    }

    public override void ReleaseCandidate()
    {
        for (int i = 0; i < candidates.Count; ++i)
        {
            if (Map.InsideMap(candidates[i].xy))
            {
                // If this module is inside the map, then create a new module
                // in candidates.
                candidates[i] = AddSourceCandidate(candidates[i].config.elementType);
            }
        }
    }
}
