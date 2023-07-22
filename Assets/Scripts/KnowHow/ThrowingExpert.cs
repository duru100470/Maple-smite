using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingExpert : IKnowHow
{
    public PlayerModel ApplyEffect(PlayerModel playerModel)
    {
        playerModel.ThrowCooldown *= 1.1f;
        return playerModel;
    }

    public string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public KnowHowType GetKnowHowType()
        => KnowHowType.LEGENDARY_B;

    public string GetName()
    {
        throw new System.NotImplementedException();
    }
}
