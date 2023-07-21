using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: PlayerModel 인스펙터 창에서 조절할 수 있게
public class PlayerModel
{
    public int AxeDamage { get; set; } = 10;
    public float AxeCooldown { get; set; } = 1f;
    public float ThrowStunDuration { get; set; } = 1f;
    public float ThrowCooldown { get; set; } = 3f;
    public float JumpTime { get; set; } = 1f;
    public float JumpCooldown { get; set; } = 3f;
    public float JumpHeight { get; set; } = 3f;
}