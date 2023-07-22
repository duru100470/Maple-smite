using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField]
    private bool _isHeadingRight;
    private PlayerState _curState;
    [SerializeField]
    private int _id;

    public int Id => _id;

    private PlayerModel _playerModel;
    [Header("Dependencies")]
    [SerializeField]
    private KeyInputSender _keyInputSender;
    [SerializeField]
    private TreeController _treeController;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject _stoneObject;

    public void Init(PlayerModel playerModel)
    {
        _playerModel = playerModel;
        _playerModel.Id = Id;
        _curState = PlayerState.Idle;
        _keyInputSender.OnKeyPressed += PressKey;

        _playerModel.SkillDict[KeyType.Axe] = (PlayerController p) =>
        {
            if (_curState != PlayerState.Idle || _playerModel.Modified().IsAxeCooldown) return;
            StartCoroutine(DoAxing());
            StartCoroutine(SetAxeCooldown());
        };
        _playerModel.SkillDict[KeyType.Throw] = (PlayerController p) =>
        {
            if (_curState != PlayerState.Idle || _playerModel.Modified().IsThrowCooldown) return;
            StartCoroutine(DoThrowing());
            StartCoroutine(SetThrowCooldown());
        };
        _playerModel.SkillDict[KeyType.Jump] = (PlayerController p) =>
        {
            if (_curState != PlayerState.Idle) return;
            StartCoroutine(DoJump());
        };

        Reset();
    }

    public void Reset()
    {
        StopAllCoroutines();

        _playerModel.IsAxeCooldown = false;
        _playerModel.IsThrowCooldown = false;
        _curState = PlayerState.Idle;
    }

    private void PressKey(KeyType keyType)
    {
        _playerModel.Modified().SkillDict[keyType](this);
    }

    private IEnumerator DoAxing()
    {
        _treeController.GetDamage(_playerModel.Modified().AxeDamage, Id);
        _curState = PlayerState.Act;
        yield return new WaitForSeconds(_playerModel.Modified().AxeMotionTime);
        _curState = PlayerState.Idle;
    }

    private IEnumerator SetAxeCooldown()
    {
        _playerModel.IsAxeCooldown = true;
        yield return new WaitForSeconds(_playerModel.Modified().AxeCooldown);
        _playerModel.IsAxeCooldown = false;
    }

    private IEnumerator DoThrowing()
    {
        var go = Instantiate(_stoneObject, transform.position, Quaternion.identity);
        go.GetComponent<ProjectileController>().Init(_isHeadingRight, _playerModel.Modified().ThrowStunDuration, Id);

        _curState = PlayerState.Act;
        yield return new WaitForSeconds(_playerModel.Modified().ThrowMotionTime);
        _curState = PlayerState.Idle;
    }

    private IEnumerator SetThrowCooldown()
    {
        _playerModel.IsThrowCooldown = true;
        yield return new WaitForSeconds(_playerModel.Modified().ThrowCooldown);
        _playerModel.IsThrowCooldown = false;
    }

    private IEnumerator DoJump()
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY(transform.position.y + _playerModel.JumpHeight, _playerModel.JumpTime / 2).SetEase(Ease.OutQuad));
        seq.Append(transform.DOLocalMoveY(transform.position.y, _playerModel.JumpTime / 2).SetEase(Ease.InQuad));

        _curState = PlayerState.Act;
        yield return new WaitForSeconds(_playerModel.Modified().JumpTime);
        _curState = PlayerState.Idle;
    }

    private IEnumerator SetJumpCooldown()
    {
        _playerModel.IsJumpCooldown = true;
        yield return new WaitForSeconds(_playerModel.Modified().JumpCooldown);
        _playerModel.IsJumpCooldown = false;
    }

    public void GetStunned(float duration)
    {
        StopCoroutine(DoAxing());
        StopCoroutine(DoThrowing());
        StopCoroutine(DoJump());
        StartCoroutine(GetStunnedCoroutine(duration));
    }

    private IEnumerator GetStunnedCoroutine(float duration)
    {
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