using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float WoodRotationStop;
    public float WoodRotationSpeed;
    public int KnifeId = 0;
    public bool IsBossDefiated;
    [Range(0, 2)] public int KnifeThrowDelay;
    public float TimeAfterThrow = 0f;

    void Start()
    {
        WoodRotationStop = 0.5f;
        WoodRotationSpeed = 0.8f;

        if (PlayerPrefs.GetInt("Boss") == 0)
        {
            IsBossDefiated = false;
        }
        else
            IsBossDefiated = true;
    }

    void Update()
    {
        TimeAfterThrow += Time.deltaTime;
    }

    public void SetBonusKnife(int value)
    {
        PlayerPrefs.SetInt("Boss", value);

        if(PlayerPrefs.GetInt("Boss", value) == 0)
        {
            IsBossDefiated = false;
        }
        else
            IsBossDefiated = true;
    }

    public void TimeAfterThrowReset()
    {
        TimeAfterThrow = 0f;
    }
}
