using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnowHowManager : SingletonBehavior<KnowHowManager>
{
    private List<IKnowHowBuff> _knowHowBuffList1 = new();
    private List<IKnowHowBuff> _knowHowBuffList2 = new();
    private List<IKnowHowDebuff> _knowHowDebuffList1 = new();
    private List<IKnowHowDebuff> _knowHowDebuffList2 = new();

    public List<IKnowHowBuff> KnowHowBuffList1 => _knowHowBuffList1;

    public List<IKnowHow> GetKnowHowEffectList(int id)
    {
        var buffs = id switch
        {
            1 => _knowHowBuffList1,
            2 => _knowHowBuffList2,
            _ => throw new System.Exception()
        };

        var debuffs = id switch
        {
            1 => _knowHowDebuffList2,
            2 => _knowHowDebuffList1,
            _ => throw new System.Exception()
        };

        var knowhows = new List<IKnowHow>();
        knowhows.AddRange(buffs);
        knowhows.AddRange(debuffs);

        return knowhows;
    }

    public List<IKnowHow> GetKnowHowList(int id)
    {
        var buffs = id switch
        {
            1 => _knowHowBuffList1,
            2 => _knowHowBuffList2,
            _ => throw new System.Exception()
        };

        var debuffs = id switch
        {
            1 => _knowHowDebuffList1,
            2 => _knowHowDebuffList2,
            _ => throw new System.Exception()
        };

        var knowhows = new List<IKnowHow>();
        knowhows.AddRange(buffs);
        knowhows.AddRange(debuffs);

        return knowhows;
    }
}
