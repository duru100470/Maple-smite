using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AnimationClip", fileName = "AnimationClip 1")]
public class AnimationClip : ScriptableObject
{
    public bool IsLoop = false;
    public Sprite[] sprites;
    public float interval = 0.25f;
}