using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
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

    private void OnDisable()
    {
        QuitAllActions();
    }

    public void Poping()
    {
        QuitAllActions();

        action = DOTween.Sequence().Pause().SetUpdate(true)
        .Append(transform.DOScale(defaultSize, 0f)) // Do not make UI getting bigger.
        .Append(transform.DOPunchScale(pop_Action_TO, pop_Action_Duration, pop_Action_Vibrato).SetEase(Ease.OutQuad));

        action.Restart();
    }

    public void Jumping()
    {
        QuitAllActions();

        action = DOTween.Sequence().Pause().SetUpdate(true)
        .Append(transform.DOJump(transform.position, 2f, 1, 0.3f))
        .Join(transform.DOScale(1f, 0.1f).SetEase(Ease.Linear));

        action.Restart();
    }

    public void Closing()
    {
        QuitAllActions();

        action = DOTween.Sequence().Pause().SetUpdate(true)
        .Append(transform.DOScale(0f, 0.1f).SetEase(Ease.Linear));

        action.Restart();
    }

    private void QuitAllActions()
    {
        action.Pause();
        action.Kill();
    }
}
