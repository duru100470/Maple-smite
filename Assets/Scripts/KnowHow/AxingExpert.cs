using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxingExpert : IKnowHowDebuff
{
    public PlayerModel ApplyEffect(PlayerModel playerModel)
    {
        playerModel.AxeCooldown *= 1.1f;
        return playerModel;
    }

    public string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public KnowHowType GetKnowHowType()
        => KnowHowType.LEGENDARY_A;

    public string GetName()
    {
        throw new System.NotImplementedException();
    }
}
