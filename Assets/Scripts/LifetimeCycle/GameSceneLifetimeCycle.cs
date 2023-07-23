using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

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
    [SerializeField]
    private RoundController _roundController;
    [SerializeField]
    private SkillView _skillView1;
    [SerializeField]
    private SkillView _skillView2;
    [SerializeField]
    private RoundOverView _roundOverView;
    [SerializeField]
    private StartRoundView _startRoundView;
    [SerializeField]
    private SelectCardView _selectCardView;

    public override void Initialize()
    {
        // Initialize models
        var playerModel1 = GetModelFromJson<PlayerModel>("Text/player1");
        var playerModel2 = GetModelFromJson<PlayerModel>("Text/player2");
        var roundModel = GetModelFromJson<RoundModel>("Text/round");
        var treeModel = new TreeModel();

        // Initialize controllers
        _keyInputSender1.Init();
        _keyInputSender2.Init();
        _playerController1.Init(playerModel1);
        _playerController2.Init(playerModel2);
        _treeController.Init(treeModel);
        _skillView1.Init(playerModel1);
        _skillView2.Init(playerModel2);

        _roundController.Init(roundModel, treeModel);
        _treeView.Init(treeModel);
        _roundOverView.Init(roundModel);
        _startRoundView.Init(roundModel);
        _selectCardView.Init(roundModel);
    }

    private T GetModelFromJson<T>(string path)
    {
        var text = Resources.Load<TextAsset>(path).text;
        return JsonConvert.DeserializeObject<T>(text);
    }

    public void Dispose()
    {

    }
}
