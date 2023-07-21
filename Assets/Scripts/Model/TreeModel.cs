using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeModel
{
    private int _health;

    public int Health
    {
        get => _health;
        set
        {
            OnHpChanged?.Invoke(_health, value);
            _health = value;

            if (_health <= 0)
                OnTreeDestroyed?.Invoke();
        }
    }

    public TreeModel(int health)
    {
        _health = health;
    }

    public event Action<int, int> OnHpChanged;
    public event Action OnTreeDestroyed;
}