using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldAxeSilverAxe : IKnowHow
{
    public PlayerModel ApplyEffect(PlayerModel playerModel)
    {
        if (Random.Range(0, 10) < 3)
            playerModel.AxeDamage *= 2;

        return playerModel;
    }

    public string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public KnowHowType GetKnowHowType()
        => KnowHowType.LEGENDARY_C;

    public string GetName()
    {
        throw new System.NotImplementedException();
    }
}
