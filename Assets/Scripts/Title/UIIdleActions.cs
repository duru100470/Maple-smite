using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIdleActions : MonoBehaviour
{
    private Sequence action;

    #region Pop Action.
    [field: SerializeField]
    private Vector3 defaultSize = Vector3.one;
    [field: SerializeField]
    private Vector3 pop_Action_TO = new Vector3(0.3f, 0.3f, 0.3f);
    [field: SerializeField]
    private float pop_Action_Duration = 0.2f;
    [field: SerializeField]
    private int pop_Action_Vibrato = 0;
    #endregion

    private void Awake()
    {
        Poping();
    }

    public void Poping()
    {
        action = DOTween.Sequence().Pause().SetUpdate(true).SetLoops(-1)
        .Append(transform.DOScale(defaultSize, 0f)) // Do not make UI getting bigger.
        .Append(transform.DOPunchScale(pop_Action_TO, pop_Action_Duration, pop_Action_Vibrato).SetEase(Ease.OutQuad));

        action.Restart();
    }
}
