using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class RoundModel
{
    private int _stageIndex = 1;

    // 승자 목록
    [JsonIgnore]
    public List<int> WinnerList { get; set; } = new();
    // 라운드 별 나무 체력
    public List<int> TreeHealthByStage { get; set; } = new();
    // 라운드 별 나무 피다는 퍼센트
    public List<float> TreeDamageByStage { get; set; } = new();
    // 최대 몇 스테이지인지
    public int MaxStageIndex { get; set; } = 5;
    // 증강 선택 시간
    public float UpgradeSelectTime { get; set; }
    // 현재 몇 스테이지인지
    [JsonIgnore]
    public int StageIndex
    {
        get => _stageIndex;
        set
        {
            _stageIndex = value;
            CheckIsThereWinner();
            Debug.Log($"{_stageIndex - 1} Stage Over. Player {WinnerList[value - 2]} Won!");
            OnStageChanged?.Invoke(_stageIndex, WinnerList[value - 2]);
        }
    }
    // (몇 라운드인지, 1P/2P 중 누가 이겼는지)
    public event Action<int, int> OnStageChanged;
    public event Action<int> OnGameOver;

    private void CheckIsThereWinner()
    {
        var cnt1 = 0;
        var cnt2 = 0;

        for (int i = 0; i < WinnerList.Count; i++)
        {
            if (WinnerList[i] == 1) cnt1++;
            if (WinnerList[i] == 2) cnt2++;
        }

        if (cnt1 >= 3)
            OnGameOver?.Invoke(1);
        if (cnt2 >= 3)
            OnGameOver?.Invoke(2);
    }
}
