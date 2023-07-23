using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilledAxing : IKnowHowBuff
{
    public PlayerModel ApplyEffect(PlayerModel playerModel)
    {
        playerModel.AxeCooldown *= 0.8f;
        return playerModel;
    }

    public string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public KnowHowType GetKnowHowType()
        => KnowHowType.EPIC_A;

    public string GetName()
    {
        throw new System.NotImplementedException();
    }
}
