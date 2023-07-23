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

        StartCountDown(0);
    }

    public void StartCountDown(int stage, int winner = 0)
    {
        // TODO: 2, 4라운드로 바꿔야함
        Debug.Log(stage);

        if (stage != 0)
        {
            _roundUI[0].color = new Color(1f, 1f, 1f, 0f);
            ChangeRoundUITween(_roundUI[stage - 1], _roundUI[stage]);
        }
        else
            _roundUI[0].color = Color.white;
        StartCoroutine(StartCountCoroutine());
    }

    private IEnumerator StartCountCoroutine()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(2f);
        _countUI[0].color = Color.white;

        for (int i = 1; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            _countUI[i].color = Color.white;
            _countUI[i - 1].color = new Color(1f, 1f, 1f, 0f);
        }

        yield return new WaitForSeconds(1f);
        _countUI[2].color = new Color(1f, 1f, 1f, 0f);
        _roundController.StartNewRound();
    }

    private void ChangeRoundUITween(Image current, Image next)
    {
        current.color = new Color(1f, 1f, 1f, 0f);
        next.color = Color.white;
    }
}
