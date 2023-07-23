using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField]
    private List<AnimationClip> _clips;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private bool _autoStart = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_autoStart) Play(0);
    }

    public void Play(int index)
    {
        StopAllCoroutines();
        StartCoroutine(PlayClip(index));
    }

    public void Play(int index, float time)
    {
        StopAllCoroutines();
        var totalTime = _clips[index].interval * _clips[index].sprites.Length;
        StartCoroutine(PlayClip(index, time * _clips[index].interval / totalTime));
    }

    public void Stop()
        => StopAllCoroutines();

    private IEnumerator PlayClip(int index, float interval)
    {
        if (_clips[index].IsLoop)
        {
            int i = 0;
            while (_clips[index].IsLoop)
            {
                _spriteRenderer.sprite = _clips[index].sprites[i];
                yield return new WaitForSeconds(interval);
                if ((++i) >= _clips[index].sprites.Length) i = 0;
            }
        }
        else
        {
            for (int i = 0; i < _clips[index].sprites.Length; i++)
            {
                _spriteRenderer.sprite = _clips[index].sprites[i];
                yield return new WaitForSeconds(interval);
            }
        }
    }

    private IEnumerator PlayClip(int index)
        => PlayClip(index, _clips[index].interval);
}