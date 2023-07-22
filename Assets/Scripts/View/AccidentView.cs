using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class AccidentView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _accidentMessage;

    public void PrintMessage(string msg)
    {
        _accidentMessage.text = msg;
        _accidentMessage.color = Color.white;

        _accidentMessage.DOColor(new Color(1f, 1f, 1f, 0f), 1f);
    }
}
