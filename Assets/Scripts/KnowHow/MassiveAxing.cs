using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassiveAxing : IKnowHowBuff
{
    public PlayerModel ApplyEffect(PlayerModel playerModel)
    {
        playerModel.AxeDamage = (int)(playerModel.AxeDamage * 1.5f);
        playerModel.AxeCooldown *= 2f;
        return playerModel;
    }

    public string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public KnowHowType GetKnowHowType()
        => KnowHowType.EPIC_C;

    public string GetName()
    {
        throw new System.NotImplementedException();
    }
}
