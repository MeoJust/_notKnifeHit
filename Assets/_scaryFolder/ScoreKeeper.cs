using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Instance;

    [SerializeField] TextMeshProUGUI _scoreTXT;
    [SerializeField] TextMeshProUGUI _melonsTXT;
    [SerializeField] TextMeshProUGUI _currentLevelTXT;
    [SerializeField] TextMeshProUGUI _maxLevelTXT;

    int _score;
    int _melons;
    int _currentLevel;
    int _maxLevel;

    void Awake()
    {
        if(Instance == null) 
        { 
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else 
            Destroy(gameObject);
    }

    void Start()
    {
        _currentLevel = 1;

        if(_maxLevel < _currentLevel) { _maxLevel = _currentLevel; }

        _score = PlayerPrefs.GetInt("Score");
        _melons = PlayerPrefs.GetInt("Melons");
        _maxLevel = PlayerPrefs.GetInt("MaxLevel");

        _currentLevelTXT.text = "current level: " + _currentLevel;
        _scoreTXT.text = "score: " + _score;
        _melonsTXT.text = "melons: " + _melons;
        _maxLevelTXT.text = "max level: " + _maxLevel;
    }

    public void IncreaceScore()
    {
        _score++;
        PlayerPrefs.SetInt("Score", _score);
        _scoreTXT.text = "score: " + _score.ToString();
        TXTMove(_scoreTXT);
    }

    public void IncreaceMelons()
    {
        _melons += Random.Range(5, 15);
        PlayerPrefs.SetInt("Melons", _melons);
        _melonsTXT.text = "melons: " + _melons.ToString();
        TXTMove(_melonsTXT);
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();
        _score = 0;
        _scoreTXT.text = "score: " +  PlayerPrefs.GetInt("Score", 0).ToString();
        _melonsTXT.text = "melons: " +  PlayerPrefs.GetInt("Melons", 0).ToString();
    }

    void TXTMove(TextMeshProUGUI txt)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(txt.transform.DOScale(Random.Range(1.2f, 1.5f), 0.1f));
        sequence.Append(txt.transform.DOScale(1f, 0f));
    }

    public void LevelIncreace()
    {
        _currentLevel++;
        _currentLevelTXT.text ="current level: " + _currentLevel.ToString();

        if(_maxLevel < _currentLevel)
        {
            _maxLevel = _currentLevel;
            PlayerPrefs.SetInt("MaxLevel", _maxLevel);
            _maxLevelTXT.text = "max level: " + _maxLevel.ToString();
        }
    }

    public void ResetCurrentLevel()
    {
        _currentLevel = 1;
        _currentLevelTXT.text = "current level: " + _currentLevel;
    }
}
