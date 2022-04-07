using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
using Unity.Notifications.Android;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Button _startBTN;
    [SerializeField] Button _quitBTN;

    float _doTime;

    void Start()
    {
        _startBTN.onClick.AddListener(() => OpenLevel("level01"));
        _quitBTN.onClick.AddListener(QuitGame);

        _doTime = Random.Range(0.8f, 1.2f);

        StartCoroutine(ButtonDance(_startBTN));
        StartCoroutine(ButtonDance(_quitBTN));
    }

    public void OpenLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    
    void QuitGame()
    {
        Notify();
        Application.Quit();
    }

    public IEnumerator ButtonDance(Button btn)
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        btn.transform.DOScale(Random.Range(1.1f, 1.3f), _doTime).SetLoops(-1);
        //btn.transform.DORotate(new Vector3(0f, 0f, Random.Range(-5f, 5f)), _doTime).SetLoops(-1);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(btn.transform.DORotate(new Vector3(0f, 0f, Random.Range(-5f, -1f)), _doTime).SetLoops(-1, LoopType.Yoyo));
        sequence.Append(btn.transform.DORotate(new Vector3(0f, 0f, Random.Range(5f, 1f)), _doTime).SetLoops(-1));
        sequence.SetLoops(-1);
    }

    void Notify()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var notification = new AndroidNotification();
        notification.Title = "Hey! Come back!";
        notification.Text = "Camoooooon!!";
        notification.FireTime = System.DateTime.Now.AddHours(8);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
}