using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChooseCardView : MonoBehaviour
{
    private RoundModel _roundModel;

    [field: SerializeField]
    private RoundController _roundController;

    [field: SerializeField]
    private Image _back;
    [field: SerializeField]
    private Image[] _cards;

    private Sequence _chooseCardSeq;

    public void Init(RoundModel roundModel)
    {
        _roundModel = roundModel;
    }

    public void CardChooseUITween()
    {
        _chooseCardSeq = DOTween.Sequence().Pause().SetUpdate(true)
        .Append(_back.DOFade(0.9f, 0.3f).SetEase(Ease.Linear))
        .Append(_cards[0].rectTransform.DOAnchorPosY(0, 0.2f).SetEase(Ease.Linear))
        .Join(_cards[0].DOFade(1, 0.2f).SetEase(Ease.Linear))
        .Append(_cards[1].rectTransform.DOAnchorPosY(0, 0.2f).SetEase(Ease.Linear))
        .Join(_cards[1].DOFade(1, 0.2f).SetEase(Ease.Linear))
        .Append(_cards[2].rectTransform.DOAnchorPosY(0, 0.2f).SetEase(Ease.Linear))
        .Join(_cards[2].DOFade(1, 0.2f).SetEase(Ease.Linear))
        .Append(_cards[3].rectTransform.DOAnchorPosY(0, 0.2f).SetEase(Ease.Linear))
        .Join(_cards[3].DOFade(1, 0.2f).SetEase(Ease.Linear))
        .OnComplete(() =>
        {

        });

        _chooseCardSeq.Restart();
    }
}
