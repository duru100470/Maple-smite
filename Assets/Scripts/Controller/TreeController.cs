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

    public void GetDamage(int amount)
    {
        _treeModel.Health -= amount;
        Debug.Log(_treeModel.Health);
    }

    private IEnumerator GetLinearDamage()
    {
        while (true)
        {
            GetDamage(10);
            yield return new WaitForSeconds(.5f);
        }
    }
}
