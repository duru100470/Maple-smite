using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerModel _playerModel;
    [SerializeField]
    private KeyInputSender _keyInputSender;
    [SerializeField]
    private TreeController _treeController;
    private PlayerState _curState;

    public void Init(PlayerModel playerModel)
    {
        _playerModel = playerModel;
        _curState = PlayerState.Idle;
        _keyInputSender.OnKeyPressed += PressKey;
    }

    private void PressKey(KeyType keyType)
    {
        switch (keyType)
        {
            case KeyType.Axe:
                if (_curState == PlayerState.Idle) break;
                StartCoroutine(DoAxing());
                break;
            case KeyType.Throw:
                if (_curState == PlayerState.Idle) break;
                StartCoroutine(DoThrowing());
                break;
        }
    }

    private IEnumerator DoAxing()
    {
        _treeController.GetDamage(_playerModel.AxeDamage);
        _curState = PlayerState.Act;
        yield return new WaitForSeconds(_playerModel.AxeCooldown);
        _curState = PlayerState.Idle;
    }

    private IEnumerator DoThrowing()
    {
        // TODO: 던지기 구현
        _curState = PlayerState.Act;
        yield return new WaitForSeconds(_playerModel.ThrowCooldown);
        _curState = PlayerState.Idle;
    }

    private IEnumerator DoJump()
    {
        // TODO: 점프 구현
        _curState = PlayerState.Act;
        yield return new WaitForSeconds(_playerModel.JumpCooldown);
        _curState = PlayerState.Idle;
    }

    public void GetStunned(float duration)
    {
        StartCoroutine(GetStunnedCoroutine(duration));
    }

    private IEnumerator GetStunnedCoroutine(float duration)
    {
        StopAllCoroutines();
        _curState = PlayerState.Stun;
        yield return new WaitForSeconds(duration);
        _curState = PlayerState.Idle;
    }
}

public enum PlayerState
{
    Idle,
    Act,
    Stun
}