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
    private TreeView _treeView;
    [SerializeField]
    private KeyInputSender _keyInputSender1;
    [SerializeField]
    private KeyInputSender _keyInputSender2;

    public override void Initialize()
    {
        // FIXME: Hardcoded
        // Initialize models
        var treeModel = new TreeModel(1000);
        var playerModel1 = new PlayerModel();
        var playerModel2 = new PlayerModel();

        // Initialize controllers
        _treeController.Init(treeModel);
        _treeView.Init(treeModel);
        _keyInputSender1.Init();
        _keyInputSender2.Init();
        _playerController1.Init(playerModel1);
        _playerController2.Init(playerModel2);
    }

    public void Dispose()
    {

    }
}
