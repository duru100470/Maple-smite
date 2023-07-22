using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RoundOverView : MonoBehaviour
{
    private RoundModel _roundModel;

    [field: SerializeField]
    private Image[] _1PCounts;
    [field: SerializeField]
    private Image[] _1PMasks;

    [field: SerializeField]
    private Image[] _2PCounts;
    [field: SerializeField]
    private Image[] _2PMasks;

    private int _1PCount = 0;
    private int _2PCount = 0;

    [field: SerializeField]
    private Vector3 _pointSeq_TO = new Vector3(0.05f, 0.05f, 0.05f);
    [field: SerializeField]
    private float _pointSeq_Duration = 0.2f;
    [field: SerializeField]
    private int _pointSeq_Vibrato = 0;

    private Sequence _pointSeq;

    private Sequence _roundSeq;

    public void Init(RoundModel roundModel)
    {
        _roundModel = roundModel;

        //_roundModel.OnStageChanged += RoundOverUI;
    }

    public void RoundOverUI(int winner)
    {
        Image count = null;
        Image mask = null;

        if (winner == 1)
        {
            count = _1PCounts[_1PCount];
            mask = _1PMasks[_1PCount];
            _1PCount++;
        }
        else
        {
            count = _2PCounts[_2PCount];
            mask = _2PMasks[_2PCount];
            _2PCount++;
        }

        PointUITween(count, mask);
    }

    private void PointUITween(Image count, Image mask)
    {
        _pointSeq = DOTween.Sequence().Pause().SetUpdate(true)
        .Append(count.transform.DOScale(0, 0.2f))
        .Append(mask.DOFade(1, 0))
        .Join(count.DOFade(0, 0))
        .Join(count.transform.DOScale(1, 0))
        .Join(mask.transform.DOPunchScale(_pointSeq_TO, _pointSeq_Duration, _pointSeq_Vibrato).SetEase(Ease.OutQuad))
        .OnComplete(() =>
        {

        });

        _pointSeq.Restart();
    }
}
