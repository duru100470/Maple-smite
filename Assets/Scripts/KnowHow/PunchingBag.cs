using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingBag : IKnowHowBuff
{
    public PlayerModel ApplyEffect(PlayerModel playerModel)
    {
        playerModel.SkillDict[KeyType.Jump] = (p) =>
        {
            if (p.CurState == PlayerState.Act || p.PlayerModel.Modified().IsJumpCooldown)
                return;

            p.DoCleans();
            p.StartCoroutine(p.SetJumpCooldown());
        };
        return playerModel;
    }

    public string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public KnowHowType GetKnowHowType()
        => KnowHowType.EPIC_E;

    public string GetName()
    {
        throw new System.NotImplementedException();
    }
}
