using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    private RoundModel _roundModel;
    private TreeModel _treeModel;
    [Header("Dependencies")]
    [SerializeField]
    private TreeController _treeController;
    [SerializeField]
    private PlayerController _playerController1;
    [SerializeField]
    private PlayerController _playerController2;

    private float _accidentPercent;
    private bool _isAccidentHappen;

    public void Init(RoundModel roundModel, TreeModel treeModel)
    {
        _roundModel = roundModel;
        _treeModel = treeModel;
        _treeModel.OnTreeDestroyed += OnStageOver;
        _treeModel.OnHpChanged += CheckAccidentShouldHappen;

        StartNewRound();
    }

    // TODO: RoundView에서 증강 선택이 완료되거나, 시간이 만료되면 StartNewRound 호출
    public void StartNewRound()
    {
        var roundIdx = _roundModel.StageIndex;

        _treeModel.Reset(_roundModel.TreeHealthByStage[roundIdx - 1]);
        _treeController.Reset(_roundModel.TreeDamageByStage[roundIdx - 1]);
        _playerController1.Reset();
        _playerController2.Reset();

        _accidentPercent = UnityEngine.Random.Range(0.3f, 0.7f);
        _isAccidentHappen = false;
    }

    private void CheckAccidentShouldHappen(int _, int health)
    {
        if (((float)(_treeModel.Health) / (float)(_treeModel.MaxHealth) < _accidentPercent) && !_isAccidentHappen)
        {
            _isAccidentHappen = true;
            AccidentManager.Inst.OccurAccident(_roundModel.StageIndex);
        }
    }

    private void OnStageOver(int attackerId)
    {
        _roundModel.WinnerList.Add(attackerId switch
        {
            0 => 0,
            1 => 2,
            2 => 1,
            _ => throw new Exception()
        });
        _roundModel.StageIndex++;
    }
}
