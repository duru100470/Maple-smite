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

        SetHPUI(_hp_1P, _damage_1P, _hp_Slider_1P, 1);
        SetHPUI(_hp_2P, _damage_2P, _hp_Slider_2P, -1);

        _firstAttack = false;

        _treeModel.OnHpChanged += UpdateAllHPUI;
    }

    private void SetHPUI(RectTransform ui1, RectTransform ui2, Slider slider, int direction)
    {
        ui1.sizeDelta = new Vector2(_screen_Width * 0.5f, ui1.sizeDelta.y);
        ui1.anchoredPosition = new Vector2(direction * _screen_Width * 0.25f, ui1.anchoredPosition.y);
        slider.maxValue = _treeModel.Health;
        slider.value = _treeModel.Health;
        slider.minValue = 0;
        ui2.anchoredPosition = new Vector2(0, ui2.anchoredPosition.y);
    }

    private void UpdateAllHPUI(int prevHealth, int currentHealth)
    {
        int damage = prevHealth - currentHealth;

        UpdateDamageUI(damage, _hp_Slider_1P, _damage_Slider_1P, _hp_1P, _damage_1P);
        UpdateDamageUI(damage, _hp_Slider_2P, _damage_Slider_2P, _hp_2P, _damage_2P);

        UpdateDamageAnchor(_damage_1P, 1);
        UpdateDamageAnchor(_damage_2P, -1);

        if (!_firstAttack) _firstAttack = true;

        DamageBar();

        GetDamageUITween();
    }

    private void UpdateDamageUI(int damage, Slider slider1, Slider slider2, RectTransform ui1, RectTransform ui2)
    {
        slider1.value = _treeModel.Health;

        // 데미지 크기만큼 데미지 슬라이더 생성.
        ui2.sizeDelta = new Vector2(ui1.sizeDelta.x * (damage / slider1.maxValue), ui2.sizeDelta.y);

        slider2.maxValue = damage;
    }

    private void UpdateDamageAnchor(RectTransform ui, int direction)
    {
        if (!_firstAttack) ui.anchoredPosition += new Vector2(ui.sizeDelta.x * 0.5f, 0) * direction;
        else ui.anchoredPosition += new Vector2(ui.sizeDelta.x, 0) * direction;
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
