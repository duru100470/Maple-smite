using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrowing : IKnowHowBuff
{
    public PlayerModel ApplyEffect(PlayerModel playerModel)
    {
        playerModel.ThrowStunDuration *= 2;
        return playerModel;
    }

    public string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public KnowHowType GetKnowHowType()
        => KnowHowType.LEGENDARY_F;

    public string GetName()
    {
        throw new System.NotImplementedException();
    }
}
