using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class AccidentView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _accidentMessage;
    [SerializeField]
    private SpriteRenderer _darkSky;
    [SerializeField]
    private SpriteRenderer _lightning1;
    [SerializeField]
    private SpriteRenderer _lightning2;
    [SerializeField]
    private SpriteRenderer _lightRay;
    [SerializeField]
    private SpriteRenderer _rain;


    public void PrintMessage(string msg)
    {
        _accidentMessage.text = msg;
        _accidentMessage.color = Color.white;

        _accidentMessage.DOColor(new Color(1f, 1f, 1f, 0f), 3f);
    }

    public IEnumerator RaiseThunderAccident()
    {
        PrintMessage("Thunder is coming..");
        _darkSky.DOColor(Color.white, 1f);

        yield return new WaitForSeconds(3f);
        _lightning1.color = Color.white;
        _lightning1.DOColor(new Color(1f, 1f, 1f, 0f), 2f);
        yield return new WaitForSeconds(.5f);
        _lightning2.color = Color.white;
        _lightning2.DOColor(new Color(1f, 1f, 1f, 0f), 2f);
        yield return new WaitForSeconds(1f);
        _darkSky.DOColor(new Color(1f, 1f, 1f, 0f), 4f);

        // TODO: Sound 추가
    }

    public IEnumerator RaiseSunlightAccident()
    {
        PrintMessage("Sunlight is coming..");
        _lightRay.DOColor(Color.white, 1f);

        yield return new WaitForSeconds(3f);

        _lightRay.DOColor(new Color(1f, 1f, 1f, 0f), 1f);
    }

    public IEnumerator RaiseRainAccident()
    {
        PrintMessage("Rain is coming..");
        _darkSky.DOColor(Color.white, 1f);
        _rain.DOColor(Color.white, 1f);

        yield return new WaitForSeconds(3f);

        _rain.DOColor(new Color(1f, 1f, 1f, 0f), 1f);
        _darkSky.DOColor(new Color(1f, 1f, 1f, 0f), 1f);
    }

    public IEnumerator RaiseWoodcutterAccident()
    {
        PrintMessage("Woodcutter hit our wood..");
        yield return null;
    }
}
