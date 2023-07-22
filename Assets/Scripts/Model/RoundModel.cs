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
            OnStageChanged?.Invoke(_stageIndex, WinnerList[value - 2]);
        }
    }

    public event Action<int, int> OnStageChanged;
}