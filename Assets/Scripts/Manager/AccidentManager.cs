using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AccidentManager : SingletonBehavior<AccidentManager>
{
    [SerializeField]
    private TreeController _treeController;
    [SerializeField]
    private AccidentView _accidentView;

    public void OccurAccident(int stageIndex)
    {
        Debug.Log("Accident occurred");
        var accidentType = (Accident)(Random.Range(0, 4));

        switch (accidentType)
        {
            case Accident.Thunder:
                StartCoroutine(Thunder());
                StartCoroutine(_accidentView.RaiseThunderAccident());
                break;
            case Accident.Sunlight:
                StartCoroutine(Sunlight(stageIndex));
                StartCoroutine(_accidentView.RaiseSunlightAccident());
                break;
            case Accident.WoodCutter:
                StartCoroutine(Woodcutter());
                StartCoroutine(_accidentView.RaiseWoodcutterAccident());
                break;
            case Accident.Rain:
                StartCoroutine(Rain());
                StartCoroutine(_accidentView.RaiseRainAccident());
                break;
        }
    }

    private IEnumerator Sunlight(int stageIndex)
    {
        Debug.Log("Sunlight");
        for (int i = 0; i < 3; i++)
        {
            _treeController.GetDamagePercentage(stageIndex switch
            {
                1 => -.1f,
                2 => -.13f,
                3 => -.13f,
                4 => -.16f,
                5 => -.16f,
                _ => throw new Exception()
            }, 0);
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator Woodcutter()
    {
        for (int i = 0; i < 3; i++)
        {
            _treeController.GetDamagePercentage(Random.Range(.05f, .08f), 0);
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator Rain()
    {
        _treeController.Percent *= 2;
        yield return new WaitForSeconds(3f);
        _treeController.Percent /= 2;
    }

    private IEnumerator Thunder()
    {
        yield return new WaitForSeconds(3f);
        _treeController.GetDamagePercentage(0.18f, 0);
    }
}

public enum Accident
{
    Thunder,
    Sunlight,
    WoodCutter,
    Rain
}