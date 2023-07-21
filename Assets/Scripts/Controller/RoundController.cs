using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    private RoundModel _roundModel;
    [Header("Dependencies")]
    [SerializeField]
    private TreeController _treeController;
    [SerializeField]
    private PlayerController _playerController1;
    [SerializeField]
    private PlayerController _playerController2;

    public void Init(RoundModel roundModel)
    {
        _roundModel = roundModel;

        StartNewRound(_roundModel.StageIndex);
    }

    private void StartNewRound(int roundIdx)
    {
        var treeModel = new TreeModel(1000);
        _treeController.Init(treeModel);

        _playerController1.Reset();
        _playerController2.Reset();
    }
}