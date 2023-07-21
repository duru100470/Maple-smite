using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    private PlayerModel _playerModel;

    [field: SerializeField]
    private KeyInputSender _keyInputSender;

    [field: SerializeField]
    private Image _mask01;
    [field: SerializeField]
    private Image _mask02;
    [field: SerializeField]
    private Image _mask03;

    [field: SerializeField]
    private float _timer;

    [field: SerializeField]
    private float _coolTime;

    public void Init(PlayerModel playerModel)
    {
        _playerModel = playerModel;

        _keyInputSender.OnKeyPressed += StartCoolDown;

        // _coolTime = (쿨타임 시간)
    }

    private void StartCoolDown(KeyType keyType)
    {
        Debug.Log("호출!");

        Image mask;

        //switch (keyType)
        //{
        //    case KeyType.Skill01:
        //        if (!_canAttack) return;
        //        mask = _mask01;
        //        break;
        //    case KeyType.Skill02:
        //        if (!_canAttack) return;
        //        mask = _mask02;
        //        break;
        //    case KeyType.Skill03:
        //        if (!_canAttack) return;
        //        mask = _mask03;
        //        break;
        //}

        mask = _mask01;

        mask.fillAmount = 1;
        _timer = _coolTime;
        StartCoroutine(CoolDownTimerCoroutine(mask));
    }

    private IEnumerator CoolDownTimerCoroutine(Image mask)
    {
        while(_timer > 0)
        {
            _timer -= Time.deltaTime;

            mask.fillAmount = _timer / _coolTime;

            yield return null;
        }
    }
}
