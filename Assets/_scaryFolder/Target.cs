using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    [SerializeField] float _rotationSpeedMultiplier01;
    [SerializeField] float _rotationSpeedMultiplier02;
    [SerializeField] GameObject _targetSolid;
    [SerializeField] GameObject[] _targetCuts;
    [SerializeField] GameObject[] _startKnifes;
    [SerializeField] bool _isCanStopRotate = false;
    [SerializeField] float _rotationStopMultiplier01;
    [SerializeField] float _rotationStopMultiplier02;

    float _rotationStop;
    float _rotationSpeed;
    float _rotationSpeedMultiplier;

    bool _isRoattionStop;

    void Start()
    {
        _isRoattionStop = false;

        _rotationSpeed = FindObjectOfType<GameManager>().WoodRotationSpeed;
        _rotationStop = FindObjectOfType<GameManager>().WoodRotationStop;

        foreach (var knife in _startKnifes)
        {
            knife.SetActive(false);
        }

        ChangeRotationSpeed();

        _rotationSpeedMultiplier = Random.Range(_rotationSpeedMultiplier01, _rotationSpeedMultiplier02);

        TargetSplit(false);

        FindObjectOfType<KnifeCount>().Win += Win;

        foreach (var knife in _startKnifes)
        {
            int chance = Random.Range(0, 3);
            if(chance == 1)
            {
                knife.SetActive(true);
            }
        }

        if(_isCanStopRotate)
            StartCoroutine(WoodStop());
    }


    void Update()
    {
        if(!_isRoattionStop)
            _targetSolid.transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime * _rotationSpeedMultiplier);
    }
    void TargetSplit(bool state)
    {
        foreach (var _cut in _targetCuts)
        {
            _cut.SetActive(state);
        }
    }

    public GameObject TargetSolid() { return _targetSolid; }

    void ChangeRotationSpeed()
    {
        if (FindObjectOfType<Knife>() != null)
        {
            _rotationSpeedMultiplier = Random.Range(_rotationSpeedMultiplier01, _rotationSpeedMultiplier02);
            FindObjectOfType<Knife>().Hit += ChangeRotationSpeed;
        }
    }

    void Win()
    {
        Handheld.Vibrate();
        _targetSolid.SetActive(false);
        TargetSplit(true);
        foreach (var cut in _targetCuts)
        {
            cut.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-100f, 100f), Random.Range(-500f, 500f)));
        }
    }

    IEnumerator WoodStop()
    {
        yield return new WaitForSeconds(_rotationStop * Random.Range(_rotationStopMultiplier01, _rotationStopMultiplier02));
        _isRoattionStop = true;
        yield return new WaitForSeconds(_rotationStop * Random.Range(_rotationStopMultiplier01, _rotationStopMultiplier02) / 10);
        _isRoattionStop = false;
        StartCoroutine(WoodStop());
    }
}
