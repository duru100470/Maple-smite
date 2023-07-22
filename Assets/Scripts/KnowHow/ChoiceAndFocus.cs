using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceAndFocus : IKnowHowBuff
{
    public PlayerModel ApplyEffect(PlayerModel playerModel)
    {
        playerModel.SkillDict[KeyType.Throw] = (p) =>
        {
            if (p.CurState != PlayerState.Idle || p.PlayerModel.Modified().IsThrowCooldown)
                return;

            p.StartCoroutine(p.DoAttackTree());
            p.StartCoroutine(p.SetThrowCooldown());
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
