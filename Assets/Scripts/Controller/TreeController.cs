using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    private TreeModel _treeModel;

    public void Init(TreeModel treeModel)
    {
        _treeModel = treeModel;
    }

    public void GetDamage(int amount, int attackerId)
    {
        _treeModel.LastAttackerId = attackerId;
        _treeModel.Health -= amount;
    }

    private IEnumerator GetLinearDamage()
    {
        while (true)
        {
            GetDamage(10, 0);
            yield return new WaitForSeconds(.5f);
        }
    }
}
