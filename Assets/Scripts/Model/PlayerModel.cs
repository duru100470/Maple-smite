using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public int AxeDamage { get; set; } = 10;
    public float AxeCooldown { get; set; } = 1f;
    public float ThrowStunDuration { get; set; } = 1f;
    public float ThrowCooldown { get; set; } = 3f;
    public float JumpTime { get; set; } = 1f;
    public float JumpCooldown { get; set; } = 3f;
}