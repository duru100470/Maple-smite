using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputSender : MonoBehaviour
{
    [SerializeField]
    private KeyCode _attackKey;
    [SerializeField]
    private Dictionary<KeyCode, KeyType> _keyPool = new();
    public event Action<KeyType> OnKeyPressed;

    public void Init()
    {
        _keyPool.Add(_attackKey, KeyType.Attack);
    }

    private void Update()
    {
        var newlyPressedKey = KeyType.None;

        foreach (var candidate in _keyPool.Keys)
        {
            if (Input.GetKeyDown(candidate))
            {
                newlyPressedKey = _keyPool[candidate];
            }
        }

        if (newlyPressedKey == KeyType.None) return;

        OnKeyPressed?.Invoke(newlyPressedKey);
    }
}

public enum KeyType
{
    None,
    Attack
}