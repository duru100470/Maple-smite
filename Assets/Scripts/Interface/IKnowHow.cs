using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnowHow
{
    KnowHowType GetKnowHowType();
    string GetName();
    string GetDescription();
    void ApplyEffect(PlayerModel playerModel);
}

public enum KnowHowType
{

}