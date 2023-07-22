using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    private TreeModel _treeModel;
    public float Percent { get; set; }

    public void Init(TreeModel treeModel)
    {
        Debug.Log(treeModel);
        _treeModel = treeModel;
        _treeModel.OnTreeDestroyed += OnTreeDestroyed;
    }

    public void Reset(float percent)
    {
        Percent = percent;
        StartCoroutine(GetLinearDamage());
    }

    public void GetDamage(int amount, int attackerId)
    {
        Debug.Log(_treeModel);
        _treeModel.LastAttackerId = attackerId;
        _treeModel.Health -= amount;
    }

    public void GetDamagePercentage(float percent, int attackerId)
    {
        _treeModel.LastAttackerId = attackerId;
        _treeModel.Health -= (int)(_treeModel.MaxHealth * percent);
    }

    private void OnTreeDestroyed(int attackerId)
    {
    }

    private IEnumerator GetLinearDamage()
    {
        while (_treeModel.Health > _treeModel.MaxHealth * 0.15f)
        {
            yield return new WaitForSeconds(2f);
            GetDamagePercentage(Percent, 0);
        }
    }
}
