using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilledThrowing : IKnowHow
{
    public PlayerModel ApplyEffect(PlayerModel playerModel)
    {
        playerModel.ThrowCooldown *= .8f;
        return playerModel;
    }

    public string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public KnowHowType GetKnowHowType()
        => KnowHowType.EPIC_B;

    public string GetName()
    {
        throw new System.NotImplementedException();
    }
}
