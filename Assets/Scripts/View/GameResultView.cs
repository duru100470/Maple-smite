using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultView : MonoBehaviour
{
    private RoundModel _roundModel;
    [SerializeField]
    private Image[] _images;

    public void Init(RoundModel roundModel)
    {
        _roundModel = roundModel;
        _roundModel.OnGameOver += ShowWinner;
    }

    private void ShowWinner(int winner)
    {
        _images[winner - 1].color = Color.white;
        StartCoroutine(BackToMain());
    }

    private IEnumerator BackToMain()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }
}