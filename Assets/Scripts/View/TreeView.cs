using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TreeView : MonoBehaviour
{
    private TreeModel _treeModel;

    private float _screen_Width;
    private float _screen_Height;

    [field: SerializeField]
    private GameObject _tree_HP_UI;

    #region HP Bar.
    [field: SerializeField]
    private Slider _hp_Slider_1P;
    [field: SerializeField]
    private Slider _hp_Slider_2P;
    [field: SerializeField]
    private RectTransform _hp_1P;
    [field: SerializeField]
    private RectTransform _hp_2P;
    #endregion

    #region Damage Bar.
    [field: SerializeField]
    private Slider _damage_Slider_1P;
    [field: SerializeField]
    private Slider _damage_Slider_2P;
    [field: SerializeField]
    private RectTransform _damage_1P;
    [field: SerializeField]
    private RectTransform _damage_2P;

    [field: SerializeField]
    private float _damage_Bar_Speed = 100f;
    private WaitForSeconds _damage_Bar_Delay = new WaitForSeconds(.5f);
    #endregion

    #region Damage UI Tween.
    [field: SerializeField]
    private Vector3 _damageSeq_TO = new Vector3(0.05f, 0.05f, 0.05f);
    [field: SerializeField]
    private float _damageSeq_Duration = 0.2f;
    [field: SerializeField]
    private int _damageSeq_Vibrato = 0;

    private Sequence _damageSeq;
    #endregion

    private bool _firstAttack = false;

    public void Init(TreeModel treeModel)
    {
        _screen_Width = Screen.width;
        _screen_Height = Screen.height;

        Debug.Log("Sceen Size : (" + _screen_Width + ", " + _screen_Height + ")");

        _treeModel = treeModel;

        // 해상도 대응.
        _hp_1P.sizeDelta = new Vector2(_screen_Width * 0.5f, _hp_1P.sizeDelta.y);
        _hp_2P.sizeDelta = new Vector2(_screen_Width * 0.5f, _hp_2P.sizeDelta.y);

        _hp_1P.anchoredPosition = new Vector2(_screen_Width * 0.25f, _hp_1P.anchoredPosition.y);
        _hp_2P.anchoredPosition = new Vector2(-_screen_Width * 0.25f, _hp_2P.anchoredPosition.y);

        _hp_Slider_1P.maxValue = _treeModel.Health;
        _hp_Slider_1P.value = _treeModel.Health;
        _hp_Slider_1P.minValue = 0;

        _hp_Slider_2P.maxValue = _treeModel.Health;
        _hp_Slider_2P.value = _treeModel.Health;
        _hp_Slider_2P.minValue = 0;

        _damage_1P.anchoredPosition = new Vector2(0, _damage_1P.anchoredPosition.y);
        _damage_2P.anchoredPosition = new Vector2(0, _damage_2P.anchoredPosition.y);

        _firstAttack = false;

        _treeModel.OnHpChanged += UpdateHPUI;
    }

    private void UpdateHPUI(int prevHealth, int currentHealth)
    {
        int damage = prevHealth - currentHealth;

        _hp_Slider_1P.value = _treeModel.Health;
        _hp_Slider_2P.value = _treeModel.Health;

        // 데미지 크기만큼 데미지 슬라이더 생성.
        _damage_1P.sizeDelta = new Vector2(_hp_1P.sizeDelta.x * (damage / _hp_Slider_1P.maxValue), _damage_1P.sizeDelta.y);
        _damage_2P.sizeDelta = new Vector2(_hp_2P.sizeDelta.x * (damage / _hp_Slider_2P.maxValue), _damage_2P.sizeDelta.y);

        if (!_firstAttack)
        {
            _damage_1P.anchoredPosition += new Vector2(_damage_1P.sizeDelta.x * 0.5f, 0);
            _damage_2P.anchoredPosition -= new Vector2(_damage_2P.sizeDelta.x * 0.5f, 0);
            _firstAttack = true;
        }
        else
        {
            _damage_1P.anchoredPosition += new Vector2(_damage_1P.sizeDelta.x, 0);
            _damage_2P.anchoredPosition -= new Vector2(_damage_2P.sizeDelta.x, 0);
        }

        _damage_Slider_1P.maxValue = damage;
        _damage_Slider_2P.maxValue = damage;

        DamageBar();

        GetDamageUITween();
    }

    private void DamageBar()
    {
        _damage_Slider_1P.value = _damage_Slider_1P.maxValue;
        _damage_Slider_2P.value = _damage_Slider_2P.maxValue;

        StartCoroutine(DamageBarCoroutine());
    }

    private IEnumerator DamageBarCoroutine()
    {
        yield return _damage_Bar_Delay;

        while (_damage_Slider_1P.value > 0 && _damage_Slider_2P.value > 0)
        {
            _damage_Slider_1P.value -= _damage_Bar_Speed * Time.deltaTime;
            _damage_Slider_2P.value -= _damage_Bar_Speed * Time.deltaTime;

            yield return null;
        }
    }

    private void GetDamageUITween()
    {
        QuiAllActions();

        _damageSeq = DOTween.Sequence().Pause().SetUpdate(true)
        .Append(_tree_HP_UI.transform.DOScale(Vector3.one, 0f))
        .Append(_tree_HP_UI.transform.DOPunchScale(_damageSeq_TO, _damageSeq_Duration, _damageSeq_Vibrato).SetEase(Ease.OutQuad))
        .OnComplete(() =>
        {

        });

        _damageSeq.Restart();
    }

    private void QuiAllActions()
    {
        _damageSeq.Pause();
        _damageSeq.Kill();
    }
}
