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
    [field: SerializeField]
    private Slider _hp_Slider_1P;
    [field: SerializeField]
    private Slider _hp_Slider_2P;
    [field: SerializeField]
    private Slider _damage_Slider_1P;
    [field: SerializeField]
    private Slider _damage_Slider_2P;
    [field: SerializeField]
    private RectTransform _damage_1P;
    [field: SerializeField]
    private RectTransform _damage_2P;


    [field: SerializeField]
    private Vector3 _damageSeq_TO = new Vector3(0.05f, 0.05f, 0.05f);
    [field: SerializeField]
    private float _damageSeq_Duration = 0.2f;
    [field: SerializeField]
    private int _damageSeq_Vibrato = 0;

    private Sequence _damageSeq;

    public void Init(TreeModel treeModel)
    {
        _treeModel = treeModel;

        _hp_Slider_1P.maxValue = _treeModel.Health;
        _hp_Slider_1P.value = _treeModel.Health;
        _hp_Slider_1P.minValue = 0;

        _hp_Slider_2P.maxValue = _treeModel.Health;
        _hp_Slider_2P.value = _treeModel.Health;
        _hp_Slider_2P.minValue = 0;
    }

    public void UpdateHPUI()
    {
        _hp_Slider_1P.value = _treeModel.Health;
        _hp_Slider_2P.value = _treeModel.Health;

        GetDamageUITween();
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
