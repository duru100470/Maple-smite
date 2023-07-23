using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartRoundView : MonoBehaviour
{
    private RoundModel _roundModel;

    [field: SerializeField]
    private RoundController _roundController;

    [field: SerializeField]
    private Image[] _countUI;

    #region Damage UI Tween.
    [field: SerializeField]
    private Vector3 _countDownSeq_TO = new Vector3(0.05f, 0.05f, 0.05f);
    [field: SerializeField]
    private float _countDownSeq_Duration = 0.2f;
    [field: SerializeField]
    private int _countDownSeq_Vibrato = 0;

    private Sequence _countDownSeq;
    #endregion

    [field: SerializeField]
    private int _startCountDown = 0;
    [field: SerializeField]
    private float _timer = 0f;

    [field: SerializeField]
    private Image[] _roundUI;

    private int _1PCount = 0;
    private int _2PCount = 0;

    [field: SerializeField]
    private Vector3 _roundSeq_TO = new Vector3(0.05f, 0.05f, 0.05f);
    [field: SerializeField]
    private float _roundSeq_Duration = 0.2f;
    [field: SerializeField]
    private int _roundSeq_Vibrato = 0;

    private Sequence _roundSeq;

    public void Init(RoundModel roundModel)
    {
        _roundModel = roundModel;

        StartCountDown(1);
    }

    public void StartCountDown(int stage, int winner = 0)
    {
        if (stage != 1) ChangeRoundUITween(_roundUI[stage - 1], _roundUI[stage]);
        else StartCoroutine(StartCountCoroutine());
    }

    private IEnumerator StartCountCoroutine()
    {
        while(_startCountDown < 3)
        {
            if (_timer < 0)
            {
                StartCountTween();
                _startCountDown++;
                _timer = 1f;
                continue;
            }
            _timer -= Time.deltaTime;

            yield return null;
        }
    }

    private void ChangeRoundUITween(Image current, Image next)
    {
        _roundSeq = DOTween.Sequence().Pause().SetUpdate(true)
        .Append(current.transform.DOScale(0, 0.2f))
        .Append(next.DOFade(1, 0))
        .Join(current.DOFade(0, 0))
        .Join(current.transform.DOScale(1, 0))
        .Join(next.transform.DOPunchScale(_roundSeq_TO, _roundSeq_Duration, _roundSeq_Vibrato).SetEase(Ease.OutQuad))
        .OnComplete(() =>
        {
            StartCoroutine(StartCountCoroutine());
        });

        _roundSeq.Restart();
    }

    private void StartCountTween()
    {
        _countDownSeq = DOTween.Sequence().Pause().SetUpdate(true)
        .Append(_countUI[_startCountDown].transform.DOScale(0.6f, 0))
        //.Join(_countUI[_startCountDown].DOFade(1, 0))
        .Join(_countUI[_startCountDown].transform.DORotate(new Vector3(0, 0, 360), 0.8f, RotateMode.FastBeyond360).SetEase(Ease.OutBack))
        .Join(_countUI[_startCountDown].transform.DOScale(1f, 0.6f).SetEase(Ease.Linear))
        //.Join(_countUI[_startCountDown].DOFade(0, 0.8f))
        .Append(_countUI[_startCountDown].transform.DOScale(0, 0.4f))
        .OnComplete(() =>
        {
            if (_startCountDown == 3)
            {
                _startCountDown = 0;
                _timer = 0f;
                _roundController.StartNewRound();
            }
        });

        _countDownSeq.Restart();
    }
}
