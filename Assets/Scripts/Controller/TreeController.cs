using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    private TreeModel _treeModel;

    public void Init(TreeModel treeModel)
    {
        _treeModel = treeModel;
        _treeModel.OnTreeDestroyed += OnTreeDestroyed;
    }

    public void Reset(float percent)
    {
        StartCoroutine(GetLinearDamage(percent));
    }

    public void GetDamage(int amount, int attackerId)
    {
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

    private IEnumerator GetLinearDamage(float percent)
    {
        while (_treeModel.Health > _treeModel.MaxHealth * 0.15f)
        {
            yield return new WaitForSeconds(1f);
            GetDamagePercentage(percent, 0);
        }
    }
}
