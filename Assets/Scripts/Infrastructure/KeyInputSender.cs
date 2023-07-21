using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputSender : MonoBehaviour
{
    [SerializeField]
    private KeyCode _attackKey;
    [SerializeField]
    private Dictionary<KeyType, KeyCode> _keyPool = new();
    public event Action<KeyCode> OnKeyPressed;

    public void Init()
    {
        _keyPool.Add(KeyType.Attack, _attackKey);
    }

    private void Update()
    {
        var newlyPressedKey = KeyCode.None;

        foreach (var candidate in _keyPool.Values)
        {
            if (Input.GetKeyDown(candidate))
            {
                newlyPressedKey = candidate;
            }
        }

        if (newlyPressedKey == KeyCode.None) return;

        OnKeyPressed.Invoke(newlyPressedKey);
    }
}

public enum KeyType
{
    Attack
}