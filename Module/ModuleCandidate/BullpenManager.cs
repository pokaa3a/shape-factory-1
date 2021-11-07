using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BullpenManager
{
    const float verticalPos = -2f;
    public CandidateBase currentCandidate;
    public Dictionary<string, CandidateBase> nameToCandiate =
        new Dictionary<string, CandidateBase>();
}

public partial class BullpenManager
{
    private static BullpenManager _instance;
    public static BullpenManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BullpenManager();
            }
            return _instance;
        }
    }
}

public partial class BullpenManager
{
    private BullpenManager() { }

    public void Initialize()
    {
        // Source
        SourceCandidate source = new SourceCandidate();
        nameToCandiate.Add(Source.name, source);

        // Turn
        TurnCandidate turn = new TurnCandidate();
        nameToCandiate.Add(Turn.name, turn);

        // Merge
        MergeCandidate merge = new MergeCandidate();
        nameToCandiate.Add(Merge.name, merge);

        // Split
        SplitCandidate split = new SplitCandidate();
        nameToCandiate.Add(Split.name, split);

        // Paint
        PaintCandidate paint = new PaintCandidate();
        nameToCandiate.Add(Paint.name, paint);

        // Rotate
        RotateCandidate rotate = new RotateCandidate();
        nameToCandiate.Add(Rotate.name, rotate);

        // Overlay
        OverlayCandidate overlay = new OverlayCandidate();
        nameToCandiate.Add(Overlay.name, overlay);

        // Grow
        GrowCandidate grow = new GrowCandidate();
        nameToCandiate.Add(Grow.name, grow);
    }

    public void ReleaseModule(Module module)
    {
        if (currentCandidate != null)
        {
            currentCandidate.ReleaseCandidate(module);
        }
    }

    public void SelectModule(string moduleName)
    {
        if (currentCandidate == null)
        {
            currentCandidate = nameToCandiate[moduleName];
            currentCandidate.Enable();
        }
        else if (currentCandidate.name != moduleName)
        {
            currentCandidate.Disable();
            currentCandidate = nameToCandiate[moduleName];
            currentCandidate.Enable();
        }
        else    // currentCandidate.name == moduleName
        {
            currentCandidate.Disable();
            currentCandidate = null;
        }
    }
}