using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using DG.Tweening;
using TMPro;

public class KnifeCount : MonoBehaviour
{
    MusicPlayer _musicPlayer;

    [SerializeField] int _knifeCount = 5;
    [SerializeField] GameObject _knife0;
    [SerializeField] GameObject _knife1;
    [SerializeField] string _nextLevelName;
    [SerializeField] TextMeshProUGUI _knifeCountTXT;

    [SerializeField] Button _menuBTN;

    bool _isLose;
    int _bossChance;

    public Action Win;

    void Start()
    {
        _musicPlayer = FindObjectOfType<MusicPlayer>();

        _menuBTN.onClick.AddListener(GoToMenu);
        CreateNewKnife();

        _menuBTN.transform.DOScale(1.1f, 1f).SetLoops(-1);

        _knifeCountTXT.text = "x " + _knifeCount;

        _bossChance = UnityEngine.Random.Range(0, 5);
    }

    public int CountOfKnives() { return _knifeCount; }

    void CreateNewKnife()
    {
        if (_isLose) return;
        else if (_knifeCount > 0 && !_isLose)
        {
            if(FindObjectOfType<GameManager>().KnifeId == 0)
                Instantiate(_knife0);
            else if (FindObjectOfType<GameManager>().KnifeId == 1)
                Instantiate(_knife1);

            _knifeCount--;
            _knifeCountTXT.text = "x " + _knifeCount;
            FindObjectOfType<Knife>().Hit += CreateNewKnife;
            FindObjectOfType<Knife>().Stop += LoseDaGame;
            return;
        }
        else if(_knifeCount <= 0 && !_isLose)
        {
            if(SceneManager.GetActiveScene().name == "bossLevel01")
            {
                FindObjectOfType<GameManager>().SetBonusKnife(1);
            }

            Win.Invoke();
            FindObjectOfType<ScoreKeeper>().LevelIncreace();
            StartCoroutine(LoadLevel(_nextLevelName, 2f));
        }
    }

    void LoseDaGame()
    {
        _musicPlayer.PlaySFX(_musicPlayer.LooseSFX);
        _isLose = true;
        StartCoroutine(LoadLevel("endMenu", 0.5f));
    }

    IEnumerator LoadLevel(string levelName, float time)
    {
        _musicPlayer.PlaySFX(_musicPlayer.WinSFX);
        yield return new WaitForSeconds(time);
        if (!_isLose)
        {
            if (_bossChance != 1)
                SceneManager.LoadScene(levelName);
            else
                SceneManager.LoadScene("bossLevel01");
        }
        else
            SceneManager.LoadScene(levelName);
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("startMenu");
        FindObjectOfType<ScoreKeeper>().ResetCurrentLevel();
    }
}
