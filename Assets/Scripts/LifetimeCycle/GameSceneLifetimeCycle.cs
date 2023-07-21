using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneLifetimeCycle : LifetimeCycle
{
    [SerializeField]
    private PlayerController _playerController1;
    [SerializeField]
    private PlayerController _playerController2;
    [SerializeField]
    private TreeController _treeController;
    [SerializeField]
    private KeyInputSender _keyInputSender1;
    [SerializeField]
    private KeyInputSender _keyInputSender2;
    [SerializeField]
    private RoundController _roundController;

    public override void Initialize()
    {
        // FIXME: Hardcoded
        // Initialize models
        var playerModel1 = new PlayerModel();
        var playerModel2 = new PlayerModel();
        var roundModel = new RoundModel();

        // Initialize controllers
        _keyInputSender1.Init();
        _keyInputSender2.Init();
        _playerController1.Init(playerModel1);
        _playerController2.Init(playerModel2);

        _roundController.Init(roundModel);
    }

    public void Dispose()
    {

    }
}