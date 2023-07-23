using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    [field: SerializeField]
    private UITweens _transitionUI;

    [field: SerializeField]
    private GameObject _startButton;
    [field: SerializeField]
    private GameObject _quitButton;

    [field: SerializeField]
    private string _sceneName;

    private Sequence sequence;

    [field: SerializeField]
    private Image _image;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private IEnumerator LoadSceneCoroutine()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneName);

        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (!_transitionUI.IsPlaying)
            {
                asyncOperation.allowSceneActivation = true;
                /* 주의  : 이곳에 다른 것을 호출할 경우 씹힐 수 있음 */
            }

            yield return null;
        }
        _transitionUI.SceneTransition();
    }

    public void LoadScene(string sceneName)
    {
        _sceneName = sceneName;
        _transitionUI.SceneTransition(); // 커튼 치기.
        _startButton.gameObject.SetActive(false);
        _quitButton.gameObject.SetActive(false);
        StartCoroutine(LoadSceneCoroutine());
    }

    // 씬이 로드될 때마다 호출되는 함수.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _startButton = GameObject.Find("StartBtn").gameObject;
        _quitButton = GameObject.Find("QuitBtn").gameObject;

        if (_startButton != null) _startButton.GetComponent<Button>().onClick.AddListener(() => LoadScene(_sceneName));
        if (_quitButton != null) _quitButton.GetComponent<Button>().onClick.AddListener(() => QuitApplication());
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    private void TutorialPopUp()
    {
        sequence = DOTween.Sequence().Pause().SetUpdate(true)
        .Append(_image.DOFade(1, 1))
        .Join(_image.DOFade(1, 4))
        .Join(_image.DOFade(0, 1))
        .OnComplete(() =>
        {
            _transitionUI.SceneTransition();
        });

        sequence.Restart();
    }
}
