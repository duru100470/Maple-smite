using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectCardView : MonoBehaviour
{
    private RoundModel _roundModel;

    [field: SerializeField]
    private RoundController _roundController;

    [field: SerializeField]
    private StartRoundView _startRoundView;

    [field: SerializeField]
    private Image _back;
    [field: SerializeField]
    private Image[] _cards;

    private Sequence _cardSeq;

    public bool SelectActivated = false;

    private bool _playerOneSelect = false;
    private bool _playerTwoSelect = false;

    private int _count = 0;

    private int _selectedCardNum01;
    private int _selectedCardNum02;

    public void Init(RoundModel roundModel)
    {
        _roundModel = roundModel;
    }

    private void Update()
    {
        if (_playerOneSelect) CheckPlayerOne();
        if (_playerTwoSelect) CheckPlayerTwo();
    }

    public void CardSelectUITween()
    {
        _cardSeq = DOTween.Sequence().Pause().SetUpdate(true)
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
            SelectActivated = true;
            _playerOneSelect = true;
            _playerTwoSelect = true;
        });

        _cardSeq.Restart();
    }

    public void CardCloseUITween(Image card)
    {
        _cardSeq = DOTween.Sequence().Pause().SetUpdate(true);

        for (int i = 0; i < 4; i++)
        {
            if (i != _selectedCardNum01 && i != _selectedCardNum02)
            {
                _cardSeq.Append(_cards[i].rectTransform.DOAnchorPosY(200, 0.2f).SetEase(Ease.Linear))
                .Join(_cards[i].DOFade(0, 0.2f).SetEase(Ease.Linear));
            }
        }
        _cardSeq.Append(_back.DOFade(0f, 0.3f).SetEase(Ease.Linear))
        .OnComplete(() =>
        {
            _roundController.StartNewRound();
        });

        _cardSeq.Restart();
    }

    private void SelectedCardTween(Image card)
    {
        Sequence sequence = DOTween.Sequence().Pause().SetUpdate(true)
        .Append(card.rectTransform.DOAnchorPosY(200, 0.2f).SetEase(Ease.Linear))
        .Join(card.DOFade(0, 0.2f).SetEase(Ease.Linear))
        .OnComplete(() =>
        {
            _count++;
        });

        sequence.Restart();
    }

    private void CheckPlayerOne()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _selectedCardNum01 = 0;
            SelectedCardTween(_cards[0]);
            _playerOneSelect = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _selectedCardNum01 = 1;
            SelectedCardTween(_cards[1]);
            _playerOneSelect = false;
        }

        if (!_playerOneSelect && !_playerTwoSelect)
        {
            _startRoundView.StartCountDown(_roundModel.StageIndex);
        }
    }

    private void CheckPlayerTwo()
    {
        if (Input.GetKey(KeyCode.K))
        {
            _selectedCardNum02 = 2;
            SelectedCardTween(_cards[2]);
            _playerTwoSelect = false;
        }
        if (Input.GetKey(KeyCode.L))
        {
            _selectedCardNum02 = 3;
            SelectedCardTween(_cards[3]);
            _playerTwoSelect = false;
        }

        if (!_playerOneSelect && !_playerTwoSelect)
        {
            _startRoundView.StartCountDown(_roundModel.StageIndex);
        }
    }

    private void SelectCard()
    {
        if (Input.GetKey(KeyCode.A))
        {
            SelectedCardTween(_cards[0]);
            _playerOneSelect = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            SelectedCardTween(_cards[1]);
            _playerOneSelect = false;
        }
        if (Input.GetKey(KeyCode.K))
        {
            SelectedCardTween(_cards[2]);
            _playerTwoSelect = false;
        }
        if (Input.GetKey(KeyCode.L))
        {
            SelectedCardTween(_cards[3]);
            _playerTwoSelect = false;
        }
    }
}
