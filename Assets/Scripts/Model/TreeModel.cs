using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreeModel
{
    private int _health;
    public int LastAttackerId { get; set; } = 0;
    public int MaxHealth { get; set; }
    public bool IsAttackable { get; set; } = true;

    public int Health
    {
        get => _health;
        set
        {
            if (!IsAttackable) return;

            OnHpChanged?.Invoke(_health, value);
            _health = Math.Max(value, 0);

            if (_health <= 0)
            {
                OnTreeDestroyed?.Invoke(LastAttackerId);
                Debug.Log("Tree was destroyed!");
                IsAttackable = false;
            }
        }
    }

    public TreeModel() { }

    public void Reset(int health)
    {
        _health = health;
        MaxHealth = health;
    }

    public event Action<int, int> OnHpChanged;
    public event Action<int> OnTreeDestroyed;
}
