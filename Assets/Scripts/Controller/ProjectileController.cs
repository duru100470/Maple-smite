using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectileController : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField]
    private float _speed;
    private Sequence _seq;
    private int _ownerId;
    private float _duration;

    public void Init(bool isHeadingRight, float duration, int ownerId)
    {
        _ownerId = ownerId;
        _duration = duration;

        var (dist, _) = Camera.main.GetFrustum();
        _seq = DOTween.Sequence();
        _seq.Append(transform.DOLocalMoveX(transform.position.x + (isHeadingRight ? dist : (-1) * dist), dist / _speed)
            .SetEase(Ease.Linear))
            .AppendCallback(OnMoveEnd);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _ownerId != other.GetComponent<PlayerController>().Id)
        {
            other.GetComponent<PlayerController>().GetStunned(_duration);
            _seq.Kill();
            Destroy(gameObject);
        }
    }

    private void OnMoveEnd()
    {
        Destroy(gameObject);
    }
}
