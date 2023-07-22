using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassiveThrowing : IKnowHowBuff
{
    public PlayerModel ApplyEffect(PlayerModel playerModel)
    {
        playerModel.ThrowStunDuration *= 1.5f;
        playerModel.ThrowCooldown *= 2f;
        return playerModel;
    }

    public string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public KnowHowType GetKnowHowType()
        => KnowHowType.EPIC_D;

    public string GetName()
    {
        throw new System.NotImplementedException();
    }
}
