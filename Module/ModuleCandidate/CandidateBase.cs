using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CandidateBase
{
    public string name;
    protected float verticalPos = -2f;
    protected List<Module> candidates = new List<Module>();

    public void Enable()
    {
        for (int i = 0; i < candidates.Count; ++i)
        {
            Module m = candidates[i];
            if (m != null) m.enabled = true;
        }
    }

    public void Disable()
    {
        for (int i = 0; i < candidates.Count; ++i)
        {
            Module m = candidates[i];
            if (m != null) m.enabled = false;
        }
    }

    public abstract void ReleaseCandidate(Module module);
}
