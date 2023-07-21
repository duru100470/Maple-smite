using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectileController : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField]
    private float _speed;

    public void Init(bool isHeadingRight)
    {
        var (dist, _) = Camera.main.GetFrustum();
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveX(transform.position.x + (isHeadingRight ? dist : (-1) * dist), dist / _speed)
            .SetEase(Ease.Linear))
            .AppendCallback(OnMoveEnd);
    }

    private void OnMoveEnd()
    {
        Destroy(gameObject);
    }
}
