using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartCountView : MonoBehaviour
{
    [field: SerializeField]
    private RoundController _roundController;
    private RoundModel _roundModel;

    #region Damage UI Tween.
    [field: SerializeField]
    private Vector3 _damageSeq_TO = new Vector3(0.05f, 0.05f, 0.05f);
    [field: SerializeField]
    private float _damageSeq_Duration = 0.2f;
    [field: SerializeField]
    private int _damageSeq_Vibrato = 0;

    private Sequence _damageSeq;
    #endregion

    public void Init(RoundModel roundModel)
    {

        _roundModel = roundModel;
    }
}
