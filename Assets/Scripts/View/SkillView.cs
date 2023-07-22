using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillView : MonoBehaviour
{
    private PlayerModel _playerModel;

    [field: SerializeField]
    private KeyInputSender _keyInputSender;

    [field: SerializeField]
    private GameObject _axeIcon;
    [field: SerializeField]
    private GameObject _throwIcon;
    [field: SerializeField]
    private GameObject _jumpIcon;

    [field: SerializeField]
    private Image _mask01;
    [field: SerializeField]
    private Image _mask02;
    [field: SerializeField]
    private Image _mask03;

    private float _timer;
    private float _effectTime = 0.5f;

    private float _axeCoolTime;
    private float _throwCoolTime;
    private float _jumpCoolTime;

    private bool _axeDisabled = false;
    private bool _throwDisabled = false;
    private bool _jumpDisabled = false;

    #region Skill UI Tween.
    [field: SerializeField]
    private Vector3 _skillSeq_TO = new Vector3(0.1f, 0.1f, 0.1f);
    [field: SerializeField]
    private float _skillSeq_Duration = 0.2f;
    [field: SerializeField]
    private int _skillSeq_Vibrato = 0;

    private Sequence _skillSeq;
    #endregion

    public void Init(PlayerModel playerModel)
    {
        _playerModel = playerModel;

        _playerModel.OnAxeSkillUse += StartAxeCoolDown;
        _playerModel.OnThrowSkillUse += StartThrowCoolDown;
        _playerModel.OnJumpSkillUse += StartJumpCoolDown;

        _axeCoolTime = _playerModel.AxeCooldown;
        _throwCoolTime = _playerModel.ThrowCooldown;
        _jumpCoolTime = _playerModel.JumpCooldown;
    }

    private void StartAxeCoolDown(float coolTime)
    {
        if (_axeDisabled) return;
        _mask01.fillAmount = 1;
        _timer = coolTime;
        SkillUITween(_axeIcon);
        StartCoroutine(CoolDownTimerCoroutine(_axeIcon, _mask01, _axeDisabled));
    }
    private void StartThrowCoolDown(float coolTime)
    {
        if (_throwDisabled) return;
        _mask02.fillAmount = 1;
        _timer = coolTime;
        SkillUITween(_throwIcon);
        StartCoroutine(CoolDownTimerCoroutine(_throwIcon, _mask02, _throwDisabled));
    }
    private void StartJumpCoolDown(float coolTime)
    {
        if (_jumpDisabled) return;
        _mask03.fillAmount = 1;
        _timer = coolTime;
        SkillUITween(_jumpIcon);
        StartCoroutine(CoolDownTimerCoroutine(_jumpIcon, _mask03, _jumpDisabled));
    }

    private IEnumerator CoolDownTimerCoroutine(GameObject icon, Image mask, bool trigger)
    {
        trigger = true;
        while(_timer > 0)
        {
            _timer -= Time.deltaTime;
            _effectTime -= Time.deltaTime;

            if (_effectTime < 0)
            {
                SkillUITween(icon);
                _effectTime = 0.5f;
            }

            mask.fillAmount = _timer / _axeCoolTime;

            yield return null;
        }
        _effectTime = 0.5f;
        trigger = false;
    }

    private void SkillUITween(GameObject icon)
    {
        _skillSeq = DOTween.Sequence().Pause().SetUpdate(true)
        .Append(icon.transform.DOScale(Vector3.one, 0f))
        .Append(icon.transform.DOPunchScale(_skillSeq_TO, _skillSeq_Duration, _skillSeq_Vibrato).SetEase(Ease.OutQuad))
        .OnComplete(() =>
        {

        });

        _skillSeq.Restart();
    }
}
