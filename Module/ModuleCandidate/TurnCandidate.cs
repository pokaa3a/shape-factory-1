using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TurnCandidate : CandidateBase
{
    public TurnCandidate()
    {
        this.name = Turn.name;

        Module candidate0 = MakeTurnCandidate(TurnPose.TurnLeft);
        Module candidate1 = MakeTurnCandidate(TurnPose.TurnRight);

        candidates.Add(candidate0);
        candidates.Add(candidate1);

        this.Disable();
    }

    private Module MakeTurnCandidate(TurnPose turnPose)
    {
        Vector2 xy = new Vector2(0, verticalPos);
        Direction direction;

        if (turnPose == TurnPose.TurnLeft)
        {
            xy = new Vector2(-0.5f, verticalPos);
            direction = Direction.Left;
        }
        else    // turnPose == TurnPose.TurnRight
        {
            xy = new Vector2(0.5f, verticalPos);
            direction = Direction.Right;
        }

        ModuleConfig config = ModuleConfig.MakeTurnConfig(
            direction,
            turnPose);
        config.inMap = false;

        Module candidate = new Turn(config);
        candidate.xy = xy;

        return candidate;
    }

    public override void ReleaseCandidate(Module module)
    {
        int found = candidates.FindIndex(
            x => x.config.turnPose == module.config.turnPose);
        candidates[found] = MakeTurnCandidate(module.config.turnPose);
    }
}