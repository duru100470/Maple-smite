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
    private GameObject _owner;
    private float _duration;

    public void Init(bool isHeadingRight, float duration, GameObject owner)
    {
        _owner = owner;
        _duration = duration;

        var (dist, _) = Camera.main.GetFrustum();
        _seq = DOTween.Sequence();
        _seq.Append(transform.DOLocalMoveX(transform.position.x + (isHeadingRight ? dist : (-1) * dist), dist / _speed)
            .SetEase(Ease.Linear))
            .AppendCallback(OnMoveEnd);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _owner != other.gameObject)
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
