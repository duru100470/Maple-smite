using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: PlayerModel 인스펙터 창에서 조절할 수 있게
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
}