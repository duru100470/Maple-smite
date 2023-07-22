using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreeModel
{
    private int _health;
    public int LastAttackerId { get; set; } = 0;

    public int Health
    {
        get => _health;
        set
        {
            OnHpChanged?.Invoke(_health, value);
            _health = Math.Max(value, 0);

            if (_health <= 0)
                OnTreeDestroyed?.Invoke(LastAttackerId);
        }
    }

    public TreeModel() { }

    public void Reset(int health)
    {
        _health = health;
    }

    public event Action<int, int> OnHpChanged;
    public event Action<int> OnTreeDestroyed;
}
