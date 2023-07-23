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
    [SerializeField]
    private List<KnowHowType> _knowHowTypes;

    private Sequence _cardSeq;

    public bool SelectActivated = false;

    private bool _playerOneSelect = true;
    private bool _playerTwoSelect = true;

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

    public void CardSelectUITween(List<KnowHowType> knowHowTypes)
    {
        _knowHowTypes = knowHowTypes;

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

    private void CloseCard()
    {
        foreach (var card in _cards)
        {
            Sequence sequence = DOTween.Sequence().Pause().SetUpdate(true)
            .Append(card.rectTransform.DOAnchorPosY(200, 0.2f).SetEase(Ease.Linear))
            .Join(card.DOFade(0, 0.2f).SetEase(Ease.Linear));

            sequence.Restart();
        }
    }

    private IEnumerator CloseCardViewPanel()
    {
        Debug.Log("Closing Panel");
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1f);
        CloseCard();
        yield return new WaitForSeconds(1f);
        _back.DOFade(0f, 0.3f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(.3f);

        _startRoundView.StartCountDown(_roundModel.StageIndex);
        gameObject.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            _selectedCardNum01 = 0;
            SelectedCardTween(_cards[0]);
            _playerOneSelect = false;

            KnowHowManager.Inst.AddKnowHowToPlayer1(_knowHowTypes[0]);

            if (!_playerTwoSelect) StartCoroutine(CloseCardViewPanel());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _selectedCardNum01 = 1;
            SelectedCardTween(_cards[1]);
            _playerOneSelect = false;

            KnowHowManager.Inst.AddKnowHowToPlayer1(_knowHowTypes[1]);

            if (!_playerTwoSelect) StartCoroutine(CloseCardViewPanel());
        }
    }

    private void CheckPlayerTwo()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _selectedCardNum02 = 2;
            SelectedCardTween(_cards[2]);
            _playerTwoSelect = false;

            KnowHowManager.Inst.AddKnowHowToPlayer1(_knowHowTypes[2]);

            if (!_playerOneSelect) StartCoroutine(CloseCardViewPanel());
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            _selectedCardNum02 = 3;
            SelectedCardTween(_cards[3]);
            _playerTwoSelect = false;

            KnowHowManager.Inst.AddKnowHowToPlayer1(_knowHowTypes[3]);

            if (!_playerOneSelect) StartCoroutine(CloseCardViewPanel());
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
