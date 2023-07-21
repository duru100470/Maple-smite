using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputSender : MonoBehaviour
{
    [SerializeField]
    private List<KeySetting> _keyPool = new();
    public event Action<KeyType> OnKeyPressed;

    public void Init()
    {
    }

    private void Update()
    {
        var newlyPressedKey = KeyType.None;

        foreach (var candidate in _keyPool)
        {
            if (Input.GetKeyDown(candidate.InputKey))
            {
                newlyPressedKey = candidate.Action;
            }
        }

        if (newlyPressedKey == KeyType.None) return;

        OnKeyPressed?.Invoke(newlyPressedKey);
    }
}

[Serializable]
public struct KeySetting
{
    public KeyCode InputKey;
    public KeyType Action;

    public KeySetting(KeyCode inputKey, KeyType action)
    {
        InputKey = inputKey;
        Action = action;
    }
}

public enum KeyType
{
    None,
    Axe,
    Throw,
    Jump
}