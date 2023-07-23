using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UITweens : MonoBehaviour
{
    private Sequence start_Action;
    private Sequence end_Action;

    [field: SerializeField]
    private RectTransform rect;

    [field: SerializeField]
    private Image[] images;

    public int Trigger = 0;

    public bool IsPlaying = false;

    private void OnDisable()
    {
        QuitAllActions();
    }

    public void SceneTransition()
    {
        if (Trigger == 0)
        {
            SceneTransitionStart();
            Trigger++;
        }
        else
        {
            SceneTransitionEnd();
            Trigger--;
        }
    }

    private void SceneTransitionStart()
    {
        QuitAllActions();

        IsPlaying = true;

        start_Action = DOTween.Sequence().Pause().SetUpdate(true)
        .Append(images[0].rectTransform.DOAnchorPosX(300, 1)).SetDelay(0.8f)
        .Join(images[1].rectTransform.DOAnchorPosX(300, 1)).SetDelay(0.5f)
        .Join(images[2].rectTransform.DOAnchorPosX(300, 1)).SetDelay(0.2f)
        .OnComplete(() =>
        {
            IsPlaying = false;
        });
        //.Join(image.rectTransform.DOMove(image.rectTransform.position - new Vector3(0f, 100f, 0f), 1f).SetEase(Ease.OutBounce));

        start_Action.Restart();
    }

    private void SceneTransitionEnd()
    {
        QuitAllActions();

        IsPlaying = true;

        end_Action = DOTween.Sequence().Pause().SetUpdate(true)
        .Append(images[0].rectTransform.DOAnchorPosX(2450, 1)).SetDelay(0.8f)
        .Join(images[1].rectTransform.DOAnchorPosX(2450, 1)).SetDelay(0.5f)
        .Join(images[2].rectTransform.DOAnchorPosX(2450, 1)).SetDelay(0.2f)
        .OnComplete(() =>
        {
             IsPlaying = false;
        });

        end_Action.Restart();
    }

    private void QuitAllActions()
    {
        start_Action.Pause();
        start_Action.Kill();

        end_Action.Pause();
        end_Action.Kill();
    }
}
