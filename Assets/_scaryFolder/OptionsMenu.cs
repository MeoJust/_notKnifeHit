using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Button _resetBTN;
    [SerializeField] Button _knife01BTN;
    [SerializeField] Button _knife02BTN;
    [SerializeField] Button _throwDelayBTN0;
    [SerializeField] Button _throwDelayBTN1;
    [SerializeField] Button _throwDelayBTN2;
    [SerializeField] Slider _rotationSpeedSlider;
    [SerializeField] Slider _rotationStopSlider;

    void Start()
    {
        _rotationSpeedSlider.value = FindObjectOfType<GameManager>().WoodRotationSpeed;
        _rotationStopSlider.value = FindObjectOfType<GameManager>().WoodRotationStop;

        if(FindObjectOfType<GameManager>().IsBossDefiated == false)
        {
            _knife02BTN.GetComponent<Button>().enabled = false;
        }
        else
        {
            _knife02BTN.GetComponent<Button>().enabled = true;
        }

        _rotationSpeedSlider.onValueChanged.AddListener(SetWoodRotationSpeed);
        _rotationStopSlider.onValueChanged.AddListener(SetWoodRotationStop);

        _throwDelayBTN0.onClick.AddListener(() => SetKnifeThrowDelay(0));
        _throwDelayBTN1.onClick.AddListener(() => SetKnifeThrowDelay(1));
        _throwDelayBTN2.onClick.AddListener(() => SetKnifeThrowDelay(2));

        _resetBTN.onClick.AddListener(FindObjectOfType<ScoreKeeper>().ResetScore);
        _knife01BTN.onClick.AddListener(() => SetKnifeSkin(0));
        _knife02BTN.onClick.AddListener(() => SetKnifeSkin(1));
        StartCoroutine(FindObjectOfType<StartMenu>().ButtonDance(_resetBTN));
    }

    void SetKnifeSkin(int knifeId)
    {
        FindObjectOfType<GameManager>().KnifeId = knifeId;
    }

    void SetKnifeThrowDelay(int delay)
    {
        FindObjectOfType<GameManager>().KnifeThrowDelay = delay;
    }

    void SetWoodRotationSpeed(float value)
    {
        FindObjectOfType<GameManager>().WoodRotationSpeed = value;
    }

    void SetWoodRotationStop(float value)
    {
        FindObjectOfType<GameManager>().WoodRotationStop = value;
    }
}
