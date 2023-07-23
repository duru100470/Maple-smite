using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnowHowDebuff : IKnowHow { }

public interface IKnowHow
{
    KnowHowType GetKnowHowType();
    string GetName();
    string GetDescription();
    PlayerModel ApplyEffect(PlayerModel playerModel);
}