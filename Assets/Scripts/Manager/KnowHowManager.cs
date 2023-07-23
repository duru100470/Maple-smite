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
    public List<IKnowHowBuff> KnowHowBuffList2 => _knowHowBuffList2;
    public List<IKnowHowDebuff> KnowHowDebuffList1 => _knowHowDebuffList1;
    public List<IKnowHowDebuff> KnowHowDebuffList2 => _knowHowDebuffList2;

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

    public List<KnowHowType> GetRandomEpicKnowHow()
    {
        var list = new List<KnowHowType>();

        while (list.Count < 4)
        {
            var knowhow = (KnowHowType)Random.Range(0, 5);

            if (list.Contains(knowhow)) continue;
            list.Add(knowhow);
        }

        return list;
    }

    public List<KnowHowType> GetRandomLegendaryKnowHow()
    {
        var list = new List<KnowHowType>();

        while (list.Count < 4)
        {
            var knowhow = (KnowHowType)Random.Range(5, 10);

            if (list.Contains(knowhow)) continue;
            list.Add(knowhow);
        }

        return list;
    }

    public void AddKnowHowToPlayer1(KnowHowType type)
    {
        try
        {
            _knowHowBuffList1.Add(type switch
            {
                KnowHowType.EPIC_A => new SkilledAxing(),
                KnowHowType.EPIC_B => new SkilledThrowing(),
                KnowHowType.EPIC_C => new MassiveAxing(),
                KnowHowType.EPIC_D => new MassiveThrowing(),
                KnowHowType.EPIC_E => new ChoiceAndFocus(),
                KnowHowType.LEGENDARY_C => new GoldAxeSilverAxe(),
                KnowHowType.LEGENDARY_D => new RockThrowing(),
                KnowHowType.LEGENDARY_E => new PunchingBag(),
                _ => throw new KeyNotFoundException()
            });
        }
        catch { }

        try
        {
            _knowHowDebuffList2.Add(type switch
            {
                KnowHowType.LEGENDARY_A => new AxingExpert(),
                KnowHowType.LEGENDARY_B => new ThrowingExpert(),
                _ => throw new KeyNotFoundException()
            });
        }
        catch { }
    }

    public void AddKnowHowToPlayer2(KnowHowType type)
    {
        try
        {
            _knowHowBuffList2.Add(type switch
            {
                KnowHowType.EPIC_A => new SkilledAxing(),
                KnowHowType.EPIC_B => new SkilledThrowing(),
                KnowHowType.EPIC_C => new MassiveAxing(),
                KnowHowType.EPIC_D => new MassiveThrowing(),
                KnowHowType.EPIC_E => new ChoiceAndFocus(),
                KnowHowType.LEGENDARY_C => new GoldAxeSilverAxe(),
                KnowHowType.LEGENDARY_D => new RockThrowing(),
                KnowHowType.LEGENDARY_E => new PunchingBag(),
                _ => throw new KeyNotFoundException()
            });
        }
        catch { }

        try
        {
            _knowHowDebuffList1.Add(type switch
            {
                KnowHowType.LEGENDARY_A => new AxingExpert(),
                KnowHowType.LEGENDARY_B => new ThrowingExpert(),
                _ => throw new KeyNotFoundException()
            });
        }
        catch { }
    }
}
