using System;
using UnityEngine;
using DG.Tweening;

public class Knife : MonoBehaviour
{
    MusicPlayer _musicPlayer;

    [SerializeField] ParticleSystem _woodPart;
    [SerializeField] ParticleSystem _knifePart;
    [SerializeField] ParticleSystem _melonPart;

    Target _target;
    Rigidbody2D _rigidbody2D;

    bool _isStack;
    bool _canThrow;

    public Action Hit;
    public Action Stop;

    void Start()
    {
        _musicPlayer = FindObjectOfType<MusicPlayer>();

        _isStack = false;
        _target = FindObjectOfType<Target>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _canThrow = FindObjectOfType<GameManager>().KnifeThrowDelay < FindObjectOfType<GameManager>().TimeAfterThrow;

        if (Input.GetMouseButton(0) && !_isStack && _canThrow)
        {
            transform.DOScaleY(0.9f, 0.1f);
        }
        else if (Input.GetMouseButtonUp(0) && !_isStack && _canThrow)
        {
            _rigidbody2D.AddForce(new Vector3(0f, 2500f, 0f));
            transform.DOScaleY(1f, 0.05f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "knife")
        {
            _musicPlayer.PlaySFX(_musicPlayer.KnifeSFX);
            Handheld.Vibrate();
            Instantiate(_knifePart, transform);
            FindObjectOfType<ScoreKeeper>().ResetCurrentLevel();
            transform.parent = null;
            _rigidbody2D.AddForce(new Vector3(UnityEngine.Random.Range(-300f, 300f), -700f, UnityEngine.Random.Range(-300f, 300f)));
            Stop.Invoke();
        }
        else if (collision.tag == "target")
        {
            _musicPlayer.PlaySFX(_musicPlayer.WoodSFX);
            Handheld.Vibrate();
            Instantiate(_woodPart, transform);
            transform.parent = _target.TargetSolid().transform;
            _rigidbody2D.Sleep();
            _isStack = true;
            Hit.Invoke();
            FindObjectOfType<ScoreKeeper>().IncreaceScore();
            FindObjectOfType<GameManager>().TimeAfterThrowReset();

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_target.transform.DOScale(UnityEngine.Random.Range(0.8f, 1.2f), 0.1f));
            sequence.Append(_target.transform.DOScale(1f, 0.1f));
        }
    }

    public ParticleSystem GetMelonPart()
    {
        return _melonPart;
    }
}