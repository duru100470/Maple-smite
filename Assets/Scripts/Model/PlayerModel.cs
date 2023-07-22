using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public int AxeDamage { get; set; }
    public float AxeCooldown { get; set; }
    public float AxeMotionTime { get; set; }
    public float ThrowStunDuration { get; set; }
    public float ThrowCooldown { get; set; }
    public float ThrowMotionTime { get; set; }
    public float JumpTime { get; set; }
    public float JumpCooldown { get; set; }
    public float JumpHeight { get; set; }

    private bool _isAxeCooldown = false;
    private bool _isThrowCooldown = false;
    private bool _isJumpCooldown = false;

    public bool IsAxeCooldown
    {
        get => _isAxeCooldown;
        set
        {
            if (value) OnAxeSkillUse?.Invoke(AxeCooldown);
            _isAxeCooldown = value;
        }
    }
    public bool IsThrowCooldown
    {
        get => _isThrowCooldown;
        set
        {
            if (value) OnThrowSkillUse?.Invoke(ThrowCooldown);
            _isThrowCooldown = value;
        }
    }
    public bool IsJumpCooldown
    {
        get => _isJumpCooldown;
        set
        {
            if (value) OnJumpSkillUse?.Invoke(JumpCooldown);
            _isJumpCooldown = value;
        }
    }

    public event Action<float> OnAxeSkillUse;
    public event Action<float> OnThrowSkillUse;
    public event Action<float> OnJumpSkillUse;

    private Dictionary<KeyType, Action<PlayerController>> _skillDict = new();
    public Dictionary<KeyType, Action<PlayerController>> SkillDict => _skillDict;

    private List<IKnowHow> _knowHowList = new();
    public List<IKnowHow> KnowHowList => _knowHowList;

    public PlayerModel Modified()
    {
        var model = this;

        foreach (var knowhow in _knowHowList)
        {
            model = knowhow.ApplyEffect(model);
        }

        return model;
    }
}