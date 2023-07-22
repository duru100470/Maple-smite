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
    private IEnumerator _stunCoroutine;
    private PlayerState _curState;
    [SerializeField]
    private int _id;

    public int Id => _id;
    public PlayerState CurState => _curState;

    private PlayerModel _playerModel;
    [Header("Dependencies")]
    [SerializeField]
    private KeyInputSender _keyInputSender;
    [SerializeField]
    private TreeController _treeController;

    public PlayerModel PlayerModel => _playerModel;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject _stoneObject;

    public void Init(PlayerModel playerModel)
    {
        _playerModel = playerModel;
        _playerModel.Id = Id;
        _curState = PlayerState.Idle;
        _keyInputSender.OnKeyPressed += PressKey;

        if (_isHeadingRight)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        _playerModel.SkillDict[KeyType.Axe] = (PlayerController p) =>
        {
            if (_curState != PlayerState.Idle || _playerModel.Modified().IsAxeCooldown) return;
            GetComponent<SpriteAnimator>().Play(1, _playerModel.Modified().AxeMotionTime);
            StartCoroutine(DoAxing());
            StartCoroutine(SetAxeCooldown());
        };
        _playerModel.SkillDict[KeyType.Throw] = (PlayerController p) =>
        {
            if (_curState != PlayerState.Idle || _playerModel.Modified().IsThrowCooldown) return;
            GetComponent<SpriteAnimator>().Play(2, _playerModel.Modified().ThrowMotionTime);
            StartCoroutine(DoThrowing());
            StartCoroutine(SetThrowCooldown());
        };
        _playerModel.SkillDict[KeyType.Jump] = (PlayerController p) =>
        {
            if (_curState != PlayerState.Idle || _playerModel.Modified().IsJumpCooldown) return;
            GetComponent<SpriteAnimator>().Play(3, _playerModel.Modified().JumpTime);
            StartCoroutine(DoJump());
            StartCoroutine(SetJumpCooldown());
        };

        Reset();
    }

    public void Reset()
    {
        StopAllCoroutines();

        GetComponent<SpriteAnimator>().Play(0);
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
        yield return new WaitForSeconds(_playerModel.Modified().AxeMotionTime / 2);

        if (_curState != PlayerState.Stun)
        {
            _treeController.GetDamage(_playerModel.Modified().AxeDamage, Id);
            _playerModel.AxeDamage = (int)(_playerModel.AxeDamage * 1.2f);
        }

        _curState = PlayerState.Act;
        yield return new WaitForSeconds(_playerModel.Modified().AxeMotionTime / 2);
        _curState = PlayerState.Idle;
        GetComponent<SpriteAnimator>().Play(0);
    }

    private IEnumerator SetAxeCooldown()
    {
        _playerModel.IsAxeCooldown = true;
        yield return new WaitForSeconds(_playerModel.Modified().AxeCooldown);
        _playerModel.IsAxeCooldown = false;
    }

    private IEnumerator DoThrowing()
    {
        _curState = PlayerState.Act;

        yield return new WaitForSeconds(_playerModel.Modified().ThrowMotionTime / 2);

        if (_curState != PlayerState.Stun)
        {
            var go = Instantiate(_stoneObject, transform.position, Quaternion.identity);
            go.GetComponent<ProjectileController>().Init(_isHeadingRight, _playerModel.Modified().ThrowStunDuration, Id);
        }

        yield return new WaitForSeconds(_playerModel.Modified().ThrowMotionTime / 2);
        _curState = PlayerState.Idle;
        GetComponent<SpriteAnimator>().Play(0);
    }

    public IEnumerator SetThrowCooldown()
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
        GetComponent<SpriteAnimator>().Play(0);
    }

    public IEnumerator SetJumpCooldown()
    {
        _playerModel.IsJumpCooldown = true;
        yield return new WaitForSeconds(_playerModel.Modified().JumpCooldown);
        _playerModel.IsJumpCooldown = false;
    }

    public void DoCleans()
    {
        if (_stunCoroutine != null)
        {
            StopCoroutine(_stunCoroutine);
            _stunCoroutine = null;
        }

        GetComponent<SpriteAnimator>().Play(0);
        _curState = PlayerState.Idle;
    }

    public IEnumerator DoAttackTree()
    {
        _treeController.GetDamagePercentage(0.05f, Id);
        GetComponent<SpriteAnimator>().Play(2, _playerModel.Modified().ThrowMotionTime);

        _curState = PlayerState.Act;
        yield return new WaitForSeconds(_playerModel.Modified().ThrowMotionTime);
        _curState = PlayerState.Idle;
    }

    public void GetStunned(float duration)
    {
        StopCoroutine(DoAxing());
        StopCoroutine(DoThrowing());
        StopCoroutine(DoJump());

        _stunCoroutine = GetStunnedCoroutine(duration);
        StartCoroutine(_stunCoroutine);
        GetComponent<SpriteAnimator>().Play(4);
    }

    private IEnumerator GetStunnedCoroutine(float duration)
    {
        _curState = PlayerState.Stun;
        yield return new WaitForSeconds(duration);
        _curState = PlayerState.Idle;
        GetComponent<SpriteAnimator>().Play(0);
    }
}

public enum PlayerState
{
    Idle,
    Act,
    Stun
}